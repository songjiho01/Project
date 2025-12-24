using UnityEngine;

public class FishCollector : MonoBehaviour
{
    public FishCatchSystem catchSystem;
    public BobberCollision bobCol;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Caugh"))
        {
            FishWorld fish = other.GetComponent<FishWorld>();

            if (fish != null)
            {
                catchSystem.CatchFish(fish.GetSpecies());
                bobCol.ReleaseFish();
                Destroy(other.gameObject); // remove the fish from the world
            }
        }
    }
}
