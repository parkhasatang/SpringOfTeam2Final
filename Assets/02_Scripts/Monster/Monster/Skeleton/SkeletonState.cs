using UnityEngine;

public class SkeletonWarriorState : MonsterState
{
    [SerializeField] private float attackRange = 1.0f; // 공격 범위
    [SerializeField] private float defenseChance = 0.3f; // 방어 확률

    protected override void Start()
    {
        base.Start();
        currentState = State.Idle;
    }

    protected override void Update()
    {
        base.Update();

        switch (currentState)
        {
            case State.Idle:
                IdleBehavior();
                break;
            case State.Move:
                MoveBehavior();
                break;
            case State.Chase:
                ChasePlayer();
                break;
            case State.Attack:
                AttackPlayer();
                break;
            case State.Death:
                OnDeath();
                break;
        }
    }

    protected override void IdleBehavior()
    {
        base.IdleBehavior();
    }

    protected override void MoveBehavior()
    {
        base.MoveBehavior();
    }

    protected override void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, statHandler.CurrentMonsterStats.speed * Time.deltaTime);

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= attackRange)
        {
            if (UnityEngine.Random.value < defenseChance)
            {
                Defend();
            }
            else
            {
                currentState = State.Attack;
            }
        }
    }

    private void Defend()
    {
        animator.SetTrigger("Defend");
    }

    protected override void AttackPlayer()
    {
        animator.SetTrigger("Attack");
    }

    protected override void OnDeath()
    {
        base.OnDeath();
    }
}
