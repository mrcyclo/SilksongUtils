using HarmonyLib;

namespace SilksongUtils.Patches
{
    internal class AutoCollect
    {
        [HarmonyPatch(typeof(CurrencyObjectBase), "Land")]
        [HarmonyPostfix]
        private static void Postfix(CurrencyObjectBase __instance)
        {
            if (!Plugin.configAutoCollect.Value) return;
            AccessTools.Field(typeof(CurrencyObjectBase), "isAttracted").SetValue(__instance, true);
        }
    }
}
