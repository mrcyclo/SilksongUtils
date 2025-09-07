using HarmonyLib;

namespace SilksongUtils.Patches
{
    internal class ShellShard_AutoCollect
    {
        [HarmonyPatch(typeof(CurrencyObjectBase), "Land")]
        [HarmonyPostfix]
        private static void Postfix(CurrencyObjectBase __instance)
        {
            if (IMGUI.instance.AutoCollect)
            {
                AccessTools.Field(typeof(CurrencyObjectBase), "isAttracted").SetValue(__instance, true);
            }
        }
    }
}
