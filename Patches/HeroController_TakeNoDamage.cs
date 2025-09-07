using HarmonyLib;

namespace SilksongUtils.Patches
{
    internal class HeroController_TakeNoDamage
    {
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
        }
    }
}
