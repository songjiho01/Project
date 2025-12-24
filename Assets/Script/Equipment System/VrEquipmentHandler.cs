using UnityEngine;

public class VrEquipmentHandler : MonoBehaviour
{
    [Header("Hand Socket")]
    public Transform rightHandSocket;

    GameObject currentPole;
    Transform poleTipSocket;
    GameObject currentLure;

    // ==========================
    // Pole
    // ==========================
    public void EquipPole(PoleSO pole)
    {
        UnequipPole();

        if (pole == null || pole.polePrefab == null)
            return;

        currentPole = Instantiate(pole.polePrefab, rightHandSocket);
        ResetLocal(currentPole.transform);

        poleTipSocket = currentPole.transform.Find("PoleTipSocket");
        if (poleTipSocket == null)
            Debug.LogWarning("PoleTipSocket not found on pole prefab");
    }

    public void UnequipPole()
    {
        if (currentPole != null)
            Destroy(currentPole);

        currentPole = null;
        poleTipSocket = null;
        currentLure = null;
    }

    // ==========================
    // Lure
    // ==========================
    public void EquipLure(LureSO lure)
    {
        UnequipLure();

        if (lure == null || lure.lurePrefab == null) return;
        if (poleTipSocket == null) return;

        currentLure = Instantiate(lure.lurePrefab, poleTipSocket);
        ResetLocal(currentLure.transform);
    }

    public void UnequipLure()
    {
        if (currentLure != null)
            Destroy(currentLure);

        currentLure = null;
    }

    // ==========================
    // Helpers
    // ==========================
    void ResetLocal(Transform t)
    {
        t.localPosition = Vector3.zero;
        t.localRotation = Quaternion.identity;
        t.localScale = Vector3.one;
    }
}
