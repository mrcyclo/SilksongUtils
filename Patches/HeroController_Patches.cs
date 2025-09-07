using HarmonyLib;

namespace SilksongUtils.Patches
{
    internal class HeroController_Patches
    {
        [HarmonyPatch(typeof(HeroController), "Awake")]
        [HarmonyPostfix]
        private static void Awake_Postfix(HeroController __instance)
        {
            __instance.OnDeath += () => Plugin.configDeathCount.Value += 1;
        }

        [HarmonyPatch(typeof(HeroController), "Update")]
        [HarmonyPostfix]
        private static void Update_Postfix(HeroController __instance)
        {
            if (IMGUI.instance.TakeNoDamage)
            {
                __instance.SetTakeNoDamage();
            }
            else
            {
                __instance.EndTakeNoDamage();
            }

            if (IMGUI.instance.InfiniteSilk)
            {
                if (__instance.playerData.silk < __instance.playerData.CurrentSilkMax)
                {
                    __instance.playerData.silk = __instance.playerData.CurrentSilkMax;
                }
            }
        }
    }
}
