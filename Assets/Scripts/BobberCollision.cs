using UnityEngine;

public class BobberCollision : MonoBehaviour
{
    public FishingRope rope;
    public MyFishAI currentFish = null;


    private void OnCollisionEnter(Collision collision)
    {
        rope.OnLureCollision(collision);

        // 이미 물고기 물었으면 무시
        if (currentFish != null) return;

        // 물고기 확인
        MyFishAI fish = GetComponentInChildren<MyFishAI>();
        FishAI fishs = GetComponentInChildren<FishAI>();

        if (fish == null) return;

        // 물고기 물림 처리
        currentFish = fish;

        gameObject.tag = "CaughHook";

        fishs.OnHooked(transform);     // 물고기 쪽 처리
    }

    public void ReleaseFish()
    { 
        gameObject.tag = "Hook";

    }
}
