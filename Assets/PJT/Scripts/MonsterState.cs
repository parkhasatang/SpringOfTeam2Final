using UnityEngine;

public class MonsterState : MonoBehaviour
{
    public MonsterStatsSO monsterStats;
    private GameObject player;
    private Animator animator;
    protected enum State
    {
        Idle,
        Chase,
        Attack,
        Death
    }

    protected State currentState;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        currentState = State.Idle;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    protected virtual void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                IdleBehavior();
                break;
            case State.Chase:
                ChasePlayer();
                break;
            case State.Attack:
                AttackPlayer();
                break;
            case State.Death:
                break;
        }
    }

    protected virtual void IdleBehavior()
    {
        
    }
    protected virtual void ChasePlayer()
    {
        if (Vector2.Distance(transform.position, player.transform.position) <= monsterStats.followDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, monsterStats.speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, player.transform.position) <= monsterStats.attackRange)
            {
                currentState = State.Attack;
            }
        }
        else
        {
            currentState = State.Idle;
        }
    }

    protected virtual void AttackPlayer()
    {
        animator.SetTrigger("Attack");
        //실제 공격 로직은 애니메이션 이벤트에서 처리(공격 모션이 처리되는 시점에 밑에 OnAtrackHit 불러오기)
    }

    protected virtual void OnAttackHit()
    {
        if (Vector2.Distance(transform.position, player.transform.position) <= monsterStats.attackRange)
        {
            player.GetComponent<HealthSystem>().ChangeHealth(-monsterStats.attackDamage);
        }
    }
    public void TakeDamage(float damage)
    {

        monsterStats.currentHP -= damage;
        if (monsterStats.currentHP <= 0)
        {
            OnDeath();
        }
    }
    protected virtual void OnDeath()
    {
        currentState = State.Death;
        animator.SetTrigger("Die");
        Destroy(gameObject);
    }
}