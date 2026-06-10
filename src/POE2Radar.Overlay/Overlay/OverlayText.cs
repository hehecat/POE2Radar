using POE2Radar.Core.Game;

namespace POE2Radar.Overlay;

internal static class OverlayText
{
    private static readonly Dictionary<string, string> Exact = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Waypoint"] = "传送点",
        ["Checkpoint"] = "检查点",
        ["Entrance"] = "入口",
        ["Exit"] = "出口",
        ["AreaTransition"] = "区域出口",
        ["Stash"] = "仓库",
        ["Portal"] = "传送门",
        ["Town Portal"] = "城镇传送门",
        ["Quest Chest"] = "任务宝箱",
        ["Quest Object"] = "任务目标",
        ["Quest Marker"] = "任务标记",
        ["NPC"] = "NPC",
        ["Reforging Bench"] = "重铸台",
        ["Crafting Bench"] = "工艺台",
        ["Transmutation Bench"] = "工艺台",
        ["Strongbox"] = "保险箱",
        ["Shrine"] = "神龛",
        ["Essence"] = "精华",
        ["Ritual"] = "祭祀",
        ["Expedition"] = "远征",
        ["Breach"] = "裂隙",
        ["Delirium"] = "迷雾",
        ["Abyss"] = "深渊",
        ["Abyss Crack"] = "深渊裂缝",
        ["Incursion"] = "穿越",
        ["Legion"] = "军团",
        ["Betrayal"] = "辛迪加",
        ["Ultimatum"] = "最后通牒",
        ["Sanctum"] = "圣所",
        ["Delve"] = "矿坑",
        ["Heist"] = "夺宝",
        ["Blight"] = "菌潮",
        ["Hellscape"] = "炼狱",
        ["Arena"] = "竞技场",
        ["Boss"] = "首领",
        ["BossRoom"] = "首领房",
        ["Treasure"] = "宝藏",
        ["Encounter"] = "遭遇",
        ["Vault"] = "宝库",
        ["Reward"] = "奖励",
        ["Altar"] = "祭坛",
        ["Landmark"] = "地标",
        ["Medallion"] = "徽章",
        ["Sinkhole"] = "天坑",
        ["StairsUp"] = "上行楼梯",
        ["StairsDown"] = "下行楼梯",
        ["Market"] = "市场",
        ["Well"] = "井",
        ["HealingWell"] = "治疗井",
        ["Blacksmith"] = "铁匠",
        ["StoryGlyph"] = "剧情符文",
        ["Courtyard"] = "庭院",
        ["Gallows"] = "绞架",
        ["GrandEntrance"] = "大入口",
        ["Spire"] = "尖塔",
        ["Clearing"] = "空地",
        ["Dolmen"] = "石阵",
        ["Monolith"] = "巨石碑",
        ["Monster"] = "怪物",
        ["Player"] = "玩家",
        ["Chest"] = "宝箱",
        ["Transition"] = "出口",
        ["Point of Interest"] = "兴趣点",
        ["Hide dead monsters"] = "隐藏死亡怪物",
        ["Hide opened chests"] = "隐藏已开启宝箱",
        ["Hide completed encounters"] = "隐藏已完成遭遇",
        ["Mud Burrow"] = "泥穴",
        ["Beira of the Rotten (10% Cold Res)"] = "腐化者贝拉（10% 冰霜抗性）",
        ["The Corpse Tree (waypoint)"] = "尸体树（传送点）",
        ["The Moving Bramble (Skill Gem Level 2)"] = "移动荆棘（2 级技能宝石）",
        ["Areagne's Hut (Support Gem Level 1 and Flasks)"] = "阿雷格妮的小屋（1 级辅助宝石和药剂）",
        ["The Grim Tangle"] = "阴森网道",
        ["The Red Vale"] = "红谷",
        ["Den of the Druid (Support Gem Level 1)"] = "德鲁伊巢穴（1 级辅助宝石）",
        ["Cemetery of the Eternals"] = "永恒墓园",
        ["Mausoleum of the Praetor"] = "执政官陵墓",
        ["Tomb of the Consort"] = "王妃之墓",
        ["Arena / Hunting Grounds"] = "竞技场 / 猎场",
        ["Forgotten Riches"] = "遗忘财富",
        ["Freythorn"] = "茂棘深林",
        ["Crowbell Start (+2 Passive)"] = "鸦铃起点（+2 天赋）",
        ["Crowbell Arena (+2 Passive)"] = "鸦铃竞技场（+2 天赋）",
        ["Ogham Farmlands Or Deadend"] = "欧甘农地或死路",
        ["Ritual Site (Level 4 Skill Gem)"] = "祭祀地点（4 级技能宝石）",
        ["Dryadic Ritual (Support gem)"] = "林精祭祀（辅助宝石）",
        ["Hunting Grounds"] = "猎场",
        ["Una's Lute (+2 Passive)"] = "尤娜的鲁特琴（+2 天赋）",
        ["Crop Circle (Skill Gem Level 4)"] = "麦田圈（4 级技能宝石）",
        ["Executioner's Block"] = "处刑台",
        ["Renly's Tools (Salvaging Bench)"] = "伦利的工具（拆解台）",
        ["Oghams Manor"] = "欧甘庄园",
        ["Gallows (Support Gem Level 1)"] = "绞架（1 级辅助宝石）",
        ["Candlemass (+20 Max Life)"] = "烛弥撒（+20 最大生命）",
        ["Stairs Down"] = "下行楼梯",
        ["Stairs Up"] = "上行楼梯",
        ["Throne of the Wolf"] = "狼之王座",
        ["The Manor Ramparts"] = "庄园城壁",
        ["Prison of the Disgraced (Djinn Barya)"] = "耻辱者监牢（精魂巴亚）",
        ["The Halani Gates"] = "哈拉尼之门",
        ["The Ardura Caravan"] = "阿杜拉车队",
        ["Infested Tower"] = "滋孽高塔",
        ["Kabala (+2 Passive)"] = "卡巴拉（+2 天赋）",
        ["The Lost City"] = "失落之城",
        ["Looted Vault"] = "被洗劫的宝库",
        ["Buried Shrines"] = "埋没神殿",
        ["The Heart of Keth"] = "克斯之心",
        ["Elemental Offering (Res Ring)"] = "元素贡品（抗性戒指）",
        ["The Vestibule"] = "前厅",
        ["The Bone Pits"] = "遗迹深坑",
        ["Fossilised Memorial (Support Gem Level 1)"] = "化石纪念碑（1 级辅助宝石）",
        ["Mastodon Badlands"] = "长毛象荒原",
        ["Ancient Seal"] = "古代封印",
        ["The Titan Grotto"] = "泰坦石窟",
        ["Dais of Reckoning"] = "清算台座",
        ["Forgotten Corpses"] = "被遗忘的尸骸",
        ["The Spires of Deshar"] = "德莎尔尖塔",
        ["Deshar"] = "德莎尔",
        ["Path of Mourning"] = "哀悼之路",
        ["Mawdun Mine"] = "莫丹矿井",
        ["Munitions Bunker"] = "军火地堡",
        ["The Faridun Throne"] = "法里敦王座",
        ["Foul Ritual (Level 9)"] = "污秽祭祀（等级 9）",
        ["Ziggurat Encampment"] = "金字塔营地",
        ["Mini Town"] = "小镇",
        ["Chimeral Wetlands"] = "幻渺湿地",
        ["Canal Mechanism (waypoint)"] = "水道机关（传送点）",
        ["Jungle Ruins"] = "丛林废墟",
        ["The Azak Bog"] = "阿扎克沼泽",
        ["Larva Hollow"] = "幼虫巢穴",
        ["Troubled Camp (WEAPONS vendor)"] = "受困营地（武器商人）",
        ["Troubled Camp(ARMOUR VENDOR)"] = "受困营地（护甲商人）",
        ["Narag's Hut"] = "纳拉格的小屋",
        ["The Venom Crypts"] = "毒穴",
        ["Infested Barrens"] = "滋孽荒原",
        ["Deadly Nest"] = "致命巢穴",
        ["The Temple of Chaos"] = "混沌神殿",
        ["The Oubliette (10% Fire Res)"] = "地下囚牢（10% 火焰抗性）",
        ["Jiquani's Sanctum"] = "佳华尼的秘殿",
        ["Generator"] = "发电机",
        ["Corruption Altar"] = "腐化祭坛",
        ["Ignagduk, The Bog Witch (+30 Spirit)"] = "伊格纳杜克，沼泽女巫（+30 精魂）",
        ["Apex of Filth"] = "污秽巅峰",
        ["The Molten Vault"] = "熔火宝库",
        ["Mektul, The Forgemaster (Reforging Bench)"] = "梅克图，铸造大师（重铸台）",
        ["Queen of Filth"] = "污秽女王",
        ["Cauldron Keeper"] = "釜锅守卫",
        ["Sacrificial Dagger (+2 Passive)"] = "献祭匕首（+2 天赋）",
        ["The Black Chambers"] = "暗胧殿堂",
        ["Utzaal"] = "乌扎尔",
        ["Aggorat"] = "阿戈拉特",
        ["Volcanic Warrens"] = "火山洞群",
        ["Quest - Treasure map?"] = "任务 - 藏宝图？",
        ["Voltaxic spire"] = "伏击尖塔",
        ["Goblin Arena (+2 Passive)"] = "竞技场（+2 天赋）",
        ["Rhoa - No reward?"] = "恐喙鸟 - 可能无奖励？",
        ["Isle of Kin"] = "亲眷岛",
        ["Shrine - No loot?"] = "神龛 - 可能无战利品？",
        ["Quest - deadman chest"] = "任务 - 死者宝箱",
        ["Whakapanu Island"] = "瓦卡帕努岛",
        ["Forge Entrance"] = "熔炉入口",
        ["Saitwaste Entrance"] = "荒原入口",
    };

    private static readonly string[] ReplacementOrder =
    [
        "AreaTransition", "Town Portal", "Quest Chest", "Quest Object", "Quest Marker",
        "Reforging Bench", "Crafting Bench", "Transmutation Bench", "Abyss Crack",
        "GrandEntrance", "HealingWell", "StairsDown", "StairsUp", "BossRoom",
        "Checkpoint", "Strongbox", "Waypoint", "Entrance", "Treasure", "Encounter",
        "Reward", "Shrine", "Essence", "Ritual", "Expedition", "Breach", "Delirium",
        "Abyss", "Incursion", "Legion", "Betrayal", "Ultimatum", "Sanctum", "Delve",
        "Heist", "Blight", "Hellscape", "Boss", "Arena", "Vault", "Altar", "Landmark",
        "Medallion", "Sinkhole", "Market", "Well", "Blacksmith", "StoryGlyph",
        "Courtyard", "Gallows", "Spire", "Clearing", "Dolmen", "Monolith", "Exit",
        "Portal", "Stash", "Chest", "Monster", "Player", "Transition"
    ];

    public static string Localize(string? text)
    {
        if (string.IsNullOrWhiteSpace(text)) return text ?? "";

        var value = text.Trim();
        if (Exact.TryGetValue(value, out var exact)) return exact;

        if (value.StartsWith("-> ", StringComparison.Ordinal))
            return "→ " + Localize(value[3..]);
        if (value.StartsWith("→ ", StringComparison.Ordinal))
            return "→ " + Localize(value[2..]);

        var result = value.EndsWith(".tdt", StringComparison.OrdinalIgnoreCase)
            ? value[..^4]
            : value;
        var changed = false;
        foreach (var key in ReplacementOrder)
        {
            if (!Exact.TryGetValue(key, out var zh)) continue;
            if (!result.Contains(key, StringComparison.OrdinalIgnoreCase)) continue;
            result = result.Replace(key, zh, StringComparison.OrdinalIgnoreCase);
            changed = true;
        }

        return changed ? result.Replace('_', ' ') : value;
    }

    public static string EntityLabel(EntityNameResolver? resolver, string metadata)
    {
        var resolved = resolver?.Resolve(metadata);
        if (!string.IsNullOrWhiteSpace(resolved)) return Localize(resolved);
        return Localize(ShortName(metadata));
    }

    public static string TransitionLabel(GameDataIndex? data, EntityNameResolver? resolver, string metadata)
    {
        var shortName = ShortName(metadata);
        var area = data?.GetArea(shortName);
        if (area.HasValue && area.Value.Name.Length > 0) return area.Value.Name;
        return EntityLabel(resolver, metadata);
    }

    public static string ShortName(string metadata)
    {
        if (string.IsNullOrWhiteSpace(metadata)) return "";
        var at = metadata.IndexOf('@');
        var path = at >= 0 ? metadata[..at] : metadata;
        var slash = path.LastIndexOf('/');
        return slash >= 0 ? path[(slash + 1)..] : path;
    }
}
