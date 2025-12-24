using UnityEngine;

public class HookManager : MonoBehaviour
{
    public static HookManager Instance;

    public MyFishAI currentFish = null;

    private void Awake()
    {   // ΩÃ±€≈Ê º≥¡§
        Instance = this;
    }

    public bool TryHook(MyFishAI fish)
    {
        if (currentFish != null) return false;

        currentFish = fish;
        return true;
    }

    public void Release()
    {
        currentFish = null;
    }
}
