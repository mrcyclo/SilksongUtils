using HarmonyLib;
using SilksongUtils.Objects;
using UnityEngine;

namespace SilksongUtils.Patches
{
    internal class HealthManager_Patches
    {
        [HarmonyPatch(typeof(HealthManager), "Awake")]
        [HarmonyPostfix]
        private static void Awake_Postfix(HealthManager __instance)
        {
            var esp = __instance.gameObject.GetComponent<ESP>();
            if (esp == null)
            {
                __instance.gameObject.AddComponent<ESP>();
            }
        }

        [HarmonyPatch(typeof(HealthManager), "SetDead")]
        [HarmonyPostfix]
        private static void SetDead_Postfix(HealthManager __instance)
        {
            var esp = __instance.gameObject.GetComponent<ESP>();
            if (esp != null)
            {
                Object.Destroy(esp);
            }
        }
    }
}
