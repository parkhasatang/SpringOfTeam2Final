using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;
    public CharacterStatHandler statHandler;

    private float lastAttackTime;
    private HealthSystem targetHealthSystem;

    private void Start()
    {
        targetHealthSystem = target.GetComponent<HealthSystem>();
        statHandler = GetComponent<CharacterStatHandler>();
    }

    private void Update()
    {
        float distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (distanceToTarget <= statHandler.CurrentStats.baseStatsSO.attackRange)
        {
            if (Time.time - lastAttackTime >= statHandler.CurrentStats.attackDelay)
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
            targetHealthSystem.ChangeHealth(statHandler.CurrentStats.attackDamage);
            Debug.Log("АјАн!");
        }
    }
}
