using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public Transform target;
    public CharacterStatHandler statHandler;
    public float moveSpeed = 5f;

    private float lastAttackTime;
    private HealthSystem targetHealthSystem;
    private Rigidbody2D rb;

    private void Start()
    {
        targetHealthSystem = target.GetComponent<HealthSystem>();
        statHandler = GetComponent<CharacterStatHandler>();
        rb = GetComponent<Rigidbody2D>();
        GetComponent<HealthSystem>().OnDeath += OnDeath;
    }

    private void Update()
    {
        float distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (target != null)
        {
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
    }

    private void ChaseTarget()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
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
        rb.velocity = Vector2.zero;
        GetComponent<HealthSystem>().OnDeath -= OnDeath;
        Destroy(gameObject);
    }
}