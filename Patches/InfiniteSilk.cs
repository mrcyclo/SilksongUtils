using HarmonyLib;

namespace SilksongUtils.Patches
{
    internal class InfiniteSilk
    {
        [HarmonyPatch(typeof(HeroController), "Update")]
        [HarmonyPrefix]
        private static void Update_Prefix(HeroController __instance)
        {
            if (!Plugin.configInfiniteSilk.Value) return;
            if (__instance.playerData.silk >= __instance.playerData.CurrentSilkMax) return;
            __instance.playerData.silk = __instance.playerData.CurrentSilkMax;
        }
    }
}
