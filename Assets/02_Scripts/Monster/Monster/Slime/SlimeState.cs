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
    protected override void OnDeath()
    {
        base.OnDeath();
        HandleMonsterDeath();
    }

    private void HandleMonsterDeath()
    {
        if (gameObject == null || !gameObject.activeInHierarchy)
        {
            Debug.LogWarning("GameObject is already destroyed or inactive.");
            return;
        }

        DropRandomItem();
        DropSwallowedItems();
    }

    private void DropRandomItem()
    {
        if (ItemManager.instance == null || ItemManager.instance.itemPool == null)
        {
            Debug.LogWarning("ItemManager or itemPool is null.");
            return;
        }

        int[] itemCodes = new int[] { 3101, 3011 };
        int selectedItemCode = itemCodes[Random.Range(0, itemCodes.Length)];
        ItemManager.instance.itemPool.ItemSpawn(selectedItemCode, transform.position);
    }

    private void DropSwallowedItems()
    {
        if (ItemManager.instance == null || ItemManager.instance.itemPool == null)
        {
            Debug.LogWarning("ItemManager or itemPool is null, cannot drop swallowed items.");
            return;
        }

        foreach (int itemCode in swallowedItems)
        {
            ItemManager.instance.itemPool.ItemSpawn(itemCode, transform.position);
        }
    }

    private void OnDestroy()
    {
        if (healthSystem != null)
        {
            healthSystem.OnDeath -= HandleMonsterDeath;
        }
    }
}
