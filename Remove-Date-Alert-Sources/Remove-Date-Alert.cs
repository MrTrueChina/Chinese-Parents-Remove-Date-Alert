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
            ModEntry.OnToggle = OnToggle;

            // 加载 Harmony
            var harmony = new Harmony(modEntry.Info.Id);
            harmony.PatchAll();

            modEntry.Logger.Log("移除约会警告 Mod 加载完成");
            
            // 返回加载成功
            return true;
        }

        /// <summary>
        /// Mod Manager 对 Mod 进行控制的时候会调用这个方法
        /// </summary>
        /// <param name="modEntry"></param>
        /// <param name="value">这个 Mod 是否激活</param>
        /// <returns></returns>
        static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
        {
            // 将 Mod Manager 切换的状态保存下来
            enabled = value;

            // 返回 true 表示这个 Mod 切换到 Mod Manager 切换的状态，返回 false 表示 Mod 依然保持原来的状态
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
            // 如果 Mod 没有启动，直接按照原流程继续调用
            if (!Main.enabled)
            {
                return true;
            }

            Main.ModEntry.Logger.Log("约会警告即将发出");
            
            // 阻断对发出警告方法的调用
            return false;
        }
    }
}
