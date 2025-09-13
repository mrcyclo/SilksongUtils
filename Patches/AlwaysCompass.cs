using GlobalSettings;
using HarmonyLib;
using System;

namespace SilksongUtils.Patches
{
    internal class AlwaysCompass
    {
        [HarmonyPatch(typeof(ToolItemManager), "IsToolEquipped", new Type[] { typeof(ToolItem), typeof(ToolEquippedReadSource) })]
        [HarmonyPostfix]
        private static void ToolItemManager_IsToolEquipped_Postfix(ToolItemManager __instance, ref bool __result, ToolItem tool)
        {
            if (!Plugin.configAlwaysCompass.Value) return;
            if (tool != Gameplay.CompassTool) return;
            __result = true;
        }
    }
}
