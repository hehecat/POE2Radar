namespace POE2Radar.Overlay.Web;

public static class DashboardHtml
{
    public const string Page = """
<!DOCTYPE html>
<html><head><meta charset="utf-8"><title>雷达控制台</title>
<style>
*{box-sizing:border-box;margin:0;padding:0}
body{background:#1a1a24;color:#e0e0e0;font-family:'Segoe UI',sans-serif;padding:16px;max-width:900px;margin:0 auto}
h1{font-size:18px;color:#78b4ff;margin-bottom:8px}
h2{font-size:14px;color:#78b4ff;margin:12px 0 6px}
.status{background:#252530;padding:8px 12px;border-radius:6px;margin-bottom:12px;font-size:13px;color:#aaa}
.status span{color:#fff}
.tabs{display:flex;gap:4px;margin-bottom:8px;flex-wrap:wrap}
.tab{padding:6px 16px;background:#252530;border:none;color:#aaa;cursor:pointer;border-radius:6px 6px 0 0;font-size:13px}
.tab.active{background:#2a2a3a;color:#78b4ff}
.panel{display:none;background:#2a2a3a;border-radius:0 6px 6px 6px;padding:12px}
.panel.active{display:block}
input[type=text]{background:#1e1e28;border:1px solid #444;color:#fff;padding:4px 8px;border-radius:4px;font-size:13px}
input[type=color]{width:36px;height:24px;border:1px solid #555;background:#1e1e28;cursor:pointer;vertical-align:middle;border-radius:3px}
input[type=number]{background:#1e1e28;border:1px solid #444;color:#fff;padding:4px 8px;border-radius:4px;width:80px;font-size:13px}
input[type=range]{width:150px;vertical-align:middle}
.search{margin-bottom:8px;display:flex;gap:8px;align-items:center}
.search input[type=text]{width:250px}
.filter-btns{display:flex;gap:4px;flex-wrap:wrap;margin-bottom:8px}
.filter-btn{padding:3px 10px;background:#333;border:1px solid #555;color:#ccc;cursor:pointer;border-radius:4px;font-size:12px}
.filter-btn.active{background:#3a5080;border-color:#78b4ff;color:#fff}
table{width:100%;border-collapse:collapse;font-size:12px}
th{text-align:left;padding:4px 8px;background:#1e1e28;color:#78b4ff;position:sticky;top:0}
td{padding:4px 8px;border-bottom:1px solid #333}
tr:hover{background:#333}
tr.watched{background:#2a3a2a}
.scrollbox{max-height:450px;overflow-y:auto}
.btn{padding:3px 10px;border:none;border-radius:4px;cursor:pointer;font-size:12px}
.btn-add{background:#2a5a2a;color:#8f8}.btn-add:hover{background:#3a7a3a}
.btn-rm{background:#5a2a2a;color:#f88}.btn-rm:hover{background:#7a3a3a}
.btn-save{background:#3a5080;color:#9cf;padding:6px 20px;font-size:13px}.btn-save:hover{background:#4a60a0}
.watched-item{display:flex;align-items:center;gap:8px;padding:6px 8px;background:#252530;border-radius:4px;margin-bottom:4px}
.watched-item .pattern{flex:1;font-family:monospace;font-size:11px;color:#999;overflow:hidden;text-overflow:ellipsis;white-space:nowrap}
.watched-item .label{color:#afc;font-size:13px;font-weight:bold;min-width:100px}
.watched-item .swatch{width:20px;height:20px;border-radius:3px;border:1px solid #555;flex-shrink:0}
.add-form{display:flex;gap:6px;align-items:center;padding:8px;background:#252530;border-radius:6px;margin-bottom:8px;flex-wrap:wrap}
.meta-short{max-width:300px;overflow:hidden;text-overflow:ellipsis;white-space:nowrap;font-family:monospace;font-size:11px}
.cat{padding:2px 6px;border-radius:3px;font-size:11px;font-weight:bold}
.cat-Monster{background:#4a2020;color:#f55}.cat-Player{background:#1a3a4a;color:#5cf}
.cat-Npc{background:#4a4a10;color:#fd5}.cat-Chest{background:#4a3010;color:#f90}
.cat-Transition{background:#1a4a2a;color:#6f9}.cat-Other{background:#333;color:#aaa}
.rarity-Magic{color:#79a8ff}.rarity-Rare{color:#ffd926}.rarity-Unique{color:#ff7300}
.auto-refresh{font-size:12px;color:#666;margin-left:auto}
.db-path{font-family:monospace;font-size:11px;color:#bbb}
.db-cat{color:#78b4ff;font-size:11px}
.count{background:#333;padding:2px 6px;border-radius:10px;font-size:11px;color:#aaa;margin-left:4px}
.setting-row{display:flex;align-items:center;gap:10px;margin-bottom:6px;min-height:28px}
.setting-row label{width:160px;font-size:13px;color:#ccc;flex-shrink:0}
.setting-row .val{font-size:12px;color:#78b4ff;width:50px;text-align:right}
.section{background:#252530;border-radius:6px;padding:10px 14px;margin-bottom:10px}
.section h3{font-size:13px;color:#78b4ff;margin-bottom:8px}
.saved{color:#5f5;font-size:12px;opacity:0;transition:opacity 0.3s}
.saved.show{opacity:1}
</style></head><body>
<h1>POE2Radar 控制台</h1>
<div class="status" id="status">正在连接...</div>

<div class="tabs">
  <button class="tab active" onclick="showTab('entities')">实时实体</button>
  <button class="tab" onclick="showTab('watched')">关注</button>
  <button class="tab" onclick="showTab('database')">数据库</button>
  <button class="tab" onclick="showTab('settings')">雷达设置</button>
  <button class="tab" onclick="showTab('rules')">自动技能</button>
  <button class="tab" onclick="showTab('pathing')">路线</button>
  <button class="tab" onclick="showTab('minimap')">小地图</button>
  <button class="tab" onclick="showTab('landmarks')">地标</button>
  <button class="tab" onclick="showTab('gamedata')">游戏数据</button>
  <button class="tab" onclick="showTab('hidden')">隐藏</button>
  <button class="tab" onclick="showTab('keybinds')">热键</button>
  <button class="tab" onclick="showTab('devtest')" style="color:#f88">开发测试</button>
  <button class="tab" onclick="showTab('inspector')">检查器</button>
</div>

<!-- LIVE ENTITIES -->
<div class="panel active" id="tab-entities">
  <div class="search">
    <input type="text" id="search" placeholder="搜索 metadata..." oninput="filterEntities()">
    <label><input type="checkbox" id="aliveOnly" onchange="refresh()"> 仅存活</label>
    <span class="auto-refresh">每 2 秒自动刷新</span>
  </div>
  <div class="filter-btns" id="catFilters"></div>
  <div class="scrollbox"><table><thead>
    <tr><th>类型</th><th>稀有度</th><th>Metadata</th><th>生命</th><th>距离</th><th></th></tr>
  </thead><tbody id="entityBody"></tbody></table></div>
</div>

<!-- WATCHED -->
<div class="panel" id="tab-watched">
  <h2>添加自定义关注</h2>
  <div class="add-form">
    <input type="text" id="addPattern" placeholder="Metadata 匹配片段" style="width:220px">
    <input type="text" id="addLabel" placeholder="雷达昵称" style="width:130px">
    <input type="color" id="addColor" value="#ff5555">
    <button class="btn btn-add" onclick="addWatched()">添加</button>
  </div>
  <p style="font-size:11px;color:#666;margin-bottom:8px">昵称会显示在 overlay 的实体点旁边。</p>
  <div style="display:flex;gap:8px;margin-bottom:10px;align-items:center">
    <h2 style="margin:0">当前关注</h2>
    <button class="btn btn-save" onclick="exportWatched()" style="margin-left:auto">导出 JSON</button>
    <button class="btn btn-add" onclick="$('importFile').click()">导入 JSON</button>
    <input type="file" id="importFile" accept=".json" style="display:none" onchange="importWatched(this)">
    <span class="saved" id="importMsg">已导入！</span>
  </div>
  <div id="watchedList"></div>
</div>

<!-- DATABASE -->
<div class="panel" id="tab-database">
  <div class="search">
    <input type="text" id="dbSearch" placeholder="搜索全部游戏实体..." style="width:350px" oninput="filterDb()">
    <label style="font-size:12px;color:#aaa"><input type="checkbox" id="dbHideJunk" checked onchange="filterDb()"> 隐藏垃圾项</label>
    <span id="dbCount" class="count"></span>
  </div>
  <div class="filter-btns" id="dbCatFilters"></div>
  <div class="scrollbox"><table><thead>
    <tr><th>分类</th><th>路径</th><th></th></tr>
  </thead><tbody id="dbBody"></tbody></table></div>
</div>

<!-- RADAR SETTINGS -->
<div class="panel" id="tab-settings">
  <div style="display:flex;justify-content:space-between;align-items:center;margin-bottom:10px">
    <h2 style="margin:0">雷达设置</h2>
    <div><span class="saved" id="savedMsg">已保存！</span> <button class="btn btn-save" onclick="saveSettings()">保存</button> <button class="btn" style="background:#2a3a5a;color:#8cf" onclick="exportSettings()">导出</button> <button class="btn" style="background:#2a5a3a;color:#8f8" onclick="$('settingsImportFile').click()">导入</button><input type="file" id="settingsImportFile" accept=".json" style="display:none" onchange="importSettings(event)"> <button class="btn" style="background:#5a2a2a;color:#f88" onclick="resetSettings()">重置</button></div>
  </div>
  <div id="settingsBody"></div>
</div>

<!-- AUTO-SKILLS -->
<div class="panel" id="tab-rules">
  <div style="display:flex;align-items:center;gap:10px;margin-bottom:10px">
    <h2 style="margin:0">自动技能规则</h2>
    <button class="btn btn-save" id="rulesToggle" onclick="toggleRules()">加载中...</button>
    <span style="font-size:11px;color:#888">游戏内也可用 F8 开关</span>
  </div>
  <p style="font-size:12px;color:#aaa;margin-bottom:10px">
    每条规则会在所有条件满足时按下指定按键；冷却时间用于防止连续触发。
  </p>
  <div id="rulesList"></div>
  <div class="section" style="margin-top:10px">
    <h3>添加规则</h3>
    <div style="display:flex;gap:6px;flex-wrap:wrap;align-items:center">
      <input type="text" id="ruleAddName" placeholder="名称" style="width:100px">
      <label style="font-size:12px;color:#ccc">按键：</label>
      <input type="text" id="ruleAddKey" placeholder="如 Q, 1, R" style="width:50px">
      <label style="font-size:12px;color:#ccc">冷却：</label>
      <input type="number" id="ruleAddCd" value="2" min="0.1" max="30" step="0.5" style="width:50px">
      <label style="font-size:12px;color:#ccc">生命&lt;</label>
      <input type="number" id="ruleAddHp" placeholder="-" min="0" max="100" style="width:50px">
      <label style="font-size:12px;color:#ccc">魔力&lt;</label>
      <input type="number" id="ruleAddMana" placeholder="-" min="0" max="100" style="width:50px">
      <label style="font-size:12px;color:#ccc">能盾&lt;</label>
      <input type="number" id="ruleAddEs" placeholder="-" min="0" max="100" style="width:50px">
      <label style="font-size:12px;color:#ccc">敌人&ge;</label>
      <input type="number" id="ruleAddEnemies" placeholder="-" min="0" max="50" style="width:50px">
      <label style="font-size:12px;color:#ccc">首领：</label>
      <select id="ruleAddBoss" style="width:60px"><option value="">-</option><option value="true">是</option><option value="false">否</option></select>
      <label style="font-size:12px;color:#ccc">等待(秒)：</label>
      <input type="number" id="ruleAddWait" placeholder="-" min="0" max="10" step="0.1" style="width:50px">
      <label style="font-size:12px;color:#ccc">移动中：</label>
      <select id="ruleAddMoving" style="width:60px"><option value="">-</option><option value="true">是</option><option value="false">否</option></select>
      <label style="font-size:12px;color:#ccc">按住键：</label>
      <input type="text" id="ruleAddHoldKey" placeholder="-" style="width:40px">
      <button class="btn btn-add" onclick="addRule()">添加</button>
    </div>
  </div>
</div>

<!-- PATHING -->
<div class="panel" id="tab-pathing">
  <p style="font-size:12px;color:#aaa;margin-bottom:10px">
    雷达会自动导航到下方启用目标中距离<b>最近</b>的匹配实体。<br>
    游戏内按 <b>F7</b> 可手动切换目标。
  </p>
  <div class="add-form">
    <input type="text" id="pathAddPattern" placeholder="Metadata 匹配片段" style="width:200px">
    <input type="text" id="pathAddLabel" placeholder="显示名称" style="width:120px">
    <button class="btn btn-add" onclick="addPathTarget()">添加</button>
    <button class="btn" style="background:#2a4a5a;color:#5cf;margin-left:auto" onclick="cyclePathTarget()">切换下一个 (F7)</button>
  </div>
  <div id="pathingList" style="margin-top:8px"></div>
</div>

<!-- MINIMAP -->
<div class="panel" id="tab-minimap">
  <div style="display:flex;justify-content:space-between;align-items:center;margin-bottom:10px">
    <h2 style="margin:0">小地图设置</h2>
    <div><span class="saved" id="mmSavedMsg">已保存！</span> <button class="btn btn-save" onclick="saveSettings();$('mmSavedMsg').classList.add('show');setTimeout(()=>$('mmSavedMsg').classList.remove('show'),1500)">保存</button></div>
  </div>
  <div id="minimapSettingsBody"></div>
</div>

<!-- LANDMARKS -->
<div class="panel" id="tab-landmarks">
  <div class="search"><input type="text" id="lmSearch" placeholder="搜索地标..." style="width:300px" oninput="filterLandmarks()"></div>
  <div class="scrollbox"><table><thead>
    <tr><th>名称</th><th>路径</th><th>Tile 数</th><th>距离</th><th></th></tr>
  </thead><tbody id="lmBody"></tbody></table></div>
</div>

<!-- DEVTEST -->
<div class="panel" id="tab-devtest">
  <div style="background:#3a1a1a;border:1px solid #f44;border-radius:6px;padding:10px;margin-bottom:12px">
    <h2 style="margin:0;color:#f88">⚠ 开发测试 - 写入游戏内存</h2>
    <p style="color:#faa;font-size:12px;margin:6px 0 0">这些选项会直接写入游戏实体内存，<b>很可能导致崩溃</b>。当前游戏版本的组件字段偏移需要重新验证，风险自负。</p>
  </div>
  <div id="devtestBody"></div>
  <div style="margin-top:10px"><button class="btn btn-save" onclick="saveSettings()">保存</button></div>
</div>

<!-- GAME DATA -->
<div class="panel" id="tab-gamedata">
  <div style="display:flex;gap:8px;margin-bottom:10px">
    <button class="btn btn-save" onclick="loadGdAreas()">区域表</button>
    <button class="btn btn-save" onclick="loadGdBuffs()">增益</button>
    <button class="btn btn-save" onclick="loadGdPins()">地图标记（当前区域）</button>
  </div>
  <div class="search"><input type="text" id="gdSearch" placeholder="搜索..." style="width:300px" oninput="searchGameData()"></div>
  <div class="scrollbox" id="gdResults" style="font-size:12px"></div>
</div>

<!-- HIDDEN ENTITIES -->
<div class="panel" id="tab-hidden">
  <h2>隐藏实体 / 地标</h2>
  <p style="font-size:12px;color:#aaa;margin-bottom:10px">
    这里列出的匹配规则会从雷达 overlay 中隐藏对应实体或地标。可以用“实时实体”和“地标”页里的<b>隐藏</b>按钮添加。<br>
    <b>通配符：</b><code>*</code> 表示任意字符，<code>?</code> 表示单个字符。例如 <code>*StrongBox</code> 会隐藏所有以 StrongBox 结尾的项。
    不使用通配符时按子串匹配。
  </p>
  <div class="add-form">
    <input type="text" id="hiddenAddPattern" placeholder="匹配规则，如 AbyssCrack, *StrongBox, Breach*" style="width:280px">
    <button class="btn btn-add" onclick="addHidden()">添加</button>
  </div>
  <div style="margin-top:8px" id="hiddenList"></div>
</div>

<!-- KEYBINDS -->
<div class="panel" id="tab-keybinds">
  <h2>热键绑定</h2>
  <p style="font-size:12px;color:#aaa;margin-bottom:10px">
    点击按键输入框后按下新按键即可重绑。点击<b>保存</b>后立即生效并持久化。
  </p>
  <div id="keybindsBody"></div>
  <div style="margin-top:10px">
    <button class="btn btn-save" onclick="saveKeybinds()">保存</button>
    <span class="saved" id="kbSavedMsg">已保存！</span>
  </div>
</div>

<!-- INSPECTOR -->
<div class="panel" id="tab-inspector">
  <div style="display:flex;gap:10px;align-items:center;margin-bottom:10px;flex-wrap:wrap">
    <select id="inspEntity" onchange="inspectEntity()" style="flex:1;min-width:200px;padding:4px;background:#1e1e1e;color:#eee;border:1px solid #555"></select>
    <button onclick="loadInspectorEntities()" style="padding:4px 12px;cursor:pointer">刷新</button>
    <label style="font-size:12px"><input type="checkbox" id="inspAutoRefresh" checked> 自动刷新</label>
    <span style="font-size:12px;color:#888" id="inspStatus"></span>
  </div>
  <div id="inspComponents" style="display:flex;flex-wrap:wrap;gap:4px;margin-bottom:10px"></div>
  <div class="scrollbox" id="inspResults" style="font-size:12px"></div>
</div>

<script>
let entities=[],watched=[],landmarks=[],db=[],settings={},catFilter='',dbCatFilter='';
const $=id=>document.getElementById(id);
const esc=s=>s.replace(/'/g,"\\'").replace(/"/g,'&quot;');
const CAT_ZH={Monster:'怪物',Player:'玩家',Npc:'NPC',Chest:'宝箱',Transition:'出口',Other:'其他'};
const RARITY_ZH={Normal:'普通',Magic:'魔法',Rare:'稀有',Unique:'传奇',NonMonster:'-'};
const LEAGUE_ZH={None:'',Expedition:'远征',Breach:'裂隙',Ritual:'祭祀',Delirium:'迷雾',Abyss:'深渊',Incursion:'穿越',Legion:'军团',Betrayal:'辛迪加',Ultimatum:'最后通牒',Sanctum:'圣所',Delve:'矿坑',Heist:'夺宝',Blight:'菌潮',Hellscape:'炼狱'};
const catLabel=c=>c==='All'?'全部':(CAT_ZH[c]||c);
const rarityLabel=r=>RARITY_ZH[r]||r;
const leagueLabel=l=>LEAGUE_ZH[l]||l;

function showTab(name){
  document.querySelectorAll('.tab').forEach(t=>t.classList.remove('active'));
  document.querySelectorAll('.panel').forEach(p=>p.classList.remove('active'));
  [...document.querySelectorAll('.tab')].find(t=>t.textContent.toLowerCase().includes(name.slice(0,4))||t.getAttribute('onclick')?.includes(name))?.classList.add('active');
  $('tab-'+name).classList.add('active');
  if(name==='watched')refreshWatched();
  if(name==='rules')refreshRules();
  if(name==='pathing')refreshPathing();
  if(name==='landmarks')refreshLandmarks();
  if(name==='database'&&db.length===0)loadDb();
  if(name==='settings')loadSettings();
  if(name==='devtest')loadDevTest();
  if(name==='gamedata')loadGdAreas();
  if(name==='minimap')loadMinimapSettings();
  if(name==='hidden')refreshHidden();
  if(name==='keybinds'){if(!settings||!Object.keys(settings).length)loadSettings().then(loadKeybinds);else loadKeybinds();}
  if(name==='inspector'){if(!Object.keys(inspSchema).length)loadInspectorSchema();loadInspectorEntities();}
}

// ── LIVE ENTITIES ──
async function refresh(){
  try{
    const s=await(await fetch('/state')).json();
    $('status').innerHTML=s.inGame
      ?`<span style="color:#fff">${s.areaName||s.areaCode}</span> <span style="color:#888">(${s.areaCode} · 第 ${s.act||'?'} 章 · 等级 ${s.areaLevel}${s.isTown?' · 城镇':''}${s.hasWaypoint?' · 传送点':''})</span> | ${s.player?.name||''} 等级 ${s.player?.level||'?'} | 生命 ${s.hpPct.toFixed(0)}% | 实体：${s.entityCount}`
      :'等待进入游戏...';
    const alive=$('aliveOnly').checked?'&alive=true':'';
    entities=await(await fetch('/entities?limit=1000'+alive)).json();
    renderEntities();
  }catch(e){$('status').textContent='连接已断开';}
}
function renderEntities(){
  const search=$('search').value.toLowerCase();
  const cats=[...new Set(entities.map(e=>e.category))];
  $('catFilters').innerHTML=['All',...cats].map(c=>
    `<button class="filter-btn ${catFilter===(c==='All'?'':c)?'active':''}" onclick="setCat('${c==='All'?'':c}')">${catLabel(c)}</button>`).join('');
  const f=entities.filter(e=>(!catFilter||e.category===catFilter)&&(!search||e.metadata.toLowerCase().includes(search)));
  $('entityBody').innerHTML=f.map(e=>`<tr class="${e.watched?'watched':''}">
    <td><span class="cat cat-${e.category}">${catLabel(e.category)}</span>${e.boss?'<span style="color:#f44;font-weight:bold" title="首领"> ★</span>':''}${e.league&&e.league!=='None'?`<span style="color:#0af;font-size:10px" title="联盟机制"> ${leagueLabel(e.league)}</span>`:''}</td>
    <td><span class="rarity-${e.rarity}">${rarityLabel(e.rarity)}</span></td>
    <td class="meta-short" title="${e.metadata}">${e.name||e.metadata}${e.locked?'<span style="color:#fa0" title="锁定"> 🔒</span>':''}${e.large?'<span style="color:#0af" title="大型"> L</span>':''}</td>
    <td>${e.hpMax>0?e.hpCur+'/'+e.hpMax:'-'}</td><td>${e.dist}</td>
    <td style="white-space:nowrap">
      ${e.watched?`<button class="btn btn-rm" onclick="rmByMeta('${esc(e.metadata)}')">-</button>`
                 :`<button class="btn btn-add" onclick="quickWatch('${esc(e.metadata)}')">关注</button>`}
      <button class="btn" style="background:#2a4a5a;color:#5cf" onclick="navigateTo('${esc(e.metadata)}')">导航</button>
      ${e.addr?`<button class="btn" style="background:#3a2a4a;color:#c8f" onclick="inspectFromList('${e.addr}')">检查</button>`:''}
      <button class="btn" style="background:#4a3a1a;color:#fa0" onclick="hideFromEntity('${esc(e.metadata)}')">隐藏</button>
    </td>
  </tr>`).join('');
}
function setCat(c){catFilter=c;renderEntities();}
function filterEntities(){renderEntities();}

async function navigateTo(meta){
  const short=meta.split('/').pop().replace(/@\d+$/,'');
  await fetch('/api/settings',{method:'POST',headers:{'Content-Type':'application/json'},
    body:JSON.stringify({showPath:true,pathTarget:short})});
}

// ── WATCHED ──
async function quickWatch(meta){
  const parts=meta.split('/');const def=parts[parts.length-1].replace(/@\d+$/,'');
  const nick=prompt('雷达显示昵称：',def);if(nick===null)return;
  await doAdd(meta,nick||def,$('addColor').value);
}
async function addWatched(){
  const p=$('addPattern').value.trim(),l=$('addLabel').value.trim(),c=$('addColor').value;
  if(!p)return;await doAdd(p,l||p.split('/').pop(),c);$('addPattern').value='';$('addLabel').value='';
}
async function doAdd(pattern,label,color,size=7){
  await fetch('/api/watched',{method:'POST',headers:{'Content-Type':'application/json'},body:JSON.stringify({pattern,label,color,enabled:true,size})});
  refresh();refreshWatched();
}
async function rmByMeta(meta){const w=watched.find(w=>meta.includes(w.pattern));if(w)await rmWatched(w.pattern);}
async function rmWatched(pattern){await fetch('/api/watched?pattern='+encodeURIComponent(pattern),{method:'DELETE'});refreshWatched();refresh();}
async function refreshWatched(){
  watched=await(await fetch('/api/watched')).json();
  $('watchedList').innerHTML=watched.map(w=>
    `<div class="watched-item">
      <input type="color" value="${w.color}" onchange="editWatched('${esc(w.pattern)}',{color:this.value})" title="颜色">
      <input type="text" value="${w.label}" style="width:120px;background:#1e1e28;border:1px solid #444;color:#afc;border-radius:3px;padding:2px 6px;font-size:13px;font-weight:bold"
        onchange="editWatched('${esc(w.pattern)}',{label:this.value})" title="昵称">
      <input type="number" value="${w.size??7}" min="1" max="30" step="0.5" style="width:55px;background:#1e1e28;border:1px solid #444;color:#78b4ff;border-radius:3px;padding:2px 4px;font-size:12px"
        onchange="editWatched('${esc(w.pattern)}',{size:parseFloat(this.value)})" title="点大小">
      <div class="pattern" title="${w.pattern}">${w.pattern}</div>
      <label style="font-size:11px;color:#888;white-space:nowrap"><input type="checkbox" ${w.enabled?'checked':''} onchange="editWatched('${esc(w.pattern)}',{enabled:this.checked})"> 启用</label>
      <button class="btn btn-rm" onclick="rmWatched('${esc(w.pattern)}')">X</button>
    </div>`
  ).join('')||'<div style="color:#666;padding:8px">还没有关注实体。</div>';
}
async function editWatched(pattern,changes){
  const w=watched.find(w=>w.pattern===pattern);if(!w)return;
  const updated={...w,...changes};
  await fetch('/api/watched',{method:'PUT',headers:{'Content-Type':'application/json'},body:JSON.stringify(updated)});
  refreshWatched();
}
function exportWatched(){
  const blob=new Blob([JSON.stringify(watched,null,2)],{type:'application/json'});
  const a=document.createElement('a');a.href=URL.createObjectURL(blob);
  a.download='watched_entities.json';a.click();URL.revokeObjectURL(a.href);
}
async function importWatched(input){
  if(!input.files[0])return;
  const text=await input.files[0].text();
  try{
    const r=await fetch('/api/watched/import',{method:'POST',headers:{'Content-Type':'application/json'},body:text});
    const res=await r.json();
    if(res.ok){$('importMsg').textContent=`已导入 ${res.imported} 条！`;$('importMsg').classList.add('show');setTimeout(()=>$('importMsg').classList.remove('show'),2000);}
    else alert('导入失败：'+res.error);
    refreshWatched();refresh();
  }catch(e){alert('JSON 文件无效');}
  input.value='';
}

// ── DATABASE ──
const JUNK_PATTERNS=['/attachments','monstermods','microtransactions','/timelines/','stashskins','/fx/','/mat/','/ao/','/epk/','/graph/','/audio/','/pet/','/clone/','playersummoned','essencemoddaemons','tormentedspirits','/daemon/','bossroomminimapicon','/environment/','hairstyles','/outfits/','/runemarked'];
function isJunk(p){const l=p.toLowerCase();return JUNK_PATTERNS.some(j=>l.includes(j));}

async function loadDb(){$('dbCount').textContent='加载中...';db=await(await fetch('/api/database')).json();$('dbCount').textContent=db.length+' 个实体';filterDb();}
function filterDb(){
  const s=($('dbSearch')?.value||'').toLowerCase();const cats=new Set();const hj=$('dbHideJunk')?.checked;
  const f=db.filter(p=>{if(hj&&isJunk(p))return false;if(s&&!p.toLowerCase().includes(s))return false;const c=getCat(p);cats.add(c);return!dbCatFilter||c===dbCatFilter;});
  $('dbCatFilters').innerHTML=['All',...[...cats].sort()].map(c=>
    `<button class="filter-btn ${dbCatFilter===(c==='All'?'':c)?'active':''}" onclick="setDbCat('${c==='All'?'':c}')">${catLabel(c)}</button>`).join('');
  const show=f.slice(0,200);
  $('dbBody').innerHTML=show.map(p=>{const isW=watched.some(w=>p.includes(w.pattern));
    return`<tr class="${isW?'watched':''}"><td><span class="db-cat">${getCat(p)}</span></td>
    <td class="db-path" title="${p}">${p}</td>
    <td>${isW?`<button class="btn btn-rm" onclick="rmByMeta('${esc(p)}')">-</button>`
             :`<button class="btn btn-add" onclick="dbWatch('${esc(p)}')">关注</button>`}</td></tr>`;
  }).join('')+(f.length>200?`<tr><td colspan=3 style="color:#666">显示 200/${f.length} 条，请缩小搜索范围。</td></tr>`:'');
  $('dbCount').textContent=f.length+' 条匹配';
}
function setDbCat(c){dbCatFilter=c;filterDb();}
function getCat(p){const parts=p.split('/');return parts.length>=2?parts[1]:'?';}
async function dbWatch(path){const def=path.split('/').pop();const nick=prompt('雷达显示昵称：',def);if(nick===null)return;await doAdd(path,nick||def,'#ff5555');filterDb();}

// ── SETTINGS ──
const settingsDef = [
  {section:'显示',items:[
    {key:'showMonsters',label:'显示怪物（全部）',type:'bool'},
    {key:'showRareMonsters',label:'显示稀有怪',type:'bool'},
    {key:'showUniqueMonsters',label:'显示传奇怪',type:'bool'},
    {key:'showNpcs',label:'显示 NPC',type:'bool'},
    {key:'showChests',label:'显示宝箱',type:'bool'},
    {key:'showTransitions',label:'显示区域出口',type:'bool'},
    {key:'showPlayers',label:'显示其他玩家',type:'bool'},
    {key:'showLandmarks',label:'显示地标',type:'bool'},
    {key:'showTerrain',label:'显示地形',type:'bool'},
    {key:'showStatusBar',label:'显示左上状态栏',type:'bool'},
    {key:'showWatchedLabels',label:'显示关注实体标签',type:'bool'},
    {key:'persistEntities',label:'离开同步范围后保留实体',type:'bool'},
  ]},
  {section:'标签开关（隐藏文字但保留点位）',items:[
    {key:'showTransitionLabels',label:'出口标签',type:'bool'},
    {key:'showNpcLabels',label:'NPC 标签',type:'bool'},
    {key:'showLandmarkLabels',label:'地标标签',type:'bool'},
    {key:'showPoiLabels',label:'POI 标签（非 NPC）',type:'bool'},
    {key:'showMonsterLabels',label:'怪物名称（稀有/传奇）',type:'bool'},
    {key:'showChestLabels',label:'宝箱类型标签',type:'bool'},
  ]},
  {section:'减少视觉噪音',items:[
    {key:'hideJunkEntities',label:'隐藏垃圾项（附件、特效、外观）',type:'bool'},
    {key:'hideUntargetable',label:'隐藏不可选中实体',type:'bool'},
    {key:'showDeadMonsters',label:'显示已死亡怪物',type:'bool'},
    {key:'showNormalMonsters',label:'显示普通白怪',type:'bool'},
    {key:'showNormalChests',label:'显示普通宝箱',type:'bool'},
    {key:'showFriendlyEntities',label:'显示友方怪物（召唤物/盟友）',type:'bool'},
    {key:'showImmobileEntities',label:'显示静止实体（速度 0/装饰物）',type:'bool'},
    {key:'entityDrawRange',label:'最大绘制范围（0 = 不限制，网格单位）',type:'num',min:0,max:300,step:5},
    {key:'minEntityHpPct',label:'显示最低生命百分比（0 = 全部）',type:'num',min:0,max:50,step:5},
    {key:'showDistanceRing',label:'显示距离环',type:'bool'},
    {key:'distanceRingRadius',label:'距离环半径（网格单位）',type:'num',min:10,max:200,step:5},
  ]},
  {section:'点位大小',items:[
    {key:'monsterDotSize',label:'普通怪',type:'num',min:1,max:30,step:0.5},
    {key:'magicDotSize',label:'魔法怪',type:'num',min:1,max:30,step:0.5},
    {key:'rareDotSize',label:'稀有怪',type:'num',min:1,max:30,step:0.5},
    {key:'uniqueDotSize',label:'传奇怪',type:'num',min:1,max:30,step:0.5},
    {key:'npcDotSize',label:'NPC',type:'num',min:1,max:30,step:0.5},
    {key:'chestDotSize',label:'宝箱',type:'num',min:1,max:30,step:0.5},
    {key:'transitionDotSize',label:'出口',type:'num',min:1,max:30,step:0.5},
    {key:'playerDotSize',label:'玩家',type:'num',min:1,max:30,step:0.5},
    {key:'watchedDotSize',label:'关注实体',type:'num',min:1,max:30,step:0.5},
  ]},
  {section:'描边',items:[
    {key:'dotOutlineWidth',label:'点位描边宽度',type:'num',min:0,max:10,step:0.5},
    {key:'dotOutlineColor',label:'点位描边颜色',type:'color'},
    {key:'landmarkOutlineWidth',label:'地标描边宽度',type:'num',min:0,max:10,step:0.5},
  ]},
  {section:'字体大小（4K 可放大）',items:[
    {key:'fontFamily',label:'字体',type:'text'},
    {key:'statusFontSize',label:'状态栏',type:'num',min:6,max:72,step:1},
    {key:'landmarkFontSize',label:'地标',type:'num',min:6,max:72,step:1},
    {key:'transitionFontSize',label:'出口',type:'num',min:6,max:72,step:1},
    {key:'chestFontSize',label:'宝箱',type:'num',min:6,max:72,step:1},
    {key:'watchedFontSize',label:'关注标签',type:'num',min:6,max:72,step:1},
    {key:'nameplateFontSize',label:'生命条铭牌',type:'num',min:6,max:72,step:1},
  ]},
  {section:'颜色',items:[
    {key:'monsterColor',label:'普通怪',type:'color'},
    {key:'magicColor',label:'魔法怪',type:'color'},
    {key:'rareColor',label:'稀有怪',type:'color'},
    {key:'uniqueColor',label:'传奇怪',type:'color'},
    {key:'npcColor',label:'NPC',type:'color'},
    {key:'chestColor',label:'宝箱',type:'color'},
    {key:'transitionColor',label:'出口',type:'color'},
    {key:'playerColor',label:'玩家',type:'color'},
    {key:'landmarkColor',label:'地标',type:'color'},
    {key:'watchedColor',label:'关注实体',type:'color'},
  ]},
  {section:'地形 / 地图轮廓',items:[
    {key:'terrainOpacity',label:'Overlay 不透明度',type:'num',min:0,max:1,step:0.05},
    {key:'terrainEdgeColor',label:'边缘颜色',type:'color'},
    {key:'terrainEdgeAlpha',label:'边缘不透明度',type:'num',min:0.1,max:1,step:0.05},
    {key:'terrainInteriorAlpha',label:'内部不透明度',type:'num',min:0,max:0.5,step:0.02},
  ]},
  {section:'性能',items:[
    {key:'fpsCap',label:'FPS 上限（15-360，越低越省 CPU）',type:'num',min:15,max:360,step:5},
  ]},
  {section:'校准',items:[
    {key:'resetCalibrationOnZoneChange',label:'换区自动重置校准',type:'bool'},
    {key:'offsetX',label:'X 偏移',type:'num',min:-500,max:500,step:1},
    {key:'offsetY',label:'Y 偏移',type:'num',min:-500,max:500,step:1},
    {key:'scaleMul',label:'缩放',type:'num',min:0.3,max:3,step:0.02},
  ]},
  {section:'探索迷雾',items:[
    {key:'showExplorationFog',label:'显示未探索迷雾',type:'bool'},
    {key:'fogOpacity',label:'迷雾暗度',type:'num',min:0.1,max:0.9,step:0.05},
    {key:'fogGridStep',label:'迷雾分辨率（越低越清晰但更耗 CPU）',type:'num',min:1,max:8,step:1},
    {key:'fogCellScale',label:'迷雾格大小',type:'num',min:0.05,max:0.3,step:0.01},
  ]},
  {section:'地图绘制',items:[
    {key:'mapCenterYShift',label:'地图中心 Y 偏移',type:'num',min:-300,max:300,step:1},
    {key:'playerBlipSize',label:'玩家点大小（大地图）',type:'num',min:1,max:15,step:0.5},

    {key:'landmarkIconSize',label:'地标图标大小',type:'num',min:1,max:15,step:0.5},
    {key:'pathEndMarkerSize',label:'路线终点标记大小',type:'num',min:1,max:15,step:0.5},
    {key:'clickInspectDistance',label:'点击检查距离（像素）',type:'num',min:10,max:100,step:5},
  ]},
  {section:'首领高亮',items:[
    {key:'showBossHighlight',label:'高亮首领（大星形图标）',type:'bool'},
    {key:'bossDotSize',label:'首领点大小',type:'num',min:3,max:15,step:0.5},
  ]},
  {section:'生命条铭牌',items:[
    {key:'showNameplates',label:'显示生命条',type:'bool'},
    {key:'hpBarNormal',label:'普通怪血条',type:'bool'},
    {key:'hpBarMagic',label:'魔法怪血条',type:'bool'},
    {key:'hpBarRare',label:'稀有怪血条',type:'bool'},
    {key:'hpBarUnique',label:'传奇怪血条',type:'bool'},
    {key:'nameplateBarWidth',label:'条宽比例',type:'num',min:0.3,max:3,step:0.1},
    {key:'nameplateBarHeight',label:'条高（像素）',type:'num',min:1,max:20,step:1},
    {key:'nameplateOffsetY',label:'Y 偏移（负数为上方）',type:'num',min:-100,max:50,step:1},
    {key:'nameplateFontSize',label:'名称字体大小',type:'num',min:6,max:30,step:1},
  ]},
  {section:'路线导航',items:[
    {key:'showPath',label:'启用路线导航',type:'bool'},
    {key:'showGroundWaypoints',label:'地图关闭时显示地面路线点',type:'bool'},
    {key:'pathTarget',label:'目标匹配片段',type:'text'},
    {key:'pathColor',label:'路线颜色',type:'color'},
    {key:'pathWidth',label:'路线宽度',type:'num',min:0.5,max:8,step:0.5},
    {key:'pathMaxNodes',label:'最大搜索节点（越高路径越远但更耗 CPU）',type:'num',min:50000,max:2000000,step:50000},
  ]},
  {section:'自动药剂',items:[
    {key:'hpThreshold',label:'生命阈值 %',type:'num',min:5,max:95,step:5},
    {key:'manaThreshold',label:'魔力阈值 %',type:'num',min:5,max:95,step:5},
    {key:'flaskLifeKey',label:'生命药剂按键（VK 码，0x31=1）',type:'num',min:0,max:255,step:1},
    {key:'flaskManaKey',label:'魔力药剂按键（VK 码，0x32=2）',type:'num',min:0,max:255,step:1},
    {key:'flaskLifeCooldownMs',label:'生命药剂冷却（毫秒）',type:'num',min:500,max:10000,step:100},
    {key:'flaskManaCooldownMs',label:'魔力药剂冷却（毫秒）',type:'num',min:500,max:10000,step:100},
  ]},
  {section:'自动退出（低血量强制关闭游戏）',items:[
    {key:'autoLogoutEnabled',label:'启用自动退出',type:'bool'},
    {key:'autoLogoutHpThreshold',label:'生命阈值 %（低于或等于时退出）',type:'num',min:5,max:80,step:5},
  ]},
  {section:'Atlas',items:[
    {key:'atlasOverlayEnabled',label:'启用 Atlas 悬浮层',type:'bool'},
    {key:'atlasDrawAll',label:'绘制全部 Atlas 节点',type:'bool'},
    {key:'atlasHighlightTags',label:'高亮标签（逗号分隔）',type:'csv'},
    {key:'atlasArrowTags',label:'箭头指向标签（逗号分隔）',type:'csv'},
    {key:'atlasHighlightColors',label:'标签颜色（格式：标签=#RRGGBB, 标签2=#RRGGBB）',type:'kvpairs'},
  ]},
];

async function loadSettings(){
  settings=await(await fetch('/api/settings')).json();
  let html='';
  for(let si=0; si<settingsDef.length; si++){
    const sec=settingsDef[si];
    const collapsed=sec.section.startsWith('游戏调试')||sec.section==='开发测试';
    const sid='sec_'+si;
    html+=`<div class="section"><h3 style="cursor:pointer;user-select:none" onclick="document.getElementById('${sid}').style.display=document.getElementById('${sid}').style.display==='none'?'':'none'">${collapsed?'▶':'▼'} ${sec.section}</h3><div id="${sid}" style="${collapsed?'display:none':''}">`;
    for(const item of sec.items){
      const v=settings[item.key]??'';
      html+=`<div class="setting-row"><label>${item.label}</label>`;
      if(item.type==='bool')
        html+=`<input type="checkbox" ${v?'checked':''} onchange="setSetting('${item.key}',this.checked)">`;
      else if(item.type==='color')
        html+=`<input type="color" value="${v}" onchange="setSetting('${item.key}',this.value)">`;
      else if(item.type==='text')
        html+=`<input type="text" value="${v||''}" style="width:250px" onchange="setSetting('${item.key}',this.value)" placeholder="如 AreaTransition, Waypoint">`;
      else if(item.type==='csv')
        html+=`<input type="text" value="${Array.isArray(v)?v.join(', '):v||''}" style="width:320px" onchange="setSetting('${item.key}',this.value.split(',').map(x=>x.trim()).filter(Boolean))" placeholder="如 Breach, Ritual, Boss">`;
      else if(item.type==='kvpairs')
        html+=`<input type="text" value="${v&&typeof v==='object'?Object.entries(v).map(([k,val])=>k+'='+val).join(', '):''}" style="width:420px" onchange="setSetting('${item.key}',Object.fromEntries(this.value.split(',').map(x=>x.trim()).filter(Boolean).map(x=>{const i=x.indexOf('=');return i>0?[x.slice(0,i).trim(),x.slice(i+1).trim()]:[x,'#ff5555']})))" placeholder="如 Breach=#a64dff, Ritual=#ff3355">`;
      else if(item.type==='num')
        html+=`<input type="range" min="${item.min}" max="${item.max}" step="${item.step}" value="${v}"
          oninput="setSetting('${item.key}',parseFloat(this.value));this.nextElementSibling.textContent=this.value">
          <span class="val">${v}</span>`;
      html+=`</div>`;
    }
    html+=`</div></div>`;
  }
  $('settingsBody').innerHTML=html;
}
function setSetting(key,val){settings[key]=val;}
async function saveSettings(){
  await fetch('/api/settings',{method:'POST',headers:{'Content-Type':'application/json'},body:JSON.stringify(settings)});
  $('savedMsg').classList.add('show');setTimeout(()=>$('savedMsg').classList.remove('show'),1500);
}
function exportSettings(){
  const blob=new Blob([JSON.stringify(settings,null,2)],{type:'application/json'});
  const a=document.createElement('a');a.href=URL.createObjectURL(blob);a.download='radar_settings.json';a.click();
}
async function importSettings(e){
  const file=e.target.files[0];if(!file)return;
  const text=await file.text();
  try{
    const imported=JSON.parse(text);
    await fetch('/api/settings',{method:'POST',headers:{'Content-Type':'application/json'},body:JSON.stringify(imported)});
    await loadSettings();
    $('savedMsg').textContent='已导入！';$('savedMsg').classList.add('show');setTimeout(()=>{$('savedMsg').classList.remove('show');$('savedMsg').textContent='已保存！';},2000);
  }catch(ex){alert('设置文件无效：'+ex.message)}
  e.target.value='';
}
async function resetSettings(){
  if(!confirm('确认把所有设置重置为默认值？此操作不可撤销。'))return;
  await fetch('/api/settings/reset',{method:'POST'});
  await loadSettings();
  $('savedMsg').textContent='已重置！';$('savedMsg').classList.add('show');setTimeout(()=>{$('savedMsg').classList.remove('show');$('savedMsg').textContent='已保存！';},2000);
}

// ── AUTO-SKILLS ──
const VK_NAMES={0x31:'1',0x32:'2',0x33:'3',0x34:'4',0x35:'5',0x51:'Q',0x57:'W',0x45:'E',0x52:'R',0x54:'T'};
function vkName(k){return VK_NAMES[k]||`0x${k.toString(16).toUpperCase()}`;}
function parseKey(s){
  s=s.trim().toUpperCase();
  for(const[k,v]of Object.entries(VK_NAMES))if(v===s)return parseInt(k);
  if(s.length===1)return s.charCodeAt(0);
  if(s.startsWith('0X'))return parseInt(s,16);
  return 0x51;
}

async function refreshRules(){
  const data=await(await fetch('/api/rules')).json();
  $('rulesToggle').textContent=data.enabled?'已开启':'已关闭';
  $('rulesToggle').style.background=data.enabled?'#2a5a2a':'#5a2a2a';
  $('rulesList').innerHTML=data.rules.map((r,i)=>
    `<div class="watched-item" style="flex-wrap:wrap">
      <label><input type="checkbox" ${r.enabled?'checked':''} onchange="updateRule(${i},{enabled:this.checked})"></label>
      <input type="text" value="${r.name}" style="width:90px;background:#1e1e28;border:1px solid #444;color:#afc;border-radius:3px;padding:2px 6px;font-size:13px;font-weight:bold"
        onchange="updateRule(${i},{name:this.value})">
      <span style="color:#78b4ff;font-size:12px">按键：${vkName(r.key)}</span>
      <span style="color:#aaa;font-size:11px">冷却：${r.cooldownSec}s</span>
      ${r.hpBelow?`<span style="color:#f88;font-size:11px">生命&lt;${r.hpBelow}%</span>`:''}
      ${r.hpAbove?`<span style="color:#f88;font-size:11px">生命&gt;${r.hpAbove}%</span>`:''}
      ${r.manaBelow?`<span style="color:#88f;font-size:11px">魔力&lt;${r.manaBelow}%</span>`:''}
      ${r.esBelow?`<span style="color:#8ff;font-size:11px">能盾&lt;${r.esBelow}%</span>`:''}
      ${r.enemiesNearby?`<span style="color:#ff8;font-size:11px">敌人&ge;${r.enemiesNearby}</span>`:''}
      ${r.bossNearby===true?'<span style="color:#f80;font-size:11px">附近有首领</span>':''}
      ${r.bossNearby===false?'<span style="color:#888;font-size:11px">附近无首领</span>':''}
      ${r.waitSec?`<span style="color:#8f8;font-size:11px">等待 ${r.waitSec}s</span>`:''}
      ${r.requireKeyHeld?`<span style="color:#c8f;font-size:11px">按住：${vkName(r.requireKeyHeld)}</span>`:''}
      ${r.playerMoving===true?'<span style="color:#aaf;font-size:11px">移动中</span>':''}
      ${r.playerMoving===false?'<span style="color:#aaf;font-size:11px">站定</span>':''}
      <button class="btn btn-rm" style="margin-left:auto" onclick="deleteRule(${i})">X</button>
    </div>`
  ).join('')||'<div style="color:#666;padding:8px">还没有规则，请在下方添加。</div>';
}
async function toggleRules(){await fetch('/api/rules/toggle');refreshRules();}
async function updateRule(i,changes){
  const data=await(await fetch('/api/rules')).json();
  const rule={...data.rules[i],...changes};
  await fetch('/api/rules',{method:'PUT',headers:{'Content-Type':'application/json'},body:JSON.stringify({index:i,rule})});
  refreshRules();
}
async function deleteRule(i){await fetch('/api/rules?index='+i,{method:'DELETE'});refreshRules();}
async function addRule(){
  const name=$('ruleAddName').value||'技能';
  const key=parseKey($('ruleAddKey').value||'Q');
  const cd=parseFloat($('ruleAddCd').value)||2;
  const hp=$('ruleAddHp').value?parseFloat($('ruleAddHp').value):null;
  const mana=$('ruleAddMana').value?parseFloat($('ruleAddMana').value):null;
  const es=$('ruleAddEs').value?parseFloat($('ruleAddEs').value):null;
  const enemies=$('ruleAddEnemies').value?parseInt($('ruleAddEnemies').value):null;
  const bossVal=$('ruleAddBoss').value;
  const boss=bossVal==='true'?true:bossVal==='false'?false:null;
  const wait=$('ruleAddWait').value?parseFloat($('ruleAddWait').value):null;
  const holdKeyStr=$('ruleAddHoldKey').value;
  const holdKey=holdKeyStr?parseKey(holdKeyStr):null;
  const movingVal=$('ruleAddMoving').value;
  const moving=movingVal==='true'?true:movingVal==='false'?false:null;
  await fetch('/api/rules',{method:'POST',headers:{'Content-Type':'application/json'},
    body:JSON.stringify({name,key,enabled:true,cooldownSec:cd,hpBelow:hp,manaBelow:mana,esBelow:es,enemiesNearby:enemies,bossNearby:boss,waitSec:wait,requireKeyHeld:holdKey,playerMoving:moving})});
  $('ruleAddName').value='';$('ruleAddKey').value='';$('ruleAddHoldKey').value='';refreshRules();
}

// ── PATHING ──
async function refreshPathing(){
  const data=await(await fetch('/api/pathing')).json();
  const targets=data.targets, cur=data.current;
  $('pathingList').innerHTML=targets.map((t,i)=>
    `<div class="watched-item" style="${i===cur?'border-left:3px solid #5cf':'border-left:3px solid transparent'}">
      <label style="font-size:11px;color:#888;white-space:nowrap"><input type="checkbox" ${t.enabled?'checked':''} onchange="editPathTarget('${esc(t.pattern)}',{enabled:this.checked})"> </label>
      <input type="text" value="${t.label}" style="width:120px;background:#1e1e28;border:1px solid #444;color:#afc;border-radius:3px;padding:2px 6px;font-size:13px;font-weight:bold"
        onchange="editPathTarget('${esc(t.pattern)}',{label:this.value})">
      <div class="pattern" title="${t.pattern}">${t.pattern}</div>
      ${i===cur?'<span style="color:#5cf;font-size:11px;font-weight:bold">当前</span>':''}
      <button class="btn btn-rm" onclick="rmPathTarget('${esc(t.pattern)}')">X</button>
    </div>`
  ).join('')||'<div style="color:#666;padding:8px">还没有路线目标，请在上方添加匹配片段。</div>';
}
async function addPathTarget(){
  const p=$('pathAddPattern').value.trim(),l=$('pathAddLabel').value.trim();
  if(!p)return;
  await fetch('/api/pathing',{method:'POST',headers:{'Content-Type':'application/json'},body:JSON.stringify({pattern:p,label:l||p,enabled:true})});
  $('pathAddPattern').value='';$('pathAddLabel').value='';refreshPathing();
}
async function editPathTarget(pattern,changes){
  await fetch('/api/pathing',{method:'PUT',headers:{'Content-Type':'application/json'},body:JSON.stringify({pattern,...changes})});
  refreshPathing();
}
async function rmPathTarget(pattern){
  await fetch('/api/pathing?pattern='+encodeURIComponent(pattern),{method:'DELETE'});refreshPathing();
}
async function cyclePathTarget(){
  await fetch('/api/pathing/cycle');refreshPathing();
}

// ── LANDMARKS ──
async function refreshLandmarks(){landmarks=await(await fetch('/landmarks')).json();filterLandmarks();}
function filterLandmarks(){
  const s=($('lmSearch')?.value||'').toLowerCase();
  $('lmBody').innerHTML=landmarks.filter(l=>!s||l.name.toLowerCase().includes(s)||l.path.toLowerCase().includes(s))
    .map(l=>`<tr><td>${l.name}</td><td class="meta-short" title="${l.path}">${l.path}</td><td>${l.tiles}</td><td>${l.dist}</td>
      <td><button class="btn" style="background:#4a3a1a;color:#fa0" onclick="hideFromLandmark('${esc(l.name)}','${esc(l.path)}')">隐藏</button></td></tr>`).join('');
}

// ── DevTest ──
const devtestDef = [
  {section:'渲染（写入游戏内存）',items:[
    {key:'tweakHideNormalLifeBars',label:'隐藏普通怪血条',type:'bool'},
    {key:'tweakHideMagicLifeBars',label:'隐藏普通+魔法怪血条',type:'bool'},
    {key:'tweakHideAllLifeBars',label:'隐藏全部怪物血条',type:'bool'},
    {key:'tweakHideBuffVisuals',label:'隐藏 Buff/Debuff 特效',type:'bool'},
    {key:'tweakHideNormalRendering',label:'让普通怪不可见',type:'bool'},
    {key:'tweakForceShowHover',label:'强制悬停高亮',type:'bool'},
    {key:'tweakDisableSelectionBoxes',label:'禁用选择框',type:'bool'},
    {key:'tweakHideInfoDisplay',label:'隐藏信息提示',type:'bool'},
    {key:'tweakHideTalismanIcons',label:'隐藏头顶图标',type:'bool'},
    {key:'tweakForceOutline',label:'强制轮廓发光',type:'bool'},
  ]},
  {section:'物理与碰撞',items:[
    {key:'tweakDisableMonsterBlocking',label:'禁用怪物阻挡',type:'bool'},
    {key:'tweakDisableMonsterPush',label:'禁用推挤',type:'bool'},
    {key:'tweakEnablePhaseThrough',label:'穿过地形',type:'bool'},
  ]},
  {section:'目标选择',items:[
    {key:'tweakForceAllTargetable',label:'强制全部可选中',type:'bool'},
    {key:'tweakForceAllAttackable',label:'强制全部可攻击',type:'bool'},
  ]},
  {section:'行为',items:[
    {key:'tweakFreezeNormalMonsters',label:'冻结普通怪（速度 0）',type:'bool'},
    {key:'tweakPreventCorpseSinking',label:'阻止尸体下沉',type:'bool'},
  ]},
  {section:'世界交互',items:[
    {key:'tweakInstantTransitions',label:'瞬间切换区域',type:'bool'},
    {key:'tweakUnblockDoors',label:'解除门阻挡',type:'bool'},
    {key:'tweakUnlockChests',label:'解锁宝箱',type:'bool'},
    {key:'tweakOpenChestsOnDamage',label:'受击时打开宝箱',type:'bool'},
  ]},
  {section:'实体修改',items:[
    {key:'tweakSwapTeamToFriendly',label:'让怪物变为友方',type:'bool'},
    {key:'tweakMakeAllBoss',label:'全部标记为首领',type:'bool'},
    {key:'tweakRemoveBossFlag',label:'移除首领标记',type:'bool'},
    {key:'tweakEntityScale',label:'缩放覆盖（0=关闭）',type:'num',min:0,max:5,step:0.1},
    {key:'tweakEntityColorR',label:'染色 R（-1=关闭）',type:'num',min:-1,max:2,step:0.1},
    {key:'tweakEntityColorG',label:'染色 G（-1=关闭）',type:'num',min:-1,max:2,step:0.1},
    {key:'tweakEntityColorB',label:'染色 B（-1=关闭）',type:'num',min:-1,max:2,step:0.1},
    {key:'tweakLabelViewDistance',label:'标签显示距离（0=关闭）',type:'num',min:0,max:500,step:10},
  ]},
  {section:'作用范围',items:[
    {key:'tweakApplyToNpcs',label:'应用到 NPC',type:'bool'},
    {key:'tweakApplyToChests',label:'应用到宝箱',type:'bool'},
  ]},
  {section:'原始字段写入（高度实验）',items:[
    {key:'tweakDevHideHover',label:'隐藏悬停',type:'bool'},
    {key:'tweakDevFadeArrows',label:'淡化箭头',type:'bool'},
    {key:'tweakDevDisableLight',label:'禁用光照',type:'bool'},
    {key:'tweakDevFixedSelectionSize',label:'固定选择尺寸',type:'bool'},
    {key:'tweakDevBBoxIgnoreGround',label:'碰撞盒忽略地面',type:'bool'},
    {key:'tweakDevFaceWindDirection',label:'面向风向',type:'bool'},
    {key:'tweakDevDampenHeight',label:'降低高度变化',type:'bool'},
    {key:'tweakDevHeightOffset',label:'高度偏移',type:'num',min:-200,max:200,step:5},
    {key:'tweakDevSelectionHeightOverride',label:'选择高度覆盖',type:'num',min:0,max:500,step:10},
    {key:'tweakDevLockOrientation',label:'锁定朝向',type:'bool'},
    {key:'tweakDevMakeFlying',label:'设为飞行',type:'bool'},
    {key:'tweakDevMakeStatic',label:'设为静态',type:'bool'},
    {key:'tweakDevFaceMovementDir',label:'面向移动方向',type:'bool'},
    {key:'tweakDevAvoidOthers',label:'禁用避让',type:'bool'},
    {key:'tweakDevLockAnimation',label:'锁定动画',type:'bool'},
    {key:'tweakDevCorpseUsable',label:'尸体可交互',type:'bool'},
    {key:'tweakDevNoCorpseMarker',label:'无尸体标记',type:'bool'},
  ]},
];
function loadDevTest(){
  let html='';
  for(const sec of devtestDef){
    html+=`<div class="section"><h3 style="color:#f88">${sec.section}</h3>`;
    for(const item of sec.items){
      const v=settings[item.key]??'';
      html+=`<div class="setting-row"><label>${item.label}</label>`;
      if(item.type==='bool')
        html+=`<input type="checkbox" ${v?'checked':''} onchange="setSetting('${item.key}',this.checked)">`;
      else if(item.type==='num')
        html+=`<input type="range" min="${item.min}" max="${item.max}" step="${item.step}" value="${v}"
          oninput="setSetting('${item.key}',parseFloat(this.value));this.nextElementSibling.textContent=this.value">
          <span class="val">${v}</span>`;
      html+=`</div>`;
    }
    html+=`</div>`;
  }
  $('devtestBody').innerHTML=html;
}

// ── Game Data ──
let gdData=[], gdMode='areas';
async function loadGdAreas(){
  gdMode='areas';
  const s=$('gdSearch')?.value||'';
  gdData=await(await fetch(`/api/gamedata/areas?search=${encodeURIComponent(s)}`)).json();
  renderGd();
}
async function loadGdBuffs(){
  gdMode='buffs';
  const s=$('gdSearch')?.value||'';
  gdData=await(await fetch(`/api/gamedata/buffs?search=${encodeURIComponent(s)}&limit=500`)).json();
  renderGd();
}
async function loadGdPins(){
  gdMode='pins';
  const data=await(await fetch('/api/gamedata/pins')).json();
  gdData=data.pins||[];
  $('gdResults').innerHTML=`<h3 style="color:#0af">${data.area} - ${gdData.length} 个标记</h3>`+
    (gdData.length?`<table style="width:100%"><thead><tr><th>名称</th><th>ID</th><th>类型</th></tr></thead><tbody>`+
    gdData.map(p=>`<tr><td>${p.name}</td><td style="color:#888">${p.id}</td><td>${p.type}</td></tr>`).join('')+
    '</tbody></table>':'<p style="color:#888">当前区域没有地图标记</p>');
}
function searchGameData(){
  if(gdMode==='areas')loadGdAreas();
  else if(gdMode==='buffs')loadGdBuffs();
}
function renderGd(){
  if(gdMode==='areas'){
    $('gdResults').innerHTML=`<p style="color:#888">${gdData.length} 个区域</p><table style="width:100%"><thead><tr><th>代码</th><th>名称</th><th>章节</th><th>等级</th><th>城镇</th><th>传送点</th></tr></thead><tbody>`+
      gdData.map(a=>`<tr><td style="color:#0af">${a.code}</td><td>${a.name}</td><td>${a.act}</td><td>${a.level}</td><td>${a.town?'是':''}</td><td>${a.waypoint?'是':''}</td></tr>`).join('')+
      '</tbody></table>';
  } else if(gdMode==='buffs'){
    $('gdResults').innerHTML=`<p style="color:#888">${gdData.length} 个增益</p><table style="width:100%"><thead><tr><th>ID</th><th>名称</th><th>描述</th></tr></thead><tbody>`+
      gdData.map(b=>`<tr><td style="color:#888;font-size:10px">${b.id}</td><td style="color:#0af">${b.name}</td><td style="font-size:11px">${b.description||''}</td></tr>`).join('')+
      '</tbody></table>';
  }
}

// ── MINIMAP ──
const minimapDef = [
  {section:'通用',items:[
    {key:'showMinimap',label:'大地图关闭时显示小地图',type:'bool'},
    {key:'minimapSize',label:'大小（像素）',type:'num',min:100,max:500,step:10},
    {key:'minimapScale',label:'缩放',type:'num',min:0.1,max:2,step:0.05},
    {key:'minimapOpacity',label:'不透明度',type:'num',min:0.2,max:1,step:0.05},
    {key:'minimapPosition',label:'位置（topleft/topright/bottomleft/bottomright）',type:'text'},
    {key:'minimapOffsetX',label:'X 偏移（像素）',type:'num',min:-2000,max:2000,step:5},
    {key:'minimapOffsetY',label:'Y 偏移（像素）',type:'num',min:-2000,max:2000,step:5},
    {key:'minimapPlayerBlipSize',label:'玩家点大小',type:'num',min:1,max:10,step:0.5},
    {key:'minimapDotScale',label:'点位整体缩放',type:'num',min:0.3,max:3,step:0.1},
  ]},
  {section:'实体点位显示',items:[
    {key:'minimapShowMonsters',label:'显示怪物',type:'bool'},
    {key:'minimapShowBosses',label:'显示首领（大点）',type:'bool'},
    {key:'minimapShowNpcs',label:'显示 NPC',type:'bool'},
    {key:'minimapShowChests',label:'显示宝箱',type:'bool'},
    {key:'minimapShowTransitions',label:'显示出口',type:'bool'},
    {key:'minimapShowTerrain',label:'显示地形',type:'bool'},
    {key:'minimapShowPath',label:'显示路线',type:'bool'},
  ]},
  {section:'标签显示',items:[
    {key:'minimapLabelBoss',label:'标注首领',type:'bool'},
    {key:'minimapLabelUnique',label:'标注传奇怪',type:'bool'},
    {key:'minimapLabelTransition',label:'标注出口',type:'bool'},
    {key:'minimapLabelNpc',label:'标注 POI/NPC',type:'bool'},
    {key:'minimapLabelWatched',label:'标注关注实体',type:'bool'},
    {key:'minimapLabelFontSize',label:'标签字体大小',type:'num',min:6,max:36,step:1},
  ]},
];
async function loadMinimapSettings(){
  if(!settings||!Object.keys(settings).length) settings=await(await fetch('/api/settings')).json();
  let html='';
  for(const sec of minimapDef){
    html+=`<div class="section"><h3>${sec.section}</h3>`;
    for(const item of sec.items){
      const v=settings[item.key]??'';
      html+=`<div class="setting-row"><label>${item.label}</label>`;
      if(item.type==='bool')
        html+=`<input type="checkbox" ${v?'checked':''} onchange="setSetting('${item.key}',this.checked)">`;
      else if(item.type==='color')
        html+=`<input type="color" value="${v}" onchange="setSetting('${item.key}',this.value)">`;
      else if(item.type==='text')
        html+=`<input type="text" value="${v||''}" style="width:250px" onchange="setSetting('${item.key}',this.value)">`;
      else if(item.type==='num')
        html+=`<input type="range" min="${item.min}" max="${item.max}" step="${item.step}" value="${v}"
          oninput="setSetting('${item.key}',parseFloat(this.value));this.nextElementSibling.textContent=this.value">
          <span class="val">${v}</span>`;
      html+=`</div>`;
    }
    html+=`</div>`;
  }
  $('minimapSettingsBody').innerHTML=html;
}

// ── HIDDEN ENTITIES ──
let hiddenPatterns=[];
async function refreshHidden(){
  hiddenPatterns=await(await fetch('/api/hidden')).json();
  $('hiddenList').innerHTML=hiddenPatterns.map(p=>
    `<div class="watched-item">
      <span style="font-family:monospace;font-size:13px;color:#fa0;flex:1">${p}</span>
      <button class="btn btn-rm" onclick="removeHidden('${esc(p)}')">X</button>
    </div>`
  ).join('')||'<div style="color:#666;padding:8px">还没有隐藏规则，雷达会显示所有内容。</div>';
}
async function addHidden(){
  const p=$('hiddenAddPattern').value.trim();
  if(!p)return;
  await fetch('/api/hidden',{method:'POST',headers:{'Content-Type':'application/json'},body:JSON.stringify({pattern:p})});
  $('hiddenAddPattern').value='';
  refreshHidden();refresh();
}
async function removeHidden(pattern){
  await fetch('/api/hidden?pattern='+encodeURIComponent(pattern),{method:'DELETE'});
  refreshHidden();refresh();
}
function hideFromEntity(meta){
  const parts=meta.split('/');
  const short=parts[parts.length-1].replace(/@\d+$/,'');
  const pattern=prompt('隐藏匹配规则（匹配的实体会从雷达中隐藏）：',short);
  if(pattern===null||!pattern.trim())return;
  fetch('/api/hidden',{method:'POST',headers:{'Content-Type':'application/json'},body:JSON.stringify({pattern:pattern.trim()})})
    .then(()=>{refresh();});
}
function hideFromLandmark(name,path){
  const pattern=prompt('隐藏匹配规则（匹配的地标会从雷达中隐藏）：',name);
  if(pattern===null||!pattern.trim())return;
  fetch('/api/hidden',{method:'POST',headers:{'Content-Type':'application/json'},body:JSON.stringify({pattern:pattern.trim()})})
    .then(()=>{refreshLandmarks();});
}

function inspectFromList(addr){
  showTab('inspector');
  setTimeout(()=>{
    $('inspEntity').value=addr;
    inspSelectedAddr=addr;
    inspectEntity();
  },100);
}

// ── Inspector ──
let inspEntities=[], inspTimer=null, inspSelectedAddr='';
async function loadInspectorEntities(){
  try{
    const data=await(await fetch('/entities?limit=999')).json();
    inspEntities=data;
    const sel=$('inspEntity');
    sel.innerHTML='<option value="">-- 选择实体 --</option>'+
      data.map(e=>`<option value="${e.addr}">[${catLabel(e.category)}] ${e.name||e.metadata.split('/').pop()} (${e.addr})${e.boss?' ★':''}${e.locked?' 🔒':''}</option>`).join('');
    $('inspStatus').textContent=`${data.length} 个实体`;
  }catch(ex){$('inspStatus').textContent='错误：'+ex.message}
}
let inspSchema={};
async function loadInspectorSchema(){
  try{
    const comps=await(await fetch('/api/inspect/components')).json();
    if(Array.isArray(comps)){
      for(const c of comps){
        const s=await(await fetch(`/api/inspect/schema?component=${c.name}`)).json();
        if(s.fields) inspSchema[c.name]={};
        for(const f of (s.fields||[])) inspSchema[c.name][f.name]={offset:f.offset,type:f.type,verified:f.verified,notes:f.notes};
      }
    }
  }catch{}
}
async function inspectEntity(){
  const addr=$('inspEntity').value;
  if(!addr){$('inspResults').innerHTML='';$('inspComponents').innerHTML='';return}
  inspSelectedAddr=addr;
  try{
    const data=await(await fetch(`/api/inspect?entity=${addr}`)).json();
    if(data.error){$('inspResults').innerHTML=`<p style="color:#f66">${data.error}</p>`;return}
    const comps=Object.keys(data.components||{});
    const compCount=comps.length;
    const fieldCount=comps.reduce((s,c)=>s+Object.keys(data.components[c]||{}).length,0);
    $('inspComponents').innerHTML=`<span style="color:#888;font-size:11px">${compCount} 个组件，${fieldCount} 个字段</span> `+
      comps.map(c=>`<span style="background:#333;padding:2px 8px;border-radius:4px;cursor:pointer;font-size:11px" onclick="scrollToComp('${c}')">${c}</span>`).join('');
    let html='';
    for(const[name,fields]of Object.entries(data.components||{})){
      const fc=Object.keys(fields||{}).length;
      html+=`<h3 id="insp-${name}" style="margin:12px 0 4px;color:#0af">${name} <span style="color:#666;font-size:11px">(${fc} 个字段)</span></h3>`;
      const schema=inspSchema[name]||{};
      html+='<table style="width:100%"><thead><tr><th style="text-align:left">字段</th><th>偏移</th><th>类型</th><th>值</th></tr></thead><tbody>';
      for(const[fn,fv]of Object.entries(fields||{})){
        const s=schema[fn]||{};
        const nonZero=fv!==null&&fv!==0&&fv!==false;
        const style=nonZero?'color:#eee':'color:#555';
        const val=fv===null?'<span style="color:#444">null</span>':typeof fv==='object'?`<span style="color:#8af">${JSON.stringify(fv)}</span>`:`<b style="${style}">${fv}</b>`;
        const verified=s.verified?'<span style="color:#4f4" title="已验证">&#10003;</span>':'';
        const notes=s.notes?` <span style="color:#666;font-size:10px" title="${s.notes}">[?]</span>`:'';
        html+=`<tr><td style="color:#aaa">${fn}${verified}${notes}</td><td style="color:#666;font-size:10px">${s.offset||''}</td><td style="color:#666;font-size:10px">${s.type||''}</td><td>${val}</td></tr>`;
      }
      html+='</tbody></table>';
    }
    $('inspResults').innerHTML=html||'<p style="color:#888">没有解析到组件（实体可能已离开范围）</p>';
  }catch(ex){$('inspResults').innerHTML=`<p style="color:#f66">${ex.message}</p>`}
}
function scrollToComp(name){const el=document.getElementById('insp-'+name);if(el)el.scrollIntoView({behavior:'smooth'})}
function inspAutoTick(){
  if($('inspAutoRefresh')?.checked && inspSelectedAddr && document.getElementById('tab-inspector')?.classList.contains('active'))
    inspectEntity();
}

// ── KEYBINDS ──
const keybindsDef=[
  {key:'keyCheat1',label:'补丁：Atlas 去雾',def:0x70},
  {key:'keyCheat2',label:'补丁：揭示地图',def:0x71},
  {key:'keyCheat3',label:'补丁：无限缩放',def:0x72},
  {key:'keyCheat4',label:'补丁：敌人血条',def:0x73},
  {key:'keyCheat5',label:'补丁：玩家光照',def:0x74},
  {key:'keyCycleLandmarks',label:'切换地标导航目标',def:0x75},
  {key:'keyCycleEntities',label:'切换实体导航目标',def:0x76},
  {key:'keyAutoFlask',label:'开关自动药剂',def:0x77},
  {key:'keySettings',label:'打开设置面板',def:0x78},
  {key:'keyToggleOverlay',label:'显示/隐藏悬浮层',def:0x79},
  {key:'keyDashboard',label:'打开网页控制台',def:0x7A},
];
const VK_DISPLAY={0x70:'F1',0x71:'F2',0x72:'F3',0x73:'F4',0x74:'F5',0x75:'F6',0x76:'F7',0x77:'F8',0x78:'F9',0x79:'F10',0x7A:'F11',0x7B:'F12',
  0x31:'1',0x32:'2',0x33:'3',0x34:'4',0x35:'5',0x36:'6',0x37:'7',0x38:'8',0x39:'9',0x30:'0',
  0x41:'A',0x42:'B',0x43:'C',0x44:'D',0x45:'E',0x46:'F',0x47:'G',0x48:'H',0x49:'I',0x4A:'J',
  0x4B:'K',0x4C:'L',0x4D:'M',0x4E:'N',0x4F:'O',0x50:'P',0x51:'Q',0x52:'R',0x53:'S',0x54:'T',
  0x55:'U',0x56:'V',0x57:'W',0x58:'X',0x59:'Y',0x5A:'Z',
  0x60:'Num0',0x61:'Num1',0x62:'Num2',0x63:'Num3',0x64:'Num4',0x65:'Num5',0x66:'Num6',0x67:'Num7',0x68:'Num8',0x69:'Num9',
  0x6A:'Num*',0x6B:'Num+',0x6D:'Num-',0x6E:'Num.',0x6F:'Num/',
  0xBE:'.',0xBC:',',0xBA:';',0xBF:'/',0xC0:'`',0xDB:'[',0xDD:']',0xDC:'\\\\',0xDE:"'",0xBD:'-',0xBB:'='};
function vkDisplay(code){return VK_DISPLAY[code]||('0x'+code.toString(16).toUpperCase());}
function loadKeybinds(){
  if(!settings||!Object.keys(settings).length)return;
  let html='';
  const seen={};
  for(const kb of keybindsDef){
    const vk=settings[kb.key]??kb.def;
    if(seen[vk])seen[vk].push(kb.key);else seen[vk]=[kb.key];
  }
  for(const kb of keybindsDef){
    const vk=settings[kb.key]??kb.def;
    const dup=seen[vk]&&seen[vk].length>1;
    html+=`<div class="setting-row">
      <label>${kb.label}</label>
      <input type="text" readonly value="${vkDisplay(vk)}" id="kb_${kb.key}"
        style="width:80px;text-align:center;cursor:pointer;background:#1e1e28;border:1px solid ${dup?'#f55':'#555'};color:#78b4ff;font-weight:bold"
        onfocus="this.value='... 按下新按键 ...';this.style.borderColor='#78b4ff'"
        onkeydown="captureKey(event,'${kb.key}');return false"
        onblur="this.value=vkDisplay(settings['${kb.key}']??${kb.def});this.style.borderColor='#555'">
      <span style="color:#666;font-size:11px;margin-left:8px">VK: 0x${vk.toString(16).toUpperCase()}</span>
      ${dup?'<span style="color:#f55;font-size:11px;margin-left:4px">重复！</span>':''}
    </div>`;
  }
  $('keybindsBody').innerHTML=html;
}
function captureKey(e,settingKey){
  e.preventDefault();e.stopPropagation();
  const vk=e.keyCode;
  settings[settingKey]=vk;
  const el=$('kb_'+settingKey);
  el.value=vkDisplay(vk);
  el.style.borderColor='#5f5';
  setTimeout(()=>{el.style.borderColor='#555';el.blur();loadKeybinds();},300);
}
async function saveKeybinds(){
  await fetch('/api/settings',{method:'POST',headers:{'Content-Type':'application/json'},body:JSON.stringify(settings)});
  $('kbSavedMsg').classList.add('show');setTimeout(()=>$('kbSavedMsg').classList.remove('show'),1500);
}

refresh();refreshWatched();setInterval(refresh,2000);setInterval(inspAutoTick,2000);
</script></body></html>
""";
}
