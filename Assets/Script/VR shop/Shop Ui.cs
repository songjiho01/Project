using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public GameObject buyPanel;
    public GameObject sellPanel;

    

    public void OpenBuy()
    {
        buyPanel.SetActive(true);
        sellPanel.SetActive(false);
    }

    public void OpenSell()
    {
        buyPanel.SetActive(false);
        sellPanel.SetActive(true);
    }

    
}
