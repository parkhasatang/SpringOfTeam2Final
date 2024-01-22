using UnityEngine;

public class SlimeBossController : BossMonsterController
{
    private bool isEnraged = false; // 광폭화 상태 플래그

    private void Update()
    {
        PerformActions();
    }

    protected virtual void PerformActions()
    {
        // 슬라임 보스의 특수 공격 로직
        StartCoroutine(SpecialAttacks.JumpAndDamage(transform, target, statHandler.CurrentStats.attackDamage, 5.0f, () =>
        {
            DealDamageAtPosition(target.position, statHandler.CurrentStats.attackDamage);
        }));
    }

    private void DealDamageAtPosition(Vector3 position, float damage)
    {
        float damageRadius = 5.0f; // 데미지를 주는 반경 설정
        Collider[] hitColliders = Physics.OverlapSphere(position, damageRadius);

        foreach (var hitCollider in hitColliders)
        {
            HealthSystem healthSystem = hitCollider.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.ChangeHealth(-damage); // 데미지 적용
            }
        }

        Debug.Log("플레이어게 데미지 " + damage);
    }

    private void EnrageIfNeeded()
    {
        // 체력이 50% 미만일 경우 광폭화 로직
        if (!isEnraged && healthSystem.CurrentHealth <= healthSystem.MaxHealth * 0.5f)
        {
            isEnraged = true;
            currentSpecialAttackCooldown /= 2; //광폭화 효과로 궁쿨 절반
        }
    }
}
