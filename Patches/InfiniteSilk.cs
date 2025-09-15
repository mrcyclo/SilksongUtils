using HarmonyLib;
using UnityEngine;

namespace SilksongUtils.Patches
{
    internal class InfiniteSilk
    {
        [HarmonyPatch(typeof(HeroController), "Update")]
        [HarmonyPrefix]
        private static void Update_Prefix(HeroController __instance)
        {
            if (!Plugin.configInfiniteSilk.Value) return;
            __instance.playerData.silk = Mathf.Max(__instance.playerData.silk, __instance.playerData.silkMax);
        }

        [HarmonyPatch(typeof(PlayerData), "TakeSilk")]
        [HarmonyPrefix]
        private static bool PlayerData_TakeSilk_Prefix(PlayerData __instance)
        {
            if (!Plugin.configInfiniteSilk.Value) return true;
            return false;
        }
    }
}
