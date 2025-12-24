using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    public InventorySystem inventorySystem;
    public InventoryUISlot[] inventorySlots;
    public FishDetailPanel detailPanel;

    private void OnEnable()
    {
        inventorySystem.OnInventoryChanged += RefreshUI;
        RefreshUI();
    }

    private void OnDisable()
    {
        inventorySystem.OnInventoryChanged -= RefreshUI;
    }

    public void RefreshUI()
    {
        int count = inventorySystem.caughtFishList.Count;

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (i < count)
                inventorySlots[i].Setup(inventorySystem.caughtFishList[i], detailPanel);
            else
                inventorySlots[i].ClearSlot();
        }
    }
}
