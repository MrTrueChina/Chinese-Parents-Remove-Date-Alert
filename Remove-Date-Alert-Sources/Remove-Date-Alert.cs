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
    public static class panel_girls_add_alert
    {
        private static void Postfix()
        {
            // 如果 Mod 没有启动，不进行处理
            if (!Main.enabled)
            {
                return;
            }

            Main.ModEntry.Logger.Log("女生面板增加恋爱警告值方法调用完毕");

            // 将警告值设为 0
            girlmanager.InstanceGirlmanager.alertness = 0;
        }
    }

    /// <summary>
    /// 男生面板增加恋爱警告值的方法，这个方法和女生面板一样随机增加警告值。另外一提，这个方法在原代码中还负责继续向后调用
    /// </summary>
    [HarmonyPatch(typeof(BoysManager), "AddAlert")]
    public static class BoysManager_AddAlert
    {
        private static void Postfix(BoysManager __instance)
        {
            // 如果 Mod 没有启动，不进行处理
            if (!Main.enabled)
            {
                return;
            }

            Main.ModEntry.Logger.Log("男生面板增加恋爱警告值方法调用完毕");

            // 将警告值设为 0
            __instance.alertness = 0;
        }
    }
}
