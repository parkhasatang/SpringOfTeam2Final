using System.Collections.Generic;
using UnityEngine;

public class SlimeState : MonsterState
{
    [SerializeField] private float itemDetectionRange = 5.0f;
    private List<int> swallowedItems = new List<int>();
    private Vector2 currentDirection;
    private float moveTimer;
    private float moveDuration = 2f;
    private Collider2D myCollider;
    private CharacterStatHandler stathandler;

    protected override void Start()
    {
        base.Start();
        myCollider = GetComponent<Collider2D>();
        stathandler = GetComponent<CharacterStatHandler>();
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
        DetectAndSwallowItems();
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
        base.ChasePlayer();
    }

    protected override void AttackPlayer()
    {
        base.AttackPlayer();
    }
    protected override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

    }
    protected override void OnDeath()
    {
        base.OnDeath();
        DropSwallowedItems();
    }

    private void MoveInRandomDirection()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        transform.position += (Vector3)randomDirection * stathandler.CurrentMonsterStats.speed * Time.deltaTime;
    }

    private void DetectAndSwallowItems()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(transform.position, itemDetectionRange);
        foreach (Collider2D detectedObject in detectedObjects)
        {
            if (detectedObject == myCollider)
                continue;

            if (detectedObject.CompareTag("Item"))
            {
                SwallowItem(detectedObject.gameObject);
            }
            else
            {
                Debug.Log("아니네");
                continue;
            }
        }
    }

    private void SwallowItem(GameObject item)
    {
        PickUp pickUp = item.GetComponent<PickUp>();
        if (pickUp != null)
        {
            swallowedItems.Add(pickUp.itemIndex);
            Destroy(item);
        }
    }

    private void DropSwallowedItems()
    {
        foreach (var itemCode in swallowedItems)
        {
            ItemManager.instance.itemPool.ItemSpawn(itemCode, transform.position);
        }
        swallowedItems.Clear();
    }
}