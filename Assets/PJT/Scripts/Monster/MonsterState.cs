using System;
using UnityEngine;

public class MonsterState : MonoBehaviour
{
    protected GameObject player;
    protected Animator animator;
    protected CharacterStatHandler statHandler;
    protected HealthSystem healthSystem;
    public static event Action<GameObject> OnMonsterDeath;
    


    protected enum State
    {
        Idle,
        Move,
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
        healthSystem = GetComponent<HealthSystem>();

        statHandler = GetComponent<CharacterStatHandler>();
        if (statHandler == null)
        {
            statHandler = gameObject.AddComponent<CharacterStatHandler>();
        }

        statHandler.UpdateCharacterStats();
    }

    protected virtual void Update()
    {
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
                Destroy(gameObject);
                break;
        }
    }

    protected virtual void IdleBehavior()
    {

    }

    protected virtual void MoveBehavior()
    {
        
    }
    protected virtual void ChasePlayer()
    {
        if (Vector2.Distance(transform.position, player.transform.position) <= statHandler.CurrentMonsterStats.followDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, statHandler.CurrentMonsterStats.speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, player.transform.position) <= statHandler.CurrentMonsterStats.attackRange)
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
        if (Vector2.Distance(transform.position, player.transform.position) <= statHandler.CurrentMonsterStats.followDistance)
        {
            currentState = State.Chase;
        }
        else
        {
            currentState = State.Idle;
        }

        OnAttackHit();
    }

    protected virtual void OnAttackHit()
    {
        if (player != null)
        {
            if (Vector2.Distance(transform.position, player.transform.position) <= statHandler.CurrentMonsterStats.attackRange)
            {
                player.GetComponent<HealthSystem>().ChangeHealth(-statHandler.CurrentMonsterStats.attackDamage);
            }
        }
    }

    protected virtual void TakeDamage(float damage)
    {
        statHandler.CurrentMonsterStats.HP -= damage;
        if (statHandler.CurrentMonsterStats.HP <= 0)
        {
            OnDeath();
        }
    }

    protected virtual void OnDeath()
    {
        currentState = State.Death;
        animator.SetTrigger("Die");
        OnMonsterDeath?.Invoke(gameObject);
    }
}