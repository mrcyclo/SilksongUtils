using HarmonyLib;

namespace SilksongUtils.Patches
{
    internal class HeroController_DeathCount
    {
        [HarmonyPatch(typeof(HeroController), "Awake")]
        [HarmonyPostfix]
        private static void Awake_Postfix(HeroController __instance)
        {
            __instance.OnDeath += OnDeath;
        }

        private static void OnDeath()
        {
            Plugin.configDeathCount.Value += 1;
        }
    }
}
