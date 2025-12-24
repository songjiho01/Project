using UnityEngine;

public class BestiaryGridManager : MonoBehaviour
{
    [Header("UI References")]
    public BestiarySlotUI[] slotUIs;
    public FishDetailPanel detailPanel;
    public Sprite mysterySprite;

    [Header("Data")]
    public BestiarySystem bestiarySystem;

    public void PopulateGrid()
    {
        int entryCount = bestiarySystem.entries.Count;

        for (int i = 0; i < slotUIs.Length; i++)
        {
            if (i < entryCount)
            {
                slotUIs[i].Setup(bestiarySystem.entries[i], detailPanel);
            }
            else
            {
                slotUIs[i].SetupEmpty(mysterySprite);

            }
        }
    }

    private void Start()
    {
        PopulateGrid();
    }
}
