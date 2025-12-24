using UnityEngine;

[CreateAssetMenu(fileName = "FishSpecies", menuName = "Fishing/Fish Species")]
public class FishSpeciesSO : ScriptableObject
{
    [Header("Identification")]
    public string speciesName;
    public Sprite icon;
    public string description;  // <-- Added description

    [Header("Base Stats")]
    public float baseValue;

    [Header("Length Range (cm)")]
    public float minLength;
    public float maxLength;

    [Header("Weight Range (kg)")]
    public float minWeight;
    public float maxWeight;

    // ¡Ú Centralized value calculation
    public float CalculateValue(float length)
    {
        float sizeFactor = length / maxLength;
        return Mathf.Round(baseValue * sizeFactor * 10f) / 10f;
    }
}
