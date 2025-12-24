using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishDetailPanel : MonoBehaviour
{
    public Image fishImage;
    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public TMP_Text weightText;
    public TMP_Text lengthText;
    public TMP_Text valueText;

    public Sprite mysterySprite; // assign in inspector


    private void OnEnable()
    {
        ClearPanel();
    }

    public void SetInventoryDetails(CaughtFish fish)
    {
        fishImage.enabled = true; // 👈 Show only when needed
        fishImage.sprite = fish.species.icon;

        nameText.text = fish.species.speciesName;
        weightText.text = $"Weight: {fish.weight:F1} kg";
        lengthText.text = $"Length: {fish.length:F1} cm";
        valueText.text = $"Value: {fish.finalValue} gold";
    }

    public void ClearPanel()
    {
        fishImage.sprite = null;
        fishImage.enabled = false;   // 👈 Hide image

        nameText.text = "";
        weightText.text = "";
        lengthText.text = "";
        valueText.text = "";
    }

    public void SetBestiaryDetails(BestiaryEntry entry)
    {
        if (entry == null) return;
        fishImage.enabled = true;
        if (entry.caughtBefore)
        {
            // SHOW REAL DATA

            fishImage.sprite = entry.species.icon;
            nameText.text = entry.species.speciesName;
            descriptionText.text = entry.species.description;

            weightText.text = $"Best Weight: {entry.bestWeight:F1} kg";
            lengthText.text = $"Best Length: {entry.bestLength:F1} cm";

        }
        else
        {
            // SHOW ??? DATA
            fishImage.sprite = mysterySprite;

            nameText.text = "???";
            descriptionText.text = "You have not discovered this fish yet.";

            weightText.text = "Best Weight: ???";
            lengthText.text = "Best Length: ???";

        }
    }
}
