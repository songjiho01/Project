using UnityEngine;

public class EquipmentVisualManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] PlayerEquipment playerEquipment;

    [Header("Sockets")]
    [SerializeField] Transform poleSocket;
    [SerializeField] Transform lureSocket;

    GameObject currentPoleInstance;
    GameObject currentLureInstance;

    // 캐시
    PoleSO lastPole;
    LureSO lastLure;

    void OnEnable()
    {
        if (playerEquipment != null)
        {
            playerEquipment.OnEquipmentChanged += ForceRefresh;
            ForceRefresh();
        }
    }

    void OnDisable()
    {
        if (playerEquipment != null)
            playerEquipment.OnEquipmentChanged -= ForceRefresh;
    }

    void Update()
    {
        //  값이 직접 바뀌었는지 실시간 감지
        if (playerEquipment.equippedPole != lastPole ||
            playerEquipment.equippedLure != lastLure)
        {
            RefreshVisuals();
        }
    }

    void ForceRefresh()
    {
        RefreshVisuals();
    }

    void RefreshVisuals()
    {
        lastPole = playerEquipment.equippedPole;
        lastLure = playerEquipment.equippedLure;

        UpdatePoleVisual();
        UpdateLureVisual();
    }

    // ==========================
    // Pole
    // ==========================
    void UpdatePoleVisual()
    {
        if (currentPoleInstance != null)
            Destroy(currentPoleInstance);

        var poleData = playerEquipment.equippedPole;
        if (poleData == null || poleData.polePrefab == null)
            return;

        currentPoleInstance = Instantiate(poleData.polePrefab, poleSocket);
        ResetLocalTransform(currentPoleInstance.transform);
    }

    // ==========================
    // Lure
    // ==========================
    void UpdateLureVisual()
    {
        if (currentLureInstance != null)
            Destroy(currentLureInstance);

        var lureData = playerEquipment.equippedLure;
        if (lureData == null || lureData.lurePrefab == null)
            return;

        currentLureInstance = Instantiate(lureData.lurePrefab, lureSocket);
        ResetLocalTransform(currentLureInstance.transform);
    }

    void ResetLocalTransform(Transform t)
    {
        t.localPosition = Vector3.zero;
        t.localRotation = Quaternion.identity;
        t.localScale = Vector3.one;
    }
}
