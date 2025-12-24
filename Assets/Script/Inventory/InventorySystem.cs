using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public List<CaughtFish> caughtFishList = new List<CaughtFish>();

    public event System.Action OnInventoryChanged;

    public void NotifyInventoryChanged()
    {
        OnInventoryChanged?.Invoke();
    }

    public void AddCaughtFish(CaughtFish fish)
    {
        caughtFishList.Add(fish);
        NotifyInventoryChanged();
    }

    public void RemoveCaughtFish(CaughtFish fish)
    {
        caughtFishList.Remove(fish);
        NotifyInventoryChanged();
    }
}
