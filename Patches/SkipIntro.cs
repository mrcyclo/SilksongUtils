using HarmonyLib;
using UnityEngine;

namespace SilksongUtils.Patches
{
    internal class SkipIntro
    {
        [HarmonyPatch(typeof(StartManager), "Start")]
        [HarmonyPostfix]
        private static void StartManager_Start_Postfix(StartManager __instance)
        {
            if (!Plugin.configSkipIntro.Value) return;
            __instance.gameObject.GetComponent<Animator>().speed = 99999f;
        }
    }
}
