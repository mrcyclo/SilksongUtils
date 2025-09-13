using HarmonyLib;

namespace SilksongUtils.Patches
{
    internal class InfiniteAttackTool
    {
        [HarmonyPatch(typeof(ToolItemManager), "GetToolStorageAmount")]
        [HarmonyPrefix]
        private static void ToolItemManager_GetToolStorageAmount_Prefix(ToolItemManager __instance, ToolItem tool)
        {
            if (!Plugin.configInfiniteAttackTool.Value) return;
            if (tool.Type != ToolItemType.Red) return;

            var savedData = tool.SavedData;
            savedData.AmountLeft = tool.BaseStorageAmount;
            tool.SavedData = savedData;
        }
    }
}
