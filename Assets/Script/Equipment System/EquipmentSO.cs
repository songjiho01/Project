using UnityEngine;

public class EquipmentSO : ScriptableObject
{
    [Header("Basic Info")]
    public string itemName;
    public Sprite icon;
    public int price;

    [Header("Shop State")]
    public bool Purchased = false;

    [Header("Fixed Slot Index (0–3)")]
    public int slotIndex;   // 🔥 This is the part you're missing!
}
