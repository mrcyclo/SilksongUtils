using System;
using HarmonyLib;

namespace SilksongUtils.Patches
{
    internal class AutoParry
    {
        [HarmonyReversePatch]
        [HarmonyPatch(typeof(HeroController), "DoAttack")]
        public static void HeroController_DoAttack(object instance) => throw new NotImplementedException();

        [HarmonyPatch(typeof(HeroController), "TakeDamage")]
        [HarmonyPrefix]
        private static void HeroController_TakeDamage_Prefix(HeroController __instance)
        {
            if (!Plugin.configAutoParry.Value) return;
            if (!__instance.CanAttack()) return;

            __instance.cState.parrying = true;
            HeroController_DoAttack(__instance);
        }
    }
}
