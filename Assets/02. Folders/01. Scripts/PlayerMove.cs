using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    // 이동 속도
    public float moveSpeed = 2f;

    // 마우스 민감도
    public float mouseSensitivity = 0.3f;

    // Rigidbody 컴포넌트
    private Rigidbody rb;

    // 이동 입력 저장 변수 (WASD)
    private Vector2 moveInput;

    // 마우스 입력 저장 변수
    private Vector2 lookInput;

    void Start()
    {
        // Rigidbody 가져오기
        rb = GetComponent<Rigidbody>();

        // 플레이어가 넘어지지 않도록 회전 고정
        rb.freezeRotation = true;
    }

    void Update()
    {
        Move();    // 플레이어 이동
        Rotate();  // 마우스 좌우 회전
    }

    // -------------------------------
    // Input System 이벤트 콜백 함수
    // -------------------------------

    // Move 액션이 호출되면 실행됨
    public void OnMove(InputValue value)
    {
        // Vector2 형태로 입력값 받기 (x: 좌우, y: 전후)
        moveInput = value.Get<Vector2>();
    }

    // Look 액션이 호출되면 실행됨
    public void OnLook(InputValue value)
    {
        // Vector2 (마우스 Delta 값)
        lookInput = value.Get<Vector2>();
    }

    // -------------------------------
    // 실제 기능 구현
    // -------------------------------

    // Rigidbody 이동 처리
    void Move()
    {
        // 이동 방향 계산 (전후 + 좌우)
        Vector3 move = transform.forward * moveInput.y + transform.right * moveInput.x;

        // MovePosition으로 자연스러운 물리 이동 처리
        rb.MovePosition(rb.position + move * moveSpeed * Time.deltaTime);
    }

    // 마우스 좌우 회전
    void Rotate()
    {
        // 마우스 X축 입력만 사용 (좌우 회전)
        float mouseX = lookInput.x * mouseSensitivity;

        // Y축 기준으로 회전 (Yaw)
        transform.Rotate(Vector3.up * mouseX);
    }
}
