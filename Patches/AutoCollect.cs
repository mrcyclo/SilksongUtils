using HarmonyLib;

namespace SilksongUtils.Patches
{
    internal class AutoCollect
    {
        [HarmonyPatch(typeof(CurrencyObjectBase), "MagnetToolIsEquipped")]
        [HarmonyPostfix]
        private static void Postfix(CurrencyObjectBase __instance, ref bool __result)
        {
            if (!Plugin.configAutoCollect.Value) return;
            __result = true;
        }
    }
}
