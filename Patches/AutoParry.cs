using HarmonyLib;
using UnityEngine;

namespace SilksongUtils.Patches
{
    internal class AutoParry
    {
        [HarmonyPatch(typeof(DamageHero), "TryClashTinkCollider")]
        [HarmonyPrefix]
        private static void DamageHero_TryClashTinkCollider_Prefix(DamageHero __instance, ref Collider2D collision)
        {
            if (!Plugin.configAutoParry.Value) return;

            var hc = HeroController.instance;
            if (hc == null) return;

            //Plugin.Logger.LogInfo($"collision.gameObject.layer: {LayerMask.LayerToName(collision.gameObject.layer)} ({collision.gameObject.layer})");
            if (collision.gameObject != hc.gameObject) return;

            var tinkerTransform = hc.transform.Find("Parry Tinker");
            if (tinkerTransform == null)
            {
                var tinker = new GameObject("Parry Tinker");
                tinker.layer = LayerMask.NameToLayer("Tinker");
                tinker.tag = "Nail Attack";
                tinker.transform.SetParent(hc.transform);
                tinker.transform.localPosition = Vector3.zero;

                tinker.AddComponent<NailSlashTerrainThunk>();

                var collider = tinker.AddComponent<BoxCollider2D>();
                collider.isTrigger = true;

                var rigidbody = tinker.AddComponent<Rigidbody2D>();
                rigidbody.bodyType = RigidbodyType2D.Kinematic;

                tinkerTransform = tinker.transform;
            }

            collision = tinkerTransform.GetComponent<BoxCollider2D>();
        }

        [HarmonyPatch(typeof(HeroBox), "CheckForDamage")]
        [HarmonyPrefix]
        private static bool HeroBox_CheckForDamage_Prefix(HeroBox __instance, ref GameObject otherGameObject)
        {
            if (!Plugin.configAutoParry.Value) return true;
            if (otherGameObject.layer != LayerMask.NameToLayer("Enemy Attack")) return true;

            return false;
        }
    }
}
