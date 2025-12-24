using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    [Header("References")]
    public InventorySystem inventory;
    public ShopSellSlotUI[] slots;
    public Button sellButton; // unified sell button

    // Selection tracking
    private CaughtFish selectedFish;
    private ShopSellSlotUI selectedSlot;

    private void OnEnable()
    {
        if (inventory == null)
        {
            Debug.LogError("[ShopPanel] Inventory reference is NULL on enable.", this);
            return;
        }

        // Subscribe to inventory updates
        inventory.OnInventoryChanged -= RefreshShop;
        inventory.OnInventoryChanged += RefreshShop;

        // Set up sell button
        if (sellButton != null)
        {
            sellButton.onClick.RemoveAllListeners();
            sellButton.onClick.AddListener(SellSelectedFish);
            sellButton.interactable = false;
        }

        RefreshShop();
    }

    private void OnDisable()
    {
        if (inventory != null)
            inventory.OnInventoryChanged -= RefreshShop;
    }

    // Called by slots
    public void SelectFish(ShopSellSlotUI slot, CaughtFish fish)
    {
        selectedSlot = slot;
        selectedFish = fish;

        Debug.Log($"[ShopPanel] Selected: {fish?.species?.speciesName}", this);

        // Enable unified sell button
        if (sellButton != null)
            sellButton.interactable = true;
    }

    private void SellSelectedFish()
    {
        if (selectedFish == null)
        {
            Debug.LogWarning("[ShopPanel] SellSelectedFish called with no fish selected.", this);
            return;
        }

        int price = selectedFish.GetSellPrice();
        Debug.Log($"[ShopPanel] Selling: {selectedFish.species.speciesName} for {price}", this);

        // Add gold
        GlobalGold.Instance.SellItem(price);

        // Remove fish
        inventory.RemoveCaughtFish(selectedFish);

        // Clear selected UI slot
        selectedSlot.ClearSlot();

        // Reset selection
        selectedFish = null;
        selectedSlot = null;
        sellButton.interactable = false;

        // Refresh panel
        inventory.NotifyInventoryChanged();
    }

    public void RefreshShop()
    {
        if (slots == null || slots.Length == 0)
        {
            Debug.LogWarning("[ShopPanel] No slots assigned.", this);
            return;
        }

        // Reset selection on refresh
        selectedFish = null;
        selectedSlot = null;
        if (sellButton != null)
            sellButton.interactable = false;

        // Clear all slots
        foreach (var slot in slots)
        {
            if (slot != null)
                slot.ClearSlot();
        }

        // Fill slots with fish
        int count = Mathf.Min(inventory.caughtFishList.Count, slots.Length);

        for (int i = 0; i < count; i++)
        {
            CaughtFish fish = inventory.caughtFishList[i];
            slots[i].Setup(fish, inventory, this);
        }
    }
}
