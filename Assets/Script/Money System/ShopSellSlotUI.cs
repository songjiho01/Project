using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSellSlotUI : MonoBehaviour
{
    [Header("UI")]
    public Image icon;
    public TMP_Text nameText;
    public TMP_Text statsText;
    public TMP_Text valueText;

    private CaughtFish fish;
    private InventorySystem inventory;
    private ShopPanel shopPanel;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        ClearSlot();
    }

    public void Setup(CaughtFish fishData, InventorySystem inv, ShopPanel panel)
    {
        fish = fishData;
        inventory = inv;
        shopPanel = panel;

        if (fish == null)
        {
            ClearSlot();
            return;
        }

        if (icon != null)
        {
            icon.enabled = true;
            icon.sprite = fish.species.icon;
        }

        if (nameText != null)
            nameText.text = fish.species.speciesName;

        if (statsText != null)
            statsText.text = $"{fish.length:F1}cm / {fish.weight:F1}kg";

        if (valueText != null)
            valueText.text = fish.GetSellPrice().ToString();

        // This slot is now selectable
        if (button != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => shopPanel.SelectFish(this, fish));
            button.interactable = true;
        }
    }

    public void ClearSlot()
    {
        fish = null;
        inventory = null;
        shopPanel = null;

        if (icon != null)
        {
            icon.sprite = null;
            icon.enabled = false;
        }

        if (nameText != null) nameText.text = "";
        if (statsText != null) statsText.text = "";
        if (valueText != null) valueText.text = "";

        if (button != null)
        {
            button.onClick.RemoveAllListeners();
            button.interactable = false;
        }
    }
}
