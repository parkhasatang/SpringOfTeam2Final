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
        GetComponent<HealthSystem>().OnDeath += OnDeath;
    }


    private void Update()
    {
        float distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (distanceToTarget <= statHandler.CurrentStats.specificSO.attackRange)
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
            Debug.Log("공격!");
        }
    }
    private void OnDeath()
    {
        Debug.Log("몬스터 사망");
        Destroy(gameObject);
    }
}
