using GogoGaga.OptimizedRopesAndCables;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class FishingRope : MonoBehaviour
{
    /* ===================== References ===================== */
    [Header("References")]
    public Rope rope;
    public Rigidbody lureRb;

    /* ===================== Physics Settings ===================== */
    [Header("Physics Settings")]
    public float tensionForce = 1000f;

    /* ===================== Casting Settings ===================== */
    [Header("Casting Settings")]
    public float castSpeedThreshold = 6f;
    public float castForceMultiplier = 50f;
    public float castDelay = 2f;

    /* ===================== Reeling Settings ===================== */
    [Header("Reeling Settings")]
    public float reelSpeed = 5f;
    public float tensionIncreaseRate = 1100f;

    /* ===================== State Flags ===================== */
    private bool isCasting = false;
    private bool isExtending = false;
    private bool isAttachedToGround = false;
    private bool isReeling = false;
    private bool isFullyReeled = false;

    /* ===================== Internal Data ===================== */
    private Vector3 storedDirection;
    private float fishCaughtDistance = 2.0f;

    // ===================== Audio ===================== */
    public AudioSource reelAudio;




    /* ===================== Fixed Update ===================== */
    private void FixedUpdate()
    {
        if (rope == null || lureRb == null)
            return;

        HandleReeling();

        if (isAttachedToGround)
            return;

        HandleCasting();

        Vector3 start = rope.StartPoint.position;
        Vector3 end = lureRb.position;

        float maxLength = rope.ropeLength;
        float currentDistance = Vector3.Distance(start, end);
        Vector3 direction = (end - start).normalized;

        if (!isCasting)
        {
            if (currentDistance > maxLength)
            {
                float stretch = currentDistance - maxLength;
                lureRb.AddForce(-direction * stretch * tensionForce);
            }
            else
            {
                lureRb.position = start + direction * maxLength;
            }
        }
    }

    /* ===================== Reeling ===================== */
    private void HandleReeling()
    {
        if (!isReeling || isCasting || isExtending || isFullyReeled)
            return;     // 줄을 감지 않는 중이거나 캐스팅 또는 연장 중이거나 이미 완전히 감긴 상태이면 return

        if (rope.ropeLength <= 1f)
        {
            rope.ropeLength = 1f;
            tensionForce = 5000f;

            if (isAttachedToGround)
            {
                DetachLure();
            }

            isFullyReeled = true;
            isReeling = false;

            if (reelAudio != null && reelAudio.isPlaying)
                reelAudio.Stop();


            lureRb.linearVelocity = Vector3.zero;
            lureRb.angularVelocity = Vector3.zero;
            return;
        }

        float newLength = rope.ropeLength - reelSpeed * Time.fixedDeltaTime;
        rope.ropeLength = Mathf.Max(newLength, 1f);

        Vector3 start = rope.StartPoint.position;
        Vector3 dir = (lureRb.position - start).normalized;

        if (!isAttachedToGround)
        {
            lureRb.AddForce(-dir * tensionIncreaseRate);
        }
        else
        {
            lureRb.position = start + dir * rope.ropeLength;
        }

        CheckCatchFish();
    }

    /* ===================== Reel Control ===================== */
    public void StartReel()
    {
        if (isFullyReeled)
            return;

        isReeling = true;

        if (reelAudio != null && !reelAudio.isPlaying)
            reelAudio.Play();
    }

    public void StopReel()
    {
        isReeling = false;

        if (reelAudio != null && reelAudio.isPlaying)
            reelAudio.Stop();
    }

    /* ===================== Fish Catch Check ===================== */
    private void CheckCatchFish()
    {
        if (HookManager.Instance == null)
            return;

        if (HookManager.Instance.currentFish == null)
            return;

        if (HookManager.Instance.currentFish.gameObject == null)
            return;

        Vector3 start = rope.StartPoint.position;
        float distanceToStart = Vector3.Distance(start, lureRb.position);

        if (distanceToStart <= fishCaughtDistance)
        {
            HookManager.Instance.Release();

            isAttachedToGround = false;
            isExtending = false;
            isCasting = false;

            DetachLure();

            rope.ropeLength = Mathf.Max(2f, rope.ropeLength);
            tensionForce = 1000f;
        }
    }

    /* ===================== Casting ===================== */
    private void HandleCasting()
    {
        if (isCasting || isAttachedToGround)
            return;

        float speed = lureRb.linearVelocity.magnitude;

        if (speed > castSpeedThreshold)
        {
            storedDirection = lureRb.linearVelocity.normalized;
            tensionForce = 20f;
            StartCoroutine(CastRoutine());
        }
    }

    private IEnumerator CastRoutine()
    {
        isFullyReeled = false;
        isAttachedToGround = false;
        isReeling = false;

        tensionForce = 1000f;
        rope.ropeLength = 2.0f;

        lureRb.constraints = RigidbodyConstraints.None;
        lureRb.linearDamping = 0.2f;
        lureRb.angularDamping = 0.05f;

        isCasting = true;
        isExtending = true;

        DetachLure();

        yield return new WaitForSeconds(castDelay);

        lureRb.AddForce(storedDirection * castForceMultiplier, ForceMode.Impulse);
        isCasting = false;
    }

    /* ===================== Collision ===================== */
    public void OnLureCollision(Collision collision)
    {
        if (!isExtending || isAttachedToGround)
            return;

        Vector3 start = rope.StartPoint.position;
        float distance = Vector3.Distance(start, lureRb.position);

        rope.ropeLength = distance;
        tensionForce = 50f;

        isExtending = false;
        AttachLure();
    }

    /* ===================== Attach / Detach ===================== */
    private void AttachLure()
    {
        isAttachedToGround = true;

        lureRb.linearVelocity = Vector3.zero;
        lureRb.angularVelocity = Vector3.zero;

        lureRb.angularDamping = 10f;
        lureRb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void DetachLure()
    {
        isAttachedToGround = false;

        lureRb.linearDamping = 0.2f;
        lureRb.angularDamping = 0.05f;

        lureRb.constraints = RigidbodyConstraints.None;
    }
}
