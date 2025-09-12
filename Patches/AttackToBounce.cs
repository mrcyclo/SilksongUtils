using System.Collections.Generic;
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
        public static void DamageEnemies_Awake_Postfix(DamageEnemies __instance)
        {
            __instance.HitResponded += (response) =>
            {
                if (!Plugin.configAttackToBounce.Value) return;

                var hitDirection = response.Hit.GetHitDirection(HitInstance.TargetType.BouncePod);
                if (hitDirection == HitInstance.HitDirection.Down) return;

                var hc = HeroController.instance;
                if (hc.cState.onGround) return;

                //var heroDownAttack = Object.FindAnyObjectByType<HeroDownAttack>();
                //if (heroDownAttack == null) return;

                //response.Hit.Direction = 300;

                //Debug.Break();

                //HeroDownAttack_OnHitResponded(heroDownAttack, response);
                //Plugin.Logger.LogInfo("HeroDownAttack_OnHitResponded executed");

                 hc.DownspikeBounce(false);
            };
        }

        //[HarmonyPatch(typeof(DamageEnemies), "ProcessDamageBuffer")]
        //[HarmonyPrefix]
        //public static void DamageEnemies_ProcessDamageBuffer_Prefix(DamageEnemies __instance)
        //{
        //    if (!Plugin.configAttackToBounce.Value) return;

        //    //Plugin.Logger.LogInfo("DamageEnemies_ProcessDamageBuffer_Prefix");

        //    var processingDamageBuffer = (List<DamageEnemies.HitResponse>)AccessTools.Field(__instance.GetType(), "processingDamageBuffer").GetValue(__instance);
        //    foreach (var hitResponse in processingDamageBuffer)
        //    {
        //        var hit = hitResponse.Hit;

        //        Plugin.Logger.LogInfo("Hit: " + hit.ToString());

        //        var hitDirection = hit.GetHitDirection(HitInstance.TargetType.BouncePod);
        //        if (hitDirection == HitInstance.HitDirection.Down) continue;

        //        Plugin.Logger.LogInfo("Modifying hit direction to Down");

        //        hit.Direction = 300;
        //    }
        //}
    }
}
