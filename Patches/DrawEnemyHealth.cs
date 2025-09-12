using HarmonyLib;
using SilksongUtils.Objects;
using UnityEngine;

namespace SilksongUtils.Patches
{
    internal class DrawEnemyHealth
    {
        [HarmonyPatch(typeof(HealthManager), "Awake")]
        [HarmonyPostfix]
        private static void Awake_Postfix(HealthManager __instance)
        {
            //var eventRegister = __instance.gameObject.GetComponent<EventRegister>();
            //if (eventRegister != null) return;

            var esp = __instance.gameObject.GetComponent<ESP>();
            if (esp != null) return;

            __instance.gameObject.AddComponent<ESP>();
        }

        [HarmonyPatch(typeof(HealthManager), "SetDead")]
        [HarmonyPostfix]
        private static void SetDead_Postfix(HealthManager __instance)
        {
            var esp = __instance.gameObject.GetComponent<ESP>();
            if (esp == null) return;

            Object.Destroy(esp);
        }
    }
}
