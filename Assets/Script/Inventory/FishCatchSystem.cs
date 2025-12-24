using UnityEngine;

public class FishCatchSystem : MonoBehaviour
{
    public InventorySystem inventory;
    public BestiarySystem bestiary;

    public void CatchFish(FishSpeciesSO fishData)
    {
        float weight = Random.Range(fishData.minWeight, fishData.maxWeight);
        float length = Random.Range(fishData.minLength, fishData.maxLength);
        float finalValue = fishData.CalculateValue(length);

        CaughtFish caughtFish = new CaughtFish()
        {
            species = fishData,
            weight = weight,
            length = length,
            finalValue = finalValue
        };

        inventory.AddCaughtFish(caughtFish);
        bestiary.UpdateBestiary(fishData, length, weight);
    }
}
