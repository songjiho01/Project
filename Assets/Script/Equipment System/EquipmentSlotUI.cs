using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlotUI : MonoBehaviour
{
    public Image icon;
    public Button button;

    private EquipmentSO item;
    private int slotIndex;
    private bool isPoleSlot;

    private EquipmentPanelController controller;

    public void Setup(EquipmentSO newItem, int index, bool poleSlot, EquipmentPanelController c)
{
    item = newItem;
    slotIndex = index;
    isPoleSlot = poleSlot;
    controller = c;

    if (item == null)
    {
        icon.sprite = null;                 // Clear previous sprite
        icon.color = new Color(1f, 1f, 1f, 0f); // Make transparent
        button.interactable = false;
    }
    else
    {
        icon.sprite = item.icon;            // Show the new item
        icon.color = new Color(1f, 1f, 1f, 1f); // Fully visible
        button.interactable = true;
    }

    button.onClick.RemoveAllListeners();
    button.onClick.AddListener(OnSelect);
}


    private void OnSelect()
    {
        if (controller != null)
            controller.SelectSlot(item, slotIndex, isPoleSlot);
    }
}
