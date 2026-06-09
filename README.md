# POE2Radar 中文融合版

这是一个面向 Path of Exile 2 的外部地图/雷达 overlay。当前版本以 `NattKh/POE2Radar` 为基底，合入了 `Sikaka/POE2Radar` 的实体名称、区域数据、隐藏规则、Atlas 节点读取和路线规划支撑代码，并进行了中文化。

> 风险自负。本工具会读取 PoE2 进程内存，部分功能还会写入游戏内存或发送按键。这可能违反游戏服务条款，并可能导致账号风险。仅供学习和研究。

## 主要功能

### 雷达 Overlay
- 地形地图：在游戏大地图上绘制可通行区域。
- 实体点位：显示怪物、NPC、宝箱、传送点、其他玩家和 POI。
- 稀有度图标：普通、魔法、稀有、传奇怪使用不同形状和颜色。
- 血条铭牌：在世界坐标上显示魔法/稀有/传奇怪血条。
- 地形地标：进图后显示 boss 房、奖励点、传送点等静态 tile 地标。
- 自定义地标：内置社区地标数据。
- 关注列表：可以给实体路径添加昵称，在 overlay 上显示自定义标签。
- 隐藏列表：可以隐藏噪音实体或地标，支持通配符。
- Atlas overlay：读取 Atlas 地图节点，并在 Atlas 打开时标记内容节点。

### 游戏补丁功能
所有补丁使用 AOB 扫描定位，保存原始字节并在退出时恢复。

| 热键 | 功能 | 说明 |
|---|---|---|
| F1 | 去 Atlas 迷雾 | 移除 Atlas 迷雾 |
| F2 | 揭示地图 | 显示完整小地图 |
| F3 | 无限缩放 | 移除缩放限制 |
| F4 | 敌人血条 | 强制显示敌人血条 |
| F5 | 玩家光照 | 调整光照半径 |

### 网页控制台
地址：`http://localhost:7777`，也可以在游戏中按 F11 打开。

控制台包含：
- 实时实体列表
- 关注实体列表和导入/导出
- 实体数据库
- 雷达显示设置
- 自动技能规则
- 路线目标
- 小地图设置
- 地标列表
- 隐藏列表
- 热键绑定
- 实体检查器

### 自动药剂
- HP 或 Mana 低于阈值时自动按药剂键。
- 仅在 PoE2 前台时工作。
- 支持独立冷却时间。
- F8 为总开关。

### 配置保存
配置保存在可执行文件旁边的 `config` 目录：
- `radar_settings.json`：雷达、显示、热键、药剂、Atlas 等设置。
- `watched_entities.json`：关注实体。
- `hidden_entities.json`：隐藏规则。
- `pathing_targets.json`：路线目标。
- `auto_rules.json`：自动技能规则。

## 热键

| 按键 | 动作 |
|---|---|
| F1 | 开关去 Atlas 迷雾 |
| F2 | 开关揭示地图 |
| F3 | 开关无限缩放 |
| F4 | 开关敌人血条 |
| F5 | 开关玩家光照 |
| F8 | 开关自动药剂 |
| F9 | 打开/关闭 WinForms 设置窗口 |
| F10 | 开关 overlay 显示 |
| F11 | 打开网页控制台 |
| PageUp/PageDown | 调整地图缩放 |
| 方向键 | 调整地图偏移 |
| Home | 重置校准 |
| Ctrl+C | 退出并恢复补丁字节 |

## 构建

需要 Windows x64 和 .NET 10 SDK。

```powershell
dotnet publish src/POE2Radar.Overlay/POE2Radar.Overlay.csproj -c Release -r win-x64 --self-contained
```

输出为 `Overlay.exe`。

## 运行

1. 启动 Path of Exile 2。
2. 以管理员身份运行 `Overlay.exe`。
3. 打开游戏内大地图查看雷达 overlay。
4. 按 F11 打开网页控制台进行设置。

## 项目结构

| 项目 | 作用 |
|---|---|
| `POE2Radar.Core` | 内存读取、offset、AOB 扫描、补丁引擎、Atlas/路径基础逻辑 |
| `POE2Radar.Overlay` | 主循环、Direct2D overlay、WinForms、HTTP API、网页控制台 |
| `POE2Radar.Research` | offset 研究和验证工具 |

## 来源

- 基于 [NattKh/POE2Radar](https://github.com/NattKh/POE2Radar)。
- 融合了 [Sikaka/POE2Radar](https://github.com/Sikaka/POE2Radar) 的部分支撑功能。
- Cheat 系统参考 [GameHelper2](https://github.com/mm3141/GameHelper)。
- 地标数据来自 PoE2 社区贡献。
