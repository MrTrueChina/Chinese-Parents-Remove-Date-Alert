using HarmonyLib;
using UnityEngine;
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
    /// 女生面板增加恋爱警告值的方法，这个方法会随机增加一定量的恋爱警告值，恋爱警告会根据警告值出现
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

            Main.ModEntry.Logger.Log("女生面板增加恋爱警告值方法即将调用");

            // 阻断对发出警告方法的调用
            return false;
        }
    }

    /// <summary>
    /// 男生面板增加恋爱警告值的方法，这个方法和女生面板一样随机增加警告值，但这个方法同时负责调用对话，因此不能直接阻断
    /// </summary>
    [HarmonyPatch(typeof(BoysManager), "AddAlert")]
    public static class BoysManager_AddAlert
    {
        private static bool Prefix(BoysManager __instance, begintalkoverAction completeAction)
        {
            // 如果 Mod 没有启动，直接按照原流程继续调用
            if (!Main.enabled)
            {
                return true;
            }

            Main.ModEntry.Logger.Log("女生面板增加恋爱警告值方法即将调用");

            // 以下代码直接复制粘贴自反编译

            // 增加恋爱警告值的代码，注释掉即可
            //this.alertness += UnityEngine.Random.Range(20, 45);

            // 以下代码直接复制粘贴自反编译
            open_system.InstanceOpenSystem.AlertTalkInPanel(__instance.alertness, completeAction);

            // 阻断对发出警告方法的调用
            return false;
        }
    }
}
