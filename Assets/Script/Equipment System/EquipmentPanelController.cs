using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentPanelController : MonoBehaviour
{
    [Header("References")]
    public PlayerEquipment playerEquipment;

    [Header("UI Slots")]
    public EquipmentSlotUI[] poleSlots;
    public EquipmentSlotUI[] lureSlots;

    [Header("Preview UI")]
    public Image previewImage;
    public TMP_Text previewName;
    public Button equipButton;
    public Button unequipButton;

    [Header("Currently Equipped UI")]
    public Image equippedPoleImage;
    public Image equippedLureImage;

    private EquipmentSO selectedItem;
    private int selectedIndex;
    private bool selectedIsPole;

    private void Start()
    {
        equipButton.onClick.RemoveAllListeners();
        equipButton.onClick.AddListener(OnEquip);

        unequipButton.onClick.RemoveAllListeners();
        unequipButton.onClick.AddListener(OnUnequip);

        // Subscribe to equipment changes to update currently equipped UI
        if (playerEquipment != null)
            playerEquipment.OnEquipmentChanged += UpdateEquippedUI;
        playerEquipment.OnEquipmentChanged += RefreshAllSlots;

        RefreshAllSlots();
        ClearPreview();
        UpdateEquippedUI();
    }

    private void OnDestroy()
    {
        if (playerEquipment != null)
            playerEquipment.OnEquipmentChanged -= UpdateEquippedUI;
    }

    // ==========================
    // Slots
    // ==========================
    public void RefreshAllSlots()
    {
        // Poles
        for (int i = 0; i < poleSlots.Length; i++)
        {
            var item = playerEquipment.ownedPoles[i];
            poleSlots[i].Setup(item, i, true, this);
        }

        // Lures
        for (int i = 0; i < lureSlots.Length; i++)
        {
            var item = playerEquipment.ownedLures[i];
            lureSlots[i].Setup(item, i, false, this);
        }
    }

    public void SelectSlot(EquipmentSO item, int index, bool isPole)
    {
        selectedItem = item;
        selectedIndex = index;
        selectedIsPole = isPole;

        if (item == null)
        {
            ClearPreview();
            return;
        }

        previewImage.sprite = item.icon;
        previewImage.enabled = true;
        previewName.text = item.itemName;
        equipButton.interactable = true;
        unequipButton.interactable = true;
    }

    private void ClearPreview()
    {
        selectedItem = null;
        selectedIndex = -1;
        previewImage.sprite = null;
        previewImage.enabled = false;
        previewName.text = "";
        equipButton.interactable = false;
        unequipButton.interactable = false;
    }

    // ==========================
    // Equip / Unequip
    // ==========================
    private void OnEquip()
    {
        if (selectedItem == null) return;

        if (selectedIsPole)
            playerEquipment.EquipPole(selectedIndex);
        else
            playerEquipment.EquipLure(selectedIndex);

        RefreshAllSlots();
    }

    private void OnUnequip()
    {
        if (selectedItem == null) return;

        if (selectedIsPole)
            playerEquipment.UnequipPole();
        else
            playerEquipment.UnequipLure();

        RefreshAllSlots();
    }

    // ==========================
    // Currently Equipped Display
    // ==========================
    private void UpdateEquippedUI()
    {
        // Pole
        if (playerEquipment.equippedPole != null)
        {
            equippedPoleImage.sprite = playerEquipment.equippedPole.icon;
            equippedPoleImage.color = Color.white;
        }
        else
        {
            equippedPoleImage.sprite = null;
            equippedPoleImage.color = new Color(1f, 1f, 1f, 0f);
        }

        // Lure
        if (playerEquipment.equippedLure != null)
        {
            equippedLureImage.sprite = playerEquipment.equippedLure.icon;
            equippedLureImage.color = Color.white;
        }
        else
        {
            equippedLureImage.sprite = null;
            equippedLureImage.color = new Color(1f, 1f, 1f, 0f);
        }
    }
}
