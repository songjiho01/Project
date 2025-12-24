using System;
using UnityEngine;

[Serializable]
public class CaughtFish
{
    public FishSpeciesSO species;
    public float weight;
    public float length;
    public float finalValue; // stored as float for precision

    /// <summary>
    /// Returns the selling price as an integer (rounded).
    /// Shops should always use this instead of finalValue directly.
    /// </summary>
    public int GetSellPrice()
    {
        return Mathf.RoundToInt(finalValue);
    }
}
