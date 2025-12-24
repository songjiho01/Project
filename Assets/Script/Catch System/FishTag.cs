using UnityEngine;

public class FishWorld : MonoBehaviour
{
    public FishSpeciesSO species;   // Assign this for each prefab in the inspector

    public FishSpeciesSO GetSpecies()
    {
        return species;
    }
}
