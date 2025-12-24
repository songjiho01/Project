using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ShopOpener : MonoBehaviour
{
    public GameObject shopCanvas;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;

    void Awake()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        interactable.selectEntered.AddListener(OpenShop);
    }

    void OnDestroy()
    {
        interactable.selectEntered.RemoveListener(OpenShop);
    }

    void OpenShop(SelectEnterEventArgs args)
    {
        shopCanvas.SetActive(true);
    }

    public void CloseShop()
    {
        shopCanvas.SetActive(false);
    }
}
