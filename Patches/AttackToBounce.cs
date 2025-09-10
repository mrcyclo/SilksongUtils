using HarmonyLib;
using UnityEngine;

namespace SilksongUtils.Patches
{
    internal class AttackToBounce
    {
        [HarmonyReversePatch]
        [HarmonyPatch(typeof(HeroDownAttack), "OnHitResponded")]
        public static void HeroDownAttack_OnHitResponded(object instance, DamageEnemies.HitResponse hitResponse) => throw new System.NotImplementedException();

        [HarmonyPatch(typeof(DamageEnemies), "Awake")]
        [HarmonyPostfix]
        public static void DamageEnemies_ProcessDamageBuffer_Postfix(DamageEnemies __instance)
        {
            __instance.HitResponded += (response) =>
            {
                if (!Plugin.configAttackToBounce.Value) return;

                var hitDirection = response.Hit.GetHitDirection(HitInstance.TargetType.BouncePod);
                if (hitDirection == HitInstance.HitDirection.Down) return;

                var hc = HeroController.instance;
                if (hc.cState.onGround) return;

                var heroDownAttack = Object.FindAnyObjectByType<HeroDownAttack>();
                if (heroDownAttack == null) return;

                HeroDownAttack_OnHitResponded(heroDownAttack, response);
            };
        }
    }
}
