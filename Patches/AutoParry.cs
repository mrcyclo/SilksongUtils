using HarmonyLib;
using UnityEngine;

namespace SilksongUtils.Patches
{
    internal class AutoParry
    {
        private static GameObject parryBox = null;

        [HarmonyPatch(typeof(HeroBox), "Awake")]
        [HarmonyPrefix]
        private static void HeroBox_Awake_Prefix(HeroBox __instance)
        {
            //DebugDrawColliderRuntime.IsShowing = true;

            parryBox = new GameObject("Parry Box");
            parryBox.layer = __instance.gameObject.layer;
            parryBox.transform.SetParent(__instance.gameObject.transform);
            parryBox.transform.localPosition = Vector3.zero;

            var rb = parryBox.AddComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;

            var col = parryBox.AddComponent<CircleCollider2D>();
            col.isTrigger = true;

            parryBox.AddComponent<Components.AutoParry>();

            Object.DontDestroyOnLoad(parryBox);
        }

        [HarmonyPatch(typeof(HeroBox), "OnDestroy")]
        [HarmonyPrefix]
        private static void HeroBox_OnDestroy_Prefix(HeroBox __instance)
        {
            //DebugDrawColliderRuntime.IsShowing = false;

            Object.Destroy(parryBox);
        }
    }
}
