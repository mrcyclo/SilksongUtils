using HarmonyLib;
using System;

namespace SilksongUtils.Patches
{
    internal class ChangeEquipAnywhere
    {
        [HarmonyPatch(typeof(InventoryItemToolManager), "CanChangeEquips", new Type[] { })]
        [HarmonyPostfix]
        private static void InventoryItemToolManager_CanChangeEquips_Postfix(InventoryItemToolManager __instance, ref bool __result)
        {
            if (!Plugin.configChangeEquipAnywhere.Value) return;
            __result = true;
        }

        [HarmonyPatch(typeof(InventoryPaneList), "OnClosingInventory")]
        [HarmonyPostfix]
        private static void InventoryPaneList_OnClosingInventory_Postfix(InventoryPaneList __instance)
        {
            if (!Plugin.configChangeEquipAnywhere.Value) return;
            ToolItemManager.SendEquippedChangedEvent(false);
        }
    }
}
