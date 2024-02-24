using System.Collections;
using UnityEngine;

public class KingJorgState : BossState
{
    public float chargeSpeed = 5f;
    public int chargeCount = 3;
    public float wanderRadius = 5f;
    public float attackCooldown = 3f;
    private bool isAngry = false;
    private int chargesLeft;
    private Vector2 spawnPoint;
    private float attackTimer;
    private float chargeRecoveryDelay = 5f;

    protected override void Start()
    {
        base.Start();
        spawnPoint = transform.position;
    }

    protected override void Update()
    {
        base.Update();
        if (healthSystem.CurrentMHealth <= healthSystem.MonsterMaxHealth * 0.5f && !isAngry)
        {
            isAngry = true;
            chargesLeft = chargeCount;
            StartCoroutine(PerformGroundSlamCharges());
        }

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
        }
    }

    protected override void IdleBehavior()
    {
        WanderAroundSpawnPoint();

        if (Vector2.Distance(transform.position, player.transform.position) <= statHandler.CurrentMonsterStats.followDistance)
        {
            currentState = State.Chase;
        }
    }

    protected override void ChasePlayer()
    {
        MoveTowardsPlayer();

        if (Vector2.Distance(transform.position, player.transform.position) <= statHandler.CurrentMonsterStats.attackRange)
        {
            currentState = State.Attack;
            attackTimer = attackCooldown;
        }
    }

    protected override void AttackPlayer()
    {
        if (attackTimer <= 0)
        {
            animator.SetTrigger("Attack");
            StartCoroutine(AttackCooldown());
        }
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        if (Vector2.Distance(transform.position, player.transform.position) <= statHandler.CurrentMonsterStats.attackRange)
        {
            AttackPlayer();
        }
        else
        {
            currentState = State.Chase;
        }
    }

    IEnumerator PerformGroundSlamCharges()
    {
        Collider2D collider = GetComponent<Collider2D>();


        while (chargesLeft > 0)
        {
            Vector2 chargeDirection = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
            animator.SetTrigger("Move");

            float chargeTime = 2.0f;
            for (float timer = 0; timer < chargeTime; timer += Time.deltaTime)
            {
                transform.position += (Vector3)chargeDirection * chargeSpeed * Time.deltaTime;
                yield return null;
            }

            chargesLeft--;
            yield return new WaitForSeconds(2.0f);
        }


        yield return new WaitForSeconds(3.0f);
        chargesLeft = chargeCount;
        if (isAngry)
        {
            StartCoroutine(PerformGroundSlamCharges());
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            float damageAmount = 30f;
            Debug.Log("¹âÇû´Ù");
            other.gameObject.GetComponent<HealthSystem>().ChangeHealth(-damageAmount);
        }
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        
        ItemManager.instance.itemPool.ItemSpawn(1802, transform.position);
        ItemManager.instance.itemPool.ItemSpawn(1902, transform.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            float damageAmount = 30f;
            collision.gameObject.GetComponent<HealthSystem>().ChangeHealth(-damageAmount);
        }
    }

    private void WanderAroundSpawnPoint()
    {
        Vector2 wanderTarget = spawnPoint + Random.insideUnitCircle * wanderRadius;
        transform.position = Vector2.MoveTowards(transform.position, wanderTarget, statHandler.CurrentMonsterStats.speed * Time.deltaTime);
    }

    private void MoveTowardsPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, statHandler.CurrentMonsterStats.speed * Time.deltaTime);
    }
}