using UnityEngine;
using System.Collections;

public class SlimeBossController : BossMonsterController
{
    private bool isEnraged = false;
    private float lastJumpAttackTime = 0f;
    private float jumpAttackCooldown = 5.0f;
    public float maxChaseDistance = 15f;
    public float attackDistanceThreshold = 10f;
    public float idleDistanceThreshold = 15f;
    [SerializeField] private float damageRadius = 5.0f;
    private float returnDelay = 5.0f;
    private Vector3 originalPosition;

    protected override void Start()
    {
        base.Start();
        originalPosition = transform.position;

        healthSystem.OnDeath += Death;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(target.position, transform.position);

        if (distanceToPlayer <= attackDistanceThreshold)
        {
            PerformSpecialAttack();
        }
        else if (distanceToPlayer <= idleDistanceThreshold)
        {
            StopChaseAndWait();
        }
        else
        {
            StopChaseAndWait();
        }
    }

    protected override void PerformSpecialAttack()
    {
        if (Time.time - lastJumpAttackTime >= jumpAttackCooldown)
        {
            StartCoroutine(SpecialAttacks.JumpAndDamage(transform, target, statHandler.CurrentStats.attackDamage, 5.0f, () =>
            {
                DealDamageAtPosition(target.position, statHandler.CurrentStats.attackDamage);
            }));

            lastJumpAttackTime = Time.time;
        }

        EnrageIfNeeded();
    }

    private bool IsPlayerWithinChaseDistance()
    {
        return Vector3.Distance(transform.position, target.position) <= maxChaseDistance;
    }

    private void StopChaseAndWait()
    {
        StartCoroutine(ReturnOriginalPosition(returnDelay));
    }

    private IEnumerator ReturnOriginalPosition(float delay)
    {
        yield return new WaitForSeconds(delay);

        float startTime = Time.time;
        Vector3 startPosition = transform.position;
        float journeyLength = Vector3.Distance(startPosition, originalPosition);
        float journeyDuration = journeyLength / statHandler.CurrentStats.speed;

        while (Time.time - startTime < journeyDuration)
        {
            float distanceCovered = (Time.time - startTime) * statHandler.CurrentStats.speed;
            float fractionOfJourney = distanceCovered / journeyLength;
            transform.position = Vector3.Lerp(startPosition, originalPosition, fractionOfJourney);
            yield return null;
        }

        // 최종 위치 설정 (originalPosition)
        transform.position = originalPosition;
    }

    private void DealDamageAtPosition(Vector3 position, float damage)
    {
        Collider[] hitColliders = Physics.OverlapSphere(position, damageRadius);
        foreach (var hitCollider in hitColliders)
        {
            HealthSystem healthSystem = hitCollider.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.ChangeHealth(-damage);
            }
        }
        Debug.Log("플레이어에게 데미지 " + damage);
    }

    private void EnrageIfNeeded()
    {
        if (!isEnraged && healthSystem.CurrentHealth <= healthSystem.MaxHealth * 0.5f)
        {
            isEnraged = true;
            currentSpecialAttackCooldown /= 2; // 광폭화 효과로 특수 공격 쿨타임 절반
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}