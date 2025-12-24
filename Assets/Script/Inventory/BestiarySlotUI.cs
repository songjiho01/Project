using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BestiarySlotUI : MonoBehaviour
{
    [Header("UI References")]
    public Image icon;
    public Button button;
    public TMP_Text countText;
    public Sprite unknownSprite;

    private BestiaryEntry entry;
    private FishDetailPanel detailPanel;



    public void Setup(BestiaryEntry bestiaryEntry, FishDetailPanel panel)
    {
        entry = bestiaryEntry;
        detailPanel = panel;

        icon.enabled = true;
        icon.sprite = entry.caughtBefore ? entry.species.icon : unknownSprite;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClickSlot);


        if (countText != null) countText.text = "";
    }

    // NEW – replaces ClearSlot()
    public void SetupEmpty(Sprite mystery)
    {
        entry = null;
        detailPanel = null;

        icon.enabled = true;
        icon.sprite = mystery;

        if (countText != null) countText.text = "";
    }

    private void OnClickSlot()
    {
        if (entry != null && detailPanel != null)
            detailPanel.SetBestiaryDetails(entry);
    }
}
