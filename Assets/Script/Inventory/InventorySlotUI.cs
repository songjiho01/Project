using UnityEngine;
using UnityEngine.UI;

public class InventoryUISlot : MonoBehaviour
{
    public Image icon;
    public Button button;

    private CaughtFish data;
    private FishDetailPanel detailPanel;

    public void Setup(CaughtFish fish, FishDetailPanel panel)
    {
        data = fish;
        detailPanel = panel;

        icon.sprite = fish.species.icon;
        icon.enabled = true;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClickSlot);
    }

    private void OnClickSlot()
    {
        detailPanel.SetInventoryDetails(data);
    }

    public void ClearSlot()
    {
        data = null;
        icon.sprite = null;
        icon.enabled = false;

        button.onClick.RemoveAllListeners();
    }
}
