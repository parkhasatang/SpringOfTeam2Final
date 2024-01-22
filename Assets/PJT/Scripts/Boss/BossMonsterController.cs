using UnityEngine;
using UnityEngine.AI;

public class BossMonsterController : MonoBehaviour
{
    public Transform target;
    public CharacterStatHandler statHandler;
    public HealthSystem healthSystem;

    private float lastAttackTime;
    private float lastSpecialAttackTime;
    public float initialSpecialAttackCooldown = 10f;
    protected float currentSpecialAttackCooldown;
    private HealthSystem targetHealthSystem;

    protected virtual void Start()
    {
        InitializeBoss();
    }

    protected void Update()
    {
        PerformStandardActions();
        CheckHealthAndAdjustBehavior();
    }

    private void InitializeBoss()
    {
        targetHealthSystem = target.GetComponent<HealthSystem>();
        statHandler = GetComponent<CharacterStatHandler>();
        healthSystem = GetComponent<HealthSystem>();
        currentSpecialAttackCooldown = initialSpecialAttackCooldown;
    }

    protected void PerformStandardActions()
    {
        if (ShouldAttack())
        {
            Attack();
        }

        if (ShouldPerformSpecialAttack())
        {
            PerformSpecialAttack();
        }
    }

    private bool ShouldAttack()
    {
        if (target == null)
        {
            // 타겟이 없을 때 대기
            return false;
        }

        return Vector3.Distance(target.position, transform.position) <= statHandler.CurrentStats.specificSO.attackRange
            && Time.time - lastAttackTime >= statHandler.CurrentStats.attackDelay;
    }



    private void Attack()
    {
        // 일반 공격 로직
        lastAttackTime = Time.time;
    }

    private bool ShouldPerformSpecialAttack()
    {
        return Time.time - lastSpecialAttackTime >= currentSpecialAttackCooldown;
    }

    private void PerformSpecialAttack()
    {
        // 특수 공격 로직
        lastSpecialAttackTime = Time.time;
    }

    private void CheckHealthAndAdjustBehavior()
    {
        if (healthSystem.CurrentHealth <= healthSystem.MaxHealth * 0.5f)
        {
            // 체력이 50% 미만일 경우 광폭화 로직
        }
    }
}
