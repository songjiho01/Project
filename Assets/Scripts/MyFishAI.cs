using UnityEngine;

public class MyFishAI : MonoBehaviour
{
    public float patrolSpeed = 2f;                  // 순찰 속도
    public float chaseSpeed = 4f;                   // 추적 속도
    public float detectRange = 10f;                 // 후크 감지 범위
    public float biteRange = 1.0f;                  // 물기 범위
    public string hookTag = "Hook";                 // 후크 태그
    public string coughtHookTag = "CaughHook";        // 물렸을 때 후크의 태그
    public string caughtTag = "Caugh";              // 물었을 때 물고기의 태그


    // 최적화용 변수
    public LayerMask hookLayerMask;                 // Inspector에서 훅 레이어 선택
    public int detectionArraySize = 16;             // 예상 동시 충돌 수 (적절히 조절)


    // 탐지용 변수
    private Collider[] _results;
    private float detectTimer;
    public float detectInterval = 0.12f; // 체크 빈도(초)



    // 상태 변수
    private Transform targetHook;                   // 추적 중인 후크
    private bool isBiting = false;                  // 물고기가 물었는지 여부

    void Awake()
    {
        // 재사용 가능한 배열 할당 (GC 방지)
        _results = new Collider[Mathf.Max(1, detectionArraySize)];
    }




    void Update()
    {
        if (isBiting) return;

        detectTimer += Time.deltaTime;
        if (detectTimer >= detectInterval)
        {   // 일정 시간마다 후크 감지 시도
            detectTimer = 0f;
            DetectHook();
        }

        if (targetHook != null)
            ChaseHook();
    }



    public void TryBite(Transform hook)
    {
        if (!HookManager.Instance.TryHook(this))
        {
            // 다른 물고기가 먼저 물었음 → 관심 끊기
            targetHook = null;
            isBiting = false;

            return;
        }

        isBiting = true;

        gameObject.tag = caughtTag;
        hook.tag = coughtHookTag;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb) rb.isKinematic = true;

        transform.SetParent(hook);
        transform.localPosition = Vector3.zero;

        Debug.Log(" 물고기 1마리가 Hook 점유 완료!");
    }



    public void OnHooked(Transform hook)
    {
        transform.SetParent(hook);
        transform.localPosition = Vector3.zero;
    }


    public float waterLevelY = -1f;

    void DetectHook()
    {
        // 이미 훅 점유했으면 종료
        if (HookManager.Instance.currentFish != null) return;

        // 훅이 물 밖에 있으면 무시
        if (targetHook != null && targetHook.position.y > waterLevelY)
        {
            targetHook = null;
            return;
        }

        int count = Physics.OverlapSphereNonAlloc(transform.position, detectRange, _results, hookLayerMask);

        for (int i = 0; i < count; i++)
        {
            Transform hook = _results[i].transform;

            if (hook.CompareTag(hookTag) && hook.position.y <= waterLevelY)
            {
                targetHook = hook;
                return;
            }
        }

        targetHook = null;
    }



    void ChaseHook()
    {
        if (HookManager.Instance.currentFish != null && HookManager.Instance.currentFish != this)
        {
            targetHook = null;
            return;
        }

        MoveTowards(targetHook.position, chaseSpeed);

        if (Vector3.Distance(transform.position, targetHook.position) < biteRange)
            TryBite(targetHook);
    }




    void MoveTowards(Vector3 target, float speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(target - transform.position),
            Time.deltaTime * 2f);
    }
}
