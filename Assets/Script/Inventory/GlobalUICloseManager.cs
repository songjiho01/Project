using UnityEngine;

public class GlobalUICloseManager : MonoBehaviour
{
    public static GlobalUICloseManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void CloseUI(GameObject ui)
    {
        if (ui != null)
            ui.SetActive(false);
    }
}
