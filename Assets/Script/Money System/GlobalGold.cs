using UnityEngine;
using System;

public class GlobalGold : MonoBehaviour
{
    public static GlobalGold Instance;

    [Header("Starting Gold")]
    public int gold = 0;

    public event Action<int> OnGoldChanged;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // -----------------------------
    // Internal gold modifier
    // -----------------------------
    private void ModifyGold(int amount)
    {
        gold += amount;
        OnGoldChanged?.Invoke(gold);
    }

    // -----------------------------
    // SELLING
    // -----------------------------
    public void SellFish(CaughtFish fish)
    {
        int price = fish.GetSellPrice();
        ModifyGold(price);
    }

    public void SellItem(int value)
    {
        ModifyGold(value);
    }

    // -----------------------------
    // BUYING & SPENDING GOLD
    // -----------------------------

    /// <summary>
    /// Returns TRUE if the player has enough gold for the purchase.
    /// </summary>
    public bool CanAfford(int price)
    {
        return gold >= price;
    }

    /// <summary>
    /// Attempts to spend the given amount. 
    /// Returns TRUE if successful.
    /// </summary>
    public bool SpendGold(int amount)
    {
        if (!CanAfford(amount))
            return false;

        ModifyGold(-amount);
        return true;
    }

    /// <summary>
    /// Same as SpendGold but named like a shop action.
    /// </summary>
    public bool Buy(int price)
    {
        return SpendGold(price);
    }
}
