using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [Header("물고기 프리팹 배열")]
    public GameObject[] fishPrefabs;   // 여러 물고기 프리팹

    [Header("최소 자유 물고기 수")]
    public int minFishCount = 100;

    [Header("스폰 영역")]
    public Vector3 areaSize = new Vector3(50, 0.1f, 50);

    void Update()
    {
        MaintainFishCountInArea();
    }

    // 범위 안 자유 물고기 수 유지
    void MaintainFishCountInArea()
    {
        int countInArea = 0;
        GameObject[] fishes = GameObject.FindGameObjectsWithTag("Fish");

        foreach (var fish in fishes)
        {
            if (IsInsideArea(fish.transform.position))
                countInArea++;
        }

        if (countInArea < minFishCount)
        {
            int needed = minFishCount - countInArea;
            for (int i = 0; i < needed; i++)
                SpawnFish();
        }
    }

    bool IsInsideArea(Vector3 pos)
    {
        Vector3 localPos = pos - transform.position;
        return Mathf.Abs(localPos.x) <= areaSize.x &&
               Mathf.Abs(localPos.y) <= areaSize.y &&
               Mathf.Abs(localPos.z) <= areaSize.z;
    }

    void SpawnFish()
    {
        // 랜덤 프리팹 선택
        int index = Random.Range(0, fishPrefabs.Length);
        GameObject prefab = fishPrefabs[index];

        // 스폰 위치
        Vector3 pos = transform.position + new Vector3(
            Random.Range(-areaSize.x, areaSize.x),
            Random.Range(-areaSize.y, areaSize.y),
            Random.Range(-areaSize.z, areaSize.z)
        );

        GameObject fish = Instantiate(prefab, pos, Quaternion.identity);

        // FishAI가 없으면 자동 추가
        FishAI fishAI = fish.GetComponent<FishAI>();
        if (fishAI == null)
            fishAI = fish.AddComponent<FishAI>();

        // 이동 범위와 스포너 연결
        fishAI.spawner = this.transform;
        fishAI.moveArea = areaSize;

        // Tag 설정
        if (fish.tag != "Fish" && fish.tag != "Caught")
            fish.tag = "Fish";
    }

    // Scene 뷰에서 영역 확인
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, areaSize * 2);
    }
}
