using System.Collections.Generic;
using UnityEngine;

// ================================
// Bestiary Entry
// ================================
[System.Serializable]
public class BestiaryEntry
{
    public FishSpeciesSO species;
    public bool caughtBefore = false;
    public float bestLength = 0f;
    public float bestWeight = 0f;
}

// ================================
// Bestiary System
// ================================
public class BestiarySystem : MonoBehaviour
{
    [Header("Data")]
    public FishSpeciesSO[] allFishSpecies; // Assign all fish SOs
    public List<BestiaryEntry> entries = new List<BestiaryEntry>();

    [Header("UI")]
    public BestiaryGridManager gridManager; // Assign BestiaryGridManager

    private void Awake()
    {
        entries.Clear();
        foreach (var fish in allFishSpecies)
        {
            BestiaryEntry entry = new BestiaryEntry
            {
                species = fish,
                caughtBefore = false,
                bestLength = 0,
                bestWeight = 0
            };
            entries.Add(entry);
        }
    }

    public void Initialize(FishSpeciesSO[] allSpecies)
    {
        entries.Clear();
        foreach (var species in allSpecies)
        {
            BestiaryEntry entry = new BestiaryEntry
            {
                species = species,
                caughtBefore = false,
                bestLength = 0f,
                bestWeight = 0f
            };
            entries.Add(entry);
        }
    }

    public void UpdateBestiary(FishSpeciesSO species, float length, float weight)
    {
        BestiaryEntry entry = entries.Find(e => e.species == species);
        if (entry == null) return;

        entry.caughtBefore = true;

        if (length > entry.bestLength) entry.bestLength = length;
        if (weight > entry.bestWeight) entry.bestWeight = weight;

        if (gridManager != null)
            gridManager.PopulateGrid();
    }

}