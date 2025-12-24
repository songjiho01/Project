using System.Collections.Generic;
using UnityEngine;

public class ShopBuyController : MonoBehaviour
{
    [Header("Shop Data")]
    public List<EquipmentSO> shopItems;             // Items in the shop
    public PlayerEquipment playerEquipment;         // Player inventory
    [Header("Equipment Panel")]
    public EquipmentPanelController equipmentPanel;
    [Header("UI Slots (Manual)")]
    public List<ShopBuySlotUI> slots;               // Manually placed slots

    private EquipmentSO currentSelectedItem;

    private void Start()
    {
        // Reset all items so Purchased = false at the start of play
        foreach (var item in shopItems)
        {
            item.Purchased = false;
        }

        SetupManualSlots(); // setup your UI slots
    }

    // Assign each item to each pre-placed slot
    private void SetupManualSlots()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (i < shopItems.Count)
            {
                slots[i].Setup(shopItems[i], this);
            }
            else
            {
                slots[i].gameObject.SetActive(false); // No item for this slot
            }
        }
    }

    public void SelectItem(EquipmentSO item)
    {
        currentSelectedItem = item;
        Debug.Log("Selected: " + item.itemName);
    }

    public void PurchaseSelected()
    {
        Debug.Log("PURCHASE BUTTON PRESSED");

        if (currentSelectedItem == null)
        {
            Debug.LogWarning("No item selected.");
            return;
        }

        if (currentSelectedItem.Purchased)
        {
            Debug.LogWarning("Item already purchased.");
            return;
        }

        // 🔥 Try to buy using GlobalGold
        bool success = GlobalGold.Instance.Buy(currentSelectedItem.price);

        if (!success)
        {
            Debug.LogWarning("Not enough gold.");
            return;
        }

        // Purchase successful
        currentSelectedItem.Purchased = true;

        // Add to player equipment
        playerEquipment.AddEquipment(currentSelectedItem);

        // Refresh UI
        foreach (var slot in slots)
            slot.Refresh();

        Debug.Log($"Purchased: {currentSelectedItem.itemName}");
    }



}
