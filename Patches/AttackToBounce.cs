using HarmonyLib;
using UnityEngine;

namespace SilksongUtils.Patches
{
    internal class AttackToBounce
    {
        [HarmonyReversePatch]
        [HarmonyPatch(typeof(HeroDownAttack), "OnHitResponded")]
        private static void HeroDownAttack_OnHitResponded(object instance, DamageEnemies.HitResponse hitResponse) => throw new System.NotImplementedException();

        [HarmonyPatch(typeof(DamageEnemies), "Awake")]
        [HarmonyPostfix]
        private static void DamageEnemies_Awake_Postfix(DamageEnemies __instance)
        {
            __instance.HitResponded += (response) =>
            {
                if (!Plugin.configAttackToBounce.Value) return;

                var hitDirection = response.Hit.GetHitDirection(HitInstance.TargetType.BouncePod);
                if (hitDirection == HitInstance.HitDirection.Up) return;

                var hc = HeroController.instance;
                if (hc.cState.onGround) return;

                var heroDownAttack = Object.FindAnyObjectByType<HeroDownAttack>();
                if (heroDownAttack == null) return;

                HeroDownAttack_OnHitResponded(heroDownAttack, response);
            };
        }

        [HarmonyPatch(typeof(BouncePod), "Hit")]
        [HarmonyPrefix]
        private static void BouncePod_Hit_Prefix(BouncePod __instance, ref HitInstance damageInstance)
        {
            if (!Plugin.configAttackToBounce.Value) return;
            if (damageInstance.AttackType != AttackTypes.Nail) return;

            var hitDirection = damageInstance.GetHitDirection(HitInstance.TargetType.BouncePod);
            if (hitDirection == HitInstance.HitDirection.Down) return;

            damageInstance.Direction = 300;
        }

        [HarmonyPatch(typeof(BounceBalloon), "Hit")]
        [HarmonyPrefix]
        private static void BounceBalloon_Hit_Prefix(BounceBalloon __instance, ref HitInstance damageInstance)
        {
            if (!Plugin.configAttackToBounce.Value) return;
            if (damageInstance.AttackType != AttackTypes.Nail) return;

            var hitDirection = damageInstance.GetHitDirection(HitInstance.TargetType.BouncePod);
            if (hitDirection == HitInstance.HitDirection.Down) return;

            damageInstance.Direction = 300;
        }
    }
}
