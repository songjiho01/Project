using UnityEngine;
using UnityEngine.UI;

public class ShopBuySlotUI : MonoBehaviour
{
    public Image icon;
    public Button button;

    private EquipmentSO item;
    private ShopBuyController controller;

    public void Setup(EquipmentSO newItem, ShopBuyController c)
    {
        item = newItem;
        controller = c;

        icon.sprite = item.icon;
        icon.enabled = true;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnSelect);

        ApplyPurchasedState();
    }

    private void OnSelect()
    {
        if (item == null || controller == null) return;
        controller.SelectItem(item);
    }

    public void Refresh()
    {
        ApplyPurchasedState();
    }

    private void ApplyPurchasedState()
    {
        // Button is disabled when purchased
        button.interactable = !item.Purchased;

        // Change alpha based on purchased state
        Color c = icon.color;
        c.a = item.Purchased ? 0f : 1f;   // 0 = invisible, 1 = normal
        icon.color = c;
    }
}
