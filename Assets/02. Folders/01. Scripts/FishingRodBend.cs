using System.Collections.Generic;
using UnityEngine;

public class FishingRodBend : MonoBehaviour
{
    [Header("낚싯대 본 개수")]
    public List<Transform> bones = new List<Transform>();

    [Header("물고기 당기는 힘 설정")]
    public float fishForce = 0f;        // 물고기가 현재 당기는 힘
    public float maxFishForce = 50f;    // 이 힘일 때 낚싯대가 최대 휘어짐 (임의로 변경 가능)

    [Header("휘어짐 설정")]
    public float maxBendAngle = 25f;    // 가장 끝 본의 최대 회전 각도
    public bool resetEachFrame = true;  // 매 프레임 기본 회전값으로 되돌릴지 여부

    // 각 본의 기본 로컬 회전 값을 저장할 리스트
    private List<Quaternion> defaultRot = new List<Quaternion>();


    void Start()
    {
        SaveDefaultRotations(); // 시작할 때 기본 회전 저장
    }

    void Update()
    {
        EnsureDefaultRotationSizeMatches(); // 본 개수 변경 시 자동 맞춤

        // ───────────────────────────────────────────
        //  물고기 힘 → tension(0~1) 값으로 변환
        //  fishForce / maxFishForce 로 정규화
        // ───────────────────────────────────────────
        float tension = Mathf.Clamp01(fishForce / maxFishForce);

        // tension 값에 따라 휘어짐 적용
        ApplyZPlusBend(tension);
    }


    // --------------------------------------------
    // 본들의 기본 회전 값 저장
    // --------------------------------------------
    void SaveDefaultRotations()
    {
        defaultRot.Clear();
        for (int i = 0; i < bones.Count; i++)
            defaultRot.Add(bones[i].localRotation);
    }

    // 본 리스트가 수정되었을 때 기본값 리스트도 자동 수정
    void EnsureDefaultRotationSizeMatches()
    {
        if (defaultRot.Count != bones.Count)
            SaveDefaultRotations();
    }


    // --------------------------------------------
    // tension 값을 받아 Z+ 방향으로 휘어지게 적용
    // --------------------------------------------
    void ApplyZPlusBend(float tension)
    {
        if (bones.Count == 0) return; // 본이 없으면 무시

        for (int i = 0; i < bones.Count; i++)
        {
            // 매 프레임 기본 회전으로 초기화할지 선택
            if (resetEachFrame)
                bones[i].localRotation = defaultRot[i];

            // 본의 위치 비율 (0~1) — 끝으로 갈수록 값 커짐
            float t = (float)(i + 1) / bones.Count;

            // 휘어짐 각도 계산 (제곱을 써서 끝부분이 더 많이 휘어짐)
            float angle = maxBendAngle * tension * Mathf.Pow(t, 2f);

            // 본을 로컬 Z+ 방향(정확히 Vector3.forward)으로 회전
            bones[i].localRotation *= Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
