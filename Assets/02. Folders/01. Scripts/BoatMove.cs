using UnityEngine;

public class BoatMove : MonoBehaviour
{
    public float moveSpeed = 0.5f;   // 이동 속도
    public float z1 = -50f;          // 첫 번째 Z 좌표 (시작 위치)
    public float z2 = 50f;         // 두 번째 Z 좌표

    void Start()
    {
        // 시작 위치를 첫 번째 Z 좌표로 설정
        Vector3 pos = transform.position;
        pos.z = z1;
        transform.position = pos;
    }

    void Update()
    {
        MoveBoat();
    }

    void MoveBoat()
    {
        // 현재 위치
        Vector3 pos = transform.position;

        // 두 번째 좌표를 향해 이동
        pos.z = Mathf.MoveTowards(pos.z, z2, moveSpeed * Time.deltaTime);
        transform.position = pos;

        // 목표 Z2에 도착하면 즉시 첫 번째 좌표로 순간 이동
        if (Mathf.Approximately(pos.z, z2))
        {
            pos.z = z1;
            transform.position = pos;
        }
    }
}
