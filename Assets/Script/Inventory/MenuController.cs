using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
    public GameObject menuCanvas;

    public InputAction Abutton;


    private void OnEnable()
    {
        Abutton.Enable();
        Abutton.performed += ctx => menuCanvas.SetActive(!menuCanvas.activeSelf);
    }
    private void OnDisable()
    {
        Abutton.Disable();
    }
    void Start()
    {
        menuCanvas.SetActive(false);
    }

    void Update()
    {
        
        
    }
}
