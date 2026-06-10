using System.Reflection;
using System.Text.Json;

namespace POE2Radar.Core.Game;

/// <summary>
/// Resolves opaque entity metadata paths (e.g. <c>Metadata/Monsters/Wraith/WraithSpookyLightning</c>)
/// to human-friendly names (<c>"Lightning Wraith"</c>) via a lookup table. The table is reference
/// data baked into the assembly as the embedded <c>entity_names.json</c> (a flat path→name map with
/// lower-cased keys), so the overlay ships self-contained — no config seeding needed. Read-only:
/// this never touches game memory; it only maps strings the live reader already extracted.
///
/// <para>Ported from the privately-shared fork's name DB, but adapted to our embedded-resource
/// convention (cf. <see cref="CustomLandmarkData"/>) and given a bounded prefix-fallback instead of
/// the fork's full-table scan.</para>
/// </summary>
public sealed class EntityNameResolver
{
    private readonly Dictionary<string, string> _names;
    private readonly Dictionary<string, string> _zhByPath;
    private readonly Dictionary<string, string> _zhByName;

    private EntityNameResolver(Dictionary<string, string> names, Dictionary<string, string> zhByPath, Dictionary<string, string> zhByName)
    {
        _names = names;
        _zhByPath = zhByPath;
        _zhByName = zhByName;
    }

    /// <summary>The shared resolver, loaded once from the embedded table.</summary>
    public static EntityNameResolver Shared { get; } = LoadEmbedded();

    private static EntityNameResolver LoadEmbedded()
    {
        var names = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        var zhByPath = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        var zhByName = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        try
        {
            var asm = Assembly.GetExecutingAssembly();
            var resName = FindJsonResource(asm, "entity_names");
            if (resName != null)
            {
                using var stream = asm.GetManifestResourceStream(resName)!;
                var doc = JsonDocument.Parse(stream);
                foreach (var prop in doc.RootElement.EnumerateObject())
                    names[prop.Name] = prop.Value.GetString() ?? "";
            }

            var zhResName = FindJsonResource(asm, "entity_names_zh");
            if (zhResName != null)
            {
                using var stream = asm.GetManifestResourceStream(zhResName)!;
                var doc = JsonDocument.Parse(stream);
                foreach (var prop in doc.RootElement.EnumerateObject())
                {
                    var zh = prop.Value.GetString();
                    if (string.IsNullOrWhiteSpace(zh)) continue;
                    if (prop.Name.StartsWith("metadata/", StringComparison.OrdinalIgnoreCase))
                        zhByPath[prop.Name] = zh;
                    else
                        zhByName[prop.Name] = zh;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"EntityNames load failed: {ex.Message}");
        }
        return new EntityNameResolver(names, zhByPath, zhByName);
    }

    private static string? FindJsonResource(Assembly asm, string fileName)
    {
        var suffix = $".{fileName}.json";
        return asm.GetManifestResourceNames()
            .FirstOrDefault(n => n.EndsWith(suffix, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>Number of loaded name mappings (0 means the table failed to load).</summary>
    public int Count => _names.Count;

    /// <summary>
    /// Resolve a metadata path to a friendly name, or null if unknown. Tries an exact match first,
    /// then progressively drops trailing path segments and retries — so a spawn variant such as
    /// <c>.../WraithSpookyLightning/Apparition</c> still resolves to its base entry. Bounded by the
    /// path depth (a handful of lookups), unlike the fork's full-table scan.
    /// </summary>
    public string? Resolve(string metadataPath)
    {
        if (string.IsNullOrEmpty(metadataPath)) return null;

        // Strip a trailing runtime area-level annotation ("...MonkeyJungle@34"); table keys never
        // carry it, so without this every level-scaled monster would miss. Validated live: "@34"
        // suffixes defeated exact lookup until stripped.
        var at = metadataPath.IndexOf('@');
        var path = at >= 0 ? metadataPath[..at] : metadataPath;

        if (_zhByPath.TryGetValue(path, out var zhPath)) return zhPath;
        if (_names.TryGetValue(path, out var name)) return Localize(name);

        var probe = path;
        int slash;
        while ((slash = probe.LastIndexOf('/')) > 0)
        {
            probe = probe[..slash];
            if (_zhByPath.TryGetValue(probe, out zhPath)) return zhPath;
            if (_names.TryGetValue(probe, out name)) return Localize(name);
        }
        return null;
    }

    private string Localize(string name)
    {
        var clean = CleanMarker(name);
        return _zhByName.TryGetValue(clean, out var zh) ? zh : clean;
    }

    private static string CleanMarker(string name)
    {
        const string dntUnused = "[DNT-UNUSED] ";
        const string dnt = "[DNT] ";
        if (name.StartsWith(dntUnused, StringComparison.Ordinal)) return name[dntUnused.Length..];
        if (name.StartsWith(dnt, StringComparison.Ordinal)) return name[dnt.Length..];
        return name;
    }

    /// <summary>
    /// Friendly name if known, otherwise a best-effort short label derived from the path's last
    /// segment. Leading <c>[DNT]</c> markers (do-not-translate placeholders in the source table)
    /// are stripped.
    /// </summary>
    public string ResolveOrShorten(string metadataPath)
    {
        var resolved = Resolve(metadataPath);
        if (resolved != null)
        {
            return CleanMarker(resolved);
        }
        var at = metadataPath.IndexOf('@');
        var path = at >= 0 ? metadataPath[..at] : metadataPath;
        var slash = path.LastIndexOf('/');
        return slash >= 0 ? path[(slash + 1)..] : path;
    }
}
