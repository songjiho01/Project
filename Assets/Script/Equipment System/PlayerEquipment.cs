using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    [Header("Owned Equipment")]
    public PoleSO[] ownedPoles = new PoleSO[4];
    public LureSO[] ownedLures = new LureSO[4];

    [Header("Equipped Data")]
    public PoleSO equippedPole;
    public LureSO equippedLure;

    [Header("VR")]
    public VrEquipmentHandler vrHandler;

    public delegate void EquipmentChanged();
    public event EquipmentChanged OnEquipmentChanged;

    // ==========================
    // Add item to fixed slot
    // ==========================
    public bool AddEquipment(EquipmentSO item)
    {
        if (item is PoleSO pole)
        {
            int slot = pole.slotIndex;
            if (slot < 0 || slot >= ownedPoles.Length) return false;

            ownedPoles[slot] = pole;
            NotifyChange();
            return true;
        }

        if (item is LureSO lure)
        {
            int slot = lure.slotIndex;
            if (slot < 0 || slot >= ownedLures.Length) return false;

            ownedLures[slot] = lure;
            NotifyChange();
            return true;
        }

        Debug.LogWarning("AddEquipment: Unknown type");
        return false;
    }

    // ==========================
    // Equip / Unequip Pole
    // ==========================
    public void EquipPole(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= ownedPoles.Length) return;
        if (ownedPoles[slotIndex] == null) return;

        equippedPole = ownedPoles[slotIndex];
        vrHandler.EquipPole(equippedPole);

        // Reattach lure if already equipped
        if (equippedLure != null)
            vrHandler.EquipLure(equippedLure);

        NotifyChange();
    }

    public void UnequipPole()
    {
        equippedPole = null;
        vrHandler.UnequipPole();
        NotifyChange();
    }

    // ==========================
    // Equip / Unequip Lure
    // ==========================
    public void EquipLure(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= ownedLures.Length) return;
        if (ownedLures[slotIndex] == null) return;

        equippedLure = ownedLures[slotIndex];
        vrHandler.EquipLure(equippedLure);
        NotifyChange();
    }

    public void UnequipLure()
    {
        equippedLure = null;
        vrHandler.UnequipLure();
        NotifyChange();
    }

    private void NotifyChange()
    {
        OnEquipmentChanged?.Invoke();
    }
}
