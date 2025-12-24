using UnityEngine;

public class FishAI : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 0.5f;               // 이동 속도
    public float changeDirTime = 3f;       // 방향 변경 주기

    [HideInInspector] public Transform spawner;  // 스포너 기준 위치
    [HideInInspector] public Vector3 moveArea;   // 스폰 범위

    private Vector3 moveDir;                // 현재 이동 방향
    public bool IsHooked { get; private set; } = false;

    private Animator animator;

    void Start()
    {
        // 초기 방향 설정
        SetRandomDir();
        InvokeRepeating(nameof(SetRandomDir), changeDirTime, changeDirTime);

        // Rigidbody 세팅
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }

        // Animator 세팅
        animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.enabled = true;
            animator.Play("Swim");  // 항상 Swim 재생
        }
    }

    void Update()
    {
        if (IsHooked) return;

        // 이동
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // 회전: 이동 방향으로 부드럽게 회전
        if (moveDir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
        }

        // 스폰 범위 밖이면 돌아오기
        if (spawner != null)
        {
            Vector3 localPos = transform.position - spawner.position;
            if (Mathf.Abs(localPos.x) > moveArea.x ||
                Mathf.Abs(localPos.y) > moveArea.y ||
                Mathf.Abs(localPos.z) > moveArea.z)
            {
                moveDir = (spawner.position - transform.position).normalized;
            }
        }
    }

    public void OnHooked(Transform hook)
    {
        IsHooked = true;   //  여기서 true로 변경

        // 기존 로직들
        transform.SetParent(hook);
    }

    // 랜덤 방향 설정
    void SetRandomDir()
    {
        moveDir = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-0.1f, 0.1f),
            Random.Range(-1f, 1f)
        ).normalized;
    }
}
