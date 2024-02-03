using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBossState : BossState
{
    private Vector2 currentDirection;
    private Collider2D myCollider;
    private CharacterStatHandler stathandler;
    private float moveTimer;
    private float moveDuration = 2f;
    private float chaseMaxDistance = 10f;
    private float jumpHeight = 5f;
    private float jumpAttackCooldown = 5f;
    private float jumpDuration = 1.5f;
    private bool canJumpAttack = true;
    


    protected override void Start()
    {
        base.Start();
        myCollider = GetComponent<Collider2D>();
        stathandler = GetComponent<CharacterStatHandler>();
        healthSystem.OnDeath += HandleMonsterDeath;
        if (stathandler == null)
        {
            Debug.LogError("스탯핸들러가없삼");
        }
        else
        {
            stathandler.UpdateCharacterStats();
        }
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void IdleBehavior()
    {
        if (PlayerInDetectionRange())
        {
            currentState = State.Chase;
            Debug.Log("현재상태: " + currentState);
            return;
        }

        if (!PlayerInDetectionRange())
        {
            currentState = State.Move;
            Debug.Log("현재상태: " + currentState);
            return;
        }
    }

    protected override void MoveBehavior()
    {
        if (currentDirection == Vector2.zero)
        {
            currentDirection = Random.insideUnitCircle.normalized;
            moveTimer = 0f;
        }

        transform.position += (Vector3)currentDirection * stathandler.CurrentMonsterStats.speed * Time.deltaTime;

        moveTimer += Time.deltaTime;
        if (moveTimer >= moveDuration)
        {
            currentDirection = Vector2.zero;
        }

        if (PlayerInDetectionRange())
        {
            currentState = State.Chase;
            Debug.Log("플레이어발견으로인한 상태변경: " + currentState);
        }
    }

    private bool PlayerInDetectionRange()
    {
        if (Vector2.Distance(transform.position, player.transform.position) <= statHandler.CurrentMonsterStats.followDistance)
        {
            Debug.Log("플레이어 발견");
            return true;
        }
        return false;
    }

    protected override void ChasePlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= stathandler.CurrentMonsterStats.followDistance)
        {
            if (canJumpAttack)
            {
                StartCoroutine(JumpAttack(jumpHeight, OnJumpAttackLanded));
                canJumpAttack = false;
                StartCoroutine(StartJumpAttackCooldown());
            }
        }
        else if (distanceToPlayer > chaseMaxDistance)
        {
            Debug.Log("플레이어가 너무 멀어져서 추격 중단");
            currentState = State.Idle;
        }
    }
    private IEnumerator StartJumpAttackCooldown()
    {
        yield return new WaitForSeconds(jumpAttackCooldown);
        canJumpAttack = true;
    }

    protected override void AttackPlayer()
    {

    }

    protected override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        HandleMonsterDeath();
    }
    private void HandleMonsterDeath()
    {
        Vector3 originalPosition = transform.position;
        if (ItemManager.instance != null && ItemManager.instance.itemPool != null)
            ItemManager.instance.itemPool.ItemSpawn(2101, originalPosition);
    }
private IEnumerator JumpAttack(float jumpHeight, System.Action onLanded)
    {
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = player.transform.position;
        float elapsedTime = 0;

        while (elapsedTime < jumpDuration)
        {
            float height = Mathf.Sin(Mathf.PI * elapsedTime / jumpDuration) * jumpHeight;
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / jumpDuration) + Vector3.up * height;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        onLanded?.Invoke();
    }

    private void OnJumpAttackLanded()
    {
        float jumpAttackDamage = 50f;
        float damageRadius = myCollider.bounds.size.x;

        HealthSystem playerHealth = player.GetComponent<HealthSystem>();

        if (playerHealth != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

            if (distanceToPlayer <= damageRadius)
            {
                playerHealth.ChangeHealth(-jumpAttackDamage);
                Debug.Log("점프 데미지 받았다.");
            }
        }
    }
}
