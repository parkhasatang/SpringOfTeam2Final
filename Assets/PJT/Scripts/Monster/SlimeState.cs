using System.Collections.Generic;
using UnityEngine;

public class SlimeState : MonsterState
{
    [SerializeField] private float itemDetectionRange = 5.0f;
    private List<int> swallowedItems = new List<int>();
    private float idleMoveTime;
    private float idleChangeDirectionTime;
    private Collider2D myCollider;

    protected override void Start()
    {
        base.Start();
        myCollider = GetComponent<Collider2D>();
    }

    protected override void Update()
    {
        base.Update();
        DetectAndSwallowItems();
    }

    protected override void IdleBehavior()
    {
        idleMoveTime += Time.deltaTime;

        if (idleMoveTime >= idleChangeDirectionTime)
        {
            idleMoveTime = 0;
            idleChangeDirectionTime = Random.Range(2f, 5f);
            MoveInRandomDirection();
        }
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
        transform.position += (Vector3)randomDirection * monsterStats.speed * Time.deltaTime;
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
                Debug.Log("¾Æ´Ï³×");
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