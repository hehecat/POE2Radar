using POE2Radar.Core.Cheats;
using POE2Radar.Core.Game;
using POE2Radar.Overlay.Web;
using NumVec2 = System.Numerics.Vector2;

namespace POE2Radar.Overlay;

public readonly record struct MapPin(string Name, string Type);

public readonly record struct AtlasMark(
    float X, float Y, bool Selected, bool HasContent, bool Visited, bool Unlocked,
    int Biome, int IconType, string? Label = null, string? Color = null, bool Arrow = false);

public readonly record struct AtlasInspect(float X, float Y, IReadOnlyList<string> Lines);

public sealed class GameDataIndex
{
    public static GameDataIndex Shared { get; } = new();

    public ZoneGuide.ZoneArea? GetArea(string areaCode)
        => ZoneGuide.Shared.Area(areaCode);
}

public sealed record RenderContext(
    bool InGame,
    bool Active,
    int WindowWidth,
    int WindowHeight,
    NumVec2 PlayerGrid,
    Poe2Live.MapUi Map,
    IReadOnlyList<Poe2Live.EntityDot> Entities,
    IReadOnlyList<Poe2Live.Landmark> Landmarks,
    uint AreaHash,
    Poe2Live.TerrainData? Terrain,
    float ScaleMul,
    float OffsetX,
    float OffsetY,
    float HpPct,
    float ManaPct,
    string FlaskNote,
    string AreaCode,
    int CharLevel,
    float[]? CameraMatrix,
    IReadOnlyDictionary<string, CheatInfo>? CheatStatus = null,
    RadarSettings? Radar = null,
    bool OverlayVisible = true,
    POE2Radar.Overlay.Web.WatchedEntities? Watched = null,
    List<(int X, int Y)>? PathPoints = null,
    List<(float ScreenX, float ScreenY, string Metadata)>? EntityScreenPositions = null,
    List<(float ScreenX, float ScreenY, float GridX, float GridY, string Name)>? LandmarkScreenPositions = null,
    POE2Radar.Core.Pathfinding.ExplorationTracker? Exploration = null,
    string? InspectedName = null,
    string? InspectedMeta = null,
    string? CharName = null,
    string? AreaName = null,
    int AreaAct = 0,
    bool IsTown = false,
    Poe2Live.MinimapUi GameMinimap = default,
    HiddenEntities? Hidden = null,
    EntityNameResolver? EntityNames = null,
    GameDataIndex? GameData = null,
    IReadOnlyList<MapPin>? MapPins = null,
    bool ShowZoneGuide = false,
    string? ZoneGuideTitle = null,
    string? ZoneGuideNotes = null,
    string? PathTargetName = null,
    bool AtlasOpen = false,
    IReadOnlyList<AtlasMark>? AtlasNodes = null,
    float AtlasScale = 0.5f,
    float AtlasScaleY = 0.5f,
    float AtlasOffX = 0f,
    float AtlasOffY = 0f,
    float AtlasShearX = 0f,
    float AtlasShearY = 0f,
    float AtlasPersX = 0f,
    float AtlasPersY = 0f,
    AtlasInspect? AtlasInspect = null);
