using HarmonyLib;

namespace SilksongUtils.Patches
{
    internal class ReversePatch
    {
        [HarmonyReversePatch]
        [HarmonyPatch(typeof(HeroController), "DoAttack")]
        public static void HeroController_DoAttack(object instance) => throw new System.NotImplementedException();

        [HarmonyReversePatch]
        [HarmonyPatch(typeof(HeroDownAttack), "OnHitResponded")]
        public static void HeroDownAttack_OnHitResponded(object instance, DamageEnemies.HitResponse hitResponse) => throw new System.NotImplementedException();
    }
}
