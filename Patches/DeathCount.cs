using HarmonyLib;

namespace SilksongUtils.Patches
{
    internal class DeathCount
    {
        [HarmonyPatch(typeof(HeroController), "Awake")]
        [HarmonyPostfix]
        private static void Awake_Postfix(HeroController __instance)
        {
            __instance.OnDeath += () => Plugin.configDeathCount.Value += 1;
        }
    }
}
