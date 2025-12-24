using TMPro;
using UnityEngine;

public class GoldTextUI : MonoBehaviour
{
    public TMP_Text goldText;

    private void Start()
    {
        UpdateUI(GlobalGold.Instance.gold);
        GlobalGold.Instance.OnGoldChanged += UpdateUI;
    }

    private void OnDestroy()
    {
        GlobalGold.Instance.OnGoldChanged -= UpdateUI;
    }

    private void UpdateUI(int newGold)
    {
        goldText.text = newGold.ToString();
    }
}
