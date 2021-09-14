# 中国式家长 移除约会警告 Mod

### 功能：
此 Mod 能够移除约会次数过多时家长发出的警告以达到不限次数约会效果。  
此 Mod 未对约会消耗精力和精力总量进行修改，精力不足时仍会无法约会。

### 注意事项：
此 Mod 基于 Unity Mod Manager，需要按照 Unity Mod Manager 的方式使用。  
此 Mod 使用的 Unity Mod Manager 版本是 0.23.5.0，不保证对其他版本的兼容性。  
此 Mod 的修改需要按照 Unity Mod Manager 所指定的方式进行。  
Unity Mod Manager 0.23.5.0 本身不支持中国式家长，需要在 UnityModManagerConfig.xml 文件中添加以下内容：  
```XML
	<!-- 中国式家长 -->
	<GameInfo Name="Chinese Parents(中国式家长)">
		<!-- 文件夹名称 -->
		<Folder>ChineseParents</Folder>
		<!-- Mod 文件夹名称 -->
		<ModsDirectory>Mods</ModsDirectory>
		<!-- Mod 信息文件 -->
		<ModInfo>Info.json</ModInfo>
		<!-- 游戏启动文件 -->
		<!-- <GameExe>game.exe</GameExe> -->
		<!-- 切入点 -->
		<EntryPoint>[UnityEngine.UIModule.dll]UnityEngine.Canvas.cctor:Before</EntryPoint>
		<!-- 开始点 -->
		<StartingPoint>[Assembly-CSharp.dll]main_controller.Start:Before</StartingPoint>
		<!-- 程序集名称 -->
		<AssemblyName>Assembly-CSharp.dll</AssemblyName>
		<!-- 遮罩目标？ -->
		<PatchTarget>global_data.Awake:After</PatchTarget>
	</GameInfo>
```
