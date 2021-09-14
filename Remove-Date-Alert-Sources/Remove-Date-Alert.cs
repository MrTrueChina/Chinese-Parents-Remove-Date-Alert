using HarmonyLib;
using UnityModManagerNet;

namespace MtC.Mod.ChineseParent.RemoveDateAlert
{
    public static class Main
    {
        /// <summary>
        /// Mod 对象
        /// </summary>
        public static UnityModManager.ModEntry ModEntry { get; set; }

        /// <summary>
        /// 这个 Mod 是否启动
        /// </summary>
        public static bool enabled;

        public static bool Load(UnityModManager.ModEntry modEntry)
        {
            // 保存 Mod 对象
            ModEntry = modEntry;

            // 加载 Harmony
            var harmony = new Harmony(modEntry.Info.Id);
            harmony.PatchAll();

            modEntry.Logger.Log("移除约会警告 Mod 加载完成");
            
            // 返回加载成功
            return true;
        }
    }

    /// <summary>
    /// 约会发出警告事件的补丁
    /// </summary>
    [HarmonyPatch(typeof(panel_girls), "add_alert")]
    public static class AddAlertPatch
    {
        private static bool Prefix()
        {
            Main.ModEntry.Logger.Log("约会警告即将发出");
            
            // 如果这个 Mod 已启动则返回 false，表示不调用约会警告发出方法
            return !Main.enabled;
        }
    }
}
