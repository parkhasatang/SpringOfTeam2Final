using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;
    public MonsterStatsSO monsterStats;

    private float lastAttackTime;
    private HealthSystem targetHealthSystem;

    private void Start()
    {
        targetHealthSystem = target.GetComponent<HealthSystem>();
    }

    private void Update()
    {
        float distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (distanceToTarget <= monsterStats.attackRange)
        {
            if (Time.time - lastAttackTime >= monsterStats.monsterBaseStats.attackDelay)
            {
                Attack();
                lastAttackTime = Time.time;
            }
        }
        else
        {
            ChaseTarget();
        }
    }

    private void ChaseTarget()
    {
        agent.SetDestination(target.position);
    }

    private void Attack()
    {
        if (targetHealthSystem != null)
        {
            targetHealthSystem.ChangeHealth(-monsterStats.monsterBaseStats.attackDamage);
            Debug.Log("АјАн!");
        }
    }
}
