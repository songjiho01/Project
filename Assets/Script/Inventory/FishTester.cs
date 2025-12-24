using UnityEngine;

public class FishTester : MonoBehaviour
{
    [Header("References")]
    public FishCatchSystem catchSystem;

    [Header("Test Fish")]
    public FishSpeciesSO testFish;   // Assign a species in Inspector

    [ContextMenu("Test Catch")]
    public void TestCatch()
    {
        if (catchSystem == null)
        {
            Debug.LogError("FishTester: No FishCatchSystem assigned!");
            return;
        }

        if (testFish == null)
        {
            Debug.LogWarning("FishTester: Assign a testFish in the inspector before running Test Catch.");
            return;
        }

        catchSystem.CatchFish(testFish);
        Debug.Log("<color=green>Test Catch Success:</color> " + testFish.name);
    }
}
