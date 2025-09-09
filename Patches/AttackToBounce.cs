using HarmonyLib;

namespace SilksongUtils.Patches
{
    internal class AttackToBounce
    {
        [HarmonyPatch(typeof(BouncePod), "Hit")]
        [HarmonyPrefix]
        private static void BouncePod_Hit_Prefix(BouncePod __instance, ref HitInstance damageInstance)
        {
            if (!IMGUI.instance.AttackToBounce) return;
            if (damageInstance.AttackType != AttackTypes.Nail) return;

            var hitDirection = damageInstance.GetHitDirection(HitInstance.TargetType.BouncePod);
            if (hitDirection == HitInstance.HitDirection.Down) return;

            damageInstance.Direction = 300;
        }
    }
}
