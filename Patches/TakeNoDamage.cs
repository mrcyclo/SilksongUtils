using HarmonyLib;

namespace SilksongUtils.Patches
{
    internal class TakeNoDamage
    {
        [HarmonyPatch(typeof(HeroController), "Update")]
        [HarmonyPrefix]
        private static void Update_Prefix(HeroController __instance)
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
