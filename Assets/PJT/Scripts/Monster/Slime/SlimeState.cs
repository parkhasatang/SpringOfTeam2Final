using UnityEngine;
using System.Collections.Generic;

public class SlimeState : MonsterState
{
    [SerializeField] private float itemDetectionRange = 5.0f;
    private List<int> swallowedItems = new List<int>();
    private Vector2 currentDirection;
    private float moveTimer;
    private float moveDuration = 2f;
    private float lastAttackTime;
    private Collider2D myCollider;
    private CharacterStatHandler stathandler;

    protected override void Start()
    {
        base.Start();
        myCollider = GetComponent<Collider2D>();
        stathandler = GetComponent<CharacterStatHandler>();
        healthSystem.OnDeath += HandleMonsterDeath;

        if (stathandler == null)
        {
            Debug.LogError("스탯핸들러가 없습니다.");
        }
        else
        {
            stathandler.UpdateCharacterStats();
        }
        ChooseNewDirection();
    }

    protected override void Update()
    {
        base.Update();
        DetectAndSwallowItems();

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
        }
    }

    protected override void IdleBehavior()
    {
        if (moveTimer >= moveDuration)
        {
            ChooseNewDirection();
            currentState = State.Move;
        }
        moveTimer += Time.deltaTime;
    }

    protected override void MoveBehavior()
    {
        if (currentDirection != Vector2.zero && moveTimer < moveDuration)
        {
            transform.position += (Vector3)currentDirection * stathandler.CurrentMonsterStats.speed * Time.deltaTime;
        }
        else
        {
            moveTimer = 0f;
            ChooseNewDirection();
        }
        moveTimer += Time.deltaTime;

        if (PlayerInDetectionRange())
        {
            currentState = State.Chase;
        }
    }

    protected override void ChasePlayer()
    {
        if (PlayerInAttackRange())
        {
            currentState = State.Attack;
        }
        else if (PlayerInDetectionRange())
        {
            var direction = (player.transform.position - transform.position).normalized;
            transform.position += (Vector3)direction * stathandler.CurrentMonsterStats.speed * Time.deltaTime;
        }
        else
        {
            currentState = State.Idle;
        }
    }

    protected override void AttackPlayer()
    {
        if (Time.time - lastAttackTime >= stathandler.CurrentMonsterStats.attackDelay)
        {
            Debug.Log("Attacking player");
            lastAttackTime = Time.time;
        }
        if (!PlayerInAttackRange())
        {
            currentState = PlayerInDetectionRange() ? State.Chase : State.Idle;
        }
    }

    private bool PlayerInDetectionRange()
    {
        return Vector2.Distance(transform.position, player.transform.position) <= stathandler.CurrentMonsterStats.followDistance;
    }

    private bool PlayerInAttackRange()
    {
        return Vector2.Distance(transform.position, player.transform.position) <= stathandler.CurrentMonsterStats.attackRange;
    }

    private void ChooseNewDirection()
    {
        currentDirection = Random.insideUnitCircle.normalized;
        moveTimer = 0f;
    }

    private void DetectAndSwallowItems()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(transform.position, itemDetectionRange);
        foreach (Collider2D detectedObject in detectedObjects)
        {
            if (detectedObject == myCollider || !detectedObject.CompareTag("Item"))
                continue;

            SwallowItem(detectedObject.gameObject);
        }
    }

    private void SwallowItem(GameObject item)
    {
        PickUp pickUp = item.GetComponent<PickUp>();
        if (pickUp != null)
        {
            swallowedItems.Add(pickUp.itemCode);
            Destroy(item);
        }
    }

    private void HandleMonsterDeath()
    {
        if (ItemManager.instance == null || ItemManager.instance.itemPool == null)
        {
            Debug.LogWarning("Null Null해요");
            return;
        }

        Vector3 originalPosition = transform.position;
        ItemManager.instance.itemPool.ItemSpawn(3101, originalPosition);
        ItemManager.instance.itemPool.ItemSpawn(3011, originalPosition);
        foreach (int itemCode in swallowedItems)
        {
            ItemManager.instance.itemPool.ItemSpawn(itemCode, originalPosition);
        }
    }
}