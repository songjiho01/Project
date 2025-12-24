using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ShopButton : MonoBehaviour
{
    public GameObject shopCanvas;
    public bool isBuyButton;

    XRBaseInteractable interactable;

    void Awake()
    {
        interactable = GetComponent<XRBaseInteractable>();
        interactable.selectEntered.AddListener(OnPressed);
    }

    void OnDestroy()
    {
        interactable.selectEntered.RemoveListener(OnPressed);
    }

    void OnPressed(SelectEnterEventArgs args)
    {
        shopCanvas.SetActive(true);

        ShopUI ui = shopCanvas.GetComponent<ShopUI>();
        if (isBuyButton)
            ui.OpenBuy();
        else
            ui.OpenSell();
    }
}
