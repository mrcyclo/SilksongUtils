using HarmonyLib;

namespace SilksongUtils.Patches
{
    internal class CoralCrustTreeNoBreak
    {
        [HarmonyPatch(typeof(TimedActivator), "Update")]
        [HarmonyPrefix]
        private static void TimedActivator_Update_Prefix(TimedActivator __instance)
        {
            if (!Plugin.configCoralCrustTreeNoBreak.Value) return;
            if (__instance.gameObject.name != "Coral Crust Tree Activator") return;

            var duration = (float)AccessTools.Field(__instance.GetType(), "duration").GetValue(__instance);
            AccessTools.Field(__instance.GetType(), "durationLeft").SetValue(__instance, duration);
        }

        [HarmonyPatch(typeof(TimedActivator), "SendDeactivateWarning")]
        [HarmonyPrefix]
        private static bool TimedActivator_SendDeactivateWarning_Prefix(TimedActivator __instance)
        {
            if (!Plugin.configCoralCrustTreeNoBreak.Value) return true;
            if (__instance.gameObject.name != "Coral Crust Tree Activator") return true;
            return false;
        }
    }
}
