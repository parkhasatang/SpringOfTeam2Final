using UnityEngine;

public class SlimeBossController : BossMonsterController
{
    private bool isEnraged = false; // 광폭화 상태 플래그
    private float lastJumpAttackTime = 0f;
    private float jumpAttackCooldown = 5.0f; // 점프 공격 쿨타임
    public float maxChaseDistance = 15f; // 플레이어 추적 최대 거리
    [SerializeField] private float damageRadius = 5.0f; // 인스펙터 창에서 조절 가능한 데미지 범위
    private Rigidbody2D rb;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (IsPlayerWithinChaseDistance())
        {
            PerformSpecialAttack();
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
        rb.velocity = Vector2.zero;

        //animator.SetTrigger("Idle");  이후 애니메이션 추가 하면됨
        Debug.Log("보스가 대기 상태로 전환");
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
}
