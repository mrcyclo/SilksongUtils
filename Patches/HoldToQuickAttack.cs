using HarmonyLib;

namespace SilksongUtils.Patches
{
    internal class HoldToQuickAttack
    {
        private static bool cStateAttacking = false;

        [HarmonyPatch(typeof(HeroController), "CanAttackAction")]
        [HarmonyPrefix]
        private static void HeroController_CanAttackAction_Prefix(HeroController __instance)
        {
            if (!IMGUI.instance.FastAttack) return;

            cStateAttacking = __instance.cState.attacking;
            __instance.cState.attacking = false;
        }

        [HarmonyPatch(typeof(HeroController), "CanAttackAction")]
        [HarmonyPostfix]
        private static void HeroController_CanAttackAction_Postfix(HeroController __instance)
        {
            if (!IMGUI.instance.FastAttack) return;

            __instance.cState.attacking = cStateAttacking;
        }

        [HarmonyPatch(typeof(HeroController), "DoAttack")]
        [HarmonyPostfix]
        private static void HeroController_DoAttack_Postfix(HeroController __instance)
        {
            if (!IMGUI.instance.FastAttack) return;

            AccessTools.Field(__instance.GetType(), "attack_cooldown").SetValue(__instance, 0f);
        }
    }
}
