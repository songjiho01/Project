using UnityEngine;

public class WakeUI : MonoBehaviour
{
    [SerializeField] GameObject uiToWake;

    public void WakeUIElement()
    {
        uiToWake.SetActive(true);
    }
}
