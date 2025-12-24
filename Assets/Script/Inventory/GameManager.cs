using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject titleCanvas;          // Title UI
    public CanvasGroup titleCanvasGroup;    // CanvasGroup on titleCanvas for fading

    [Header("Player")]
    public GameObject player;               // Player object with control scripts

    [Header("Fade Settings")]
    public float fadeDuration = 1f;

    void Start()
    {
        // Disable player control at start
        //SetPlayerControl(false);

        // Ensure title alpha is fully visible at start
        if (titleCanvasGroup != null)
            titleCanvasGroup.alpha = 1f;
    }

    public void OnStartButtonPressed()
    {
        // Start fade-out coroutine
        StartCoroutine(FadeOutTitle());
    }

    private IEnumerator FadeOutTitle()
    {
        if (titleCanvasGroup != null)
        {
            float elapsed = 0f;
            float startAlpha = titleCanvasGroup.alpha;

            while (elapsed < fadeDuration)
            {
                elapsed += Time.deltaTime;
                titleCanvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, elapsed / fadeDuration);
                yield return null;
            }

            titleCanvasGroup.alpha = 0f;
            titleCanvas.SetActive(false); // Hide after fade
        }

        // Enable player control after title fades
        //SetPlayerControl(true);
    }

    //private void SetPlayerControl(bool enabled)
    //{
    //    var movement = player.GetComponent<PlayerMovement>();
    //    if (movement != null) movement.enabled = enabled;

    //    var xrRig = player.GetComponent<XRRig>();
    //    if (xrRig != null) xrRig.enabled = enabled;
    //}
}
