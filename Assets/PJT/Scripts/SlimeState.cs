using System.Collections.Generic;
using UnityEngine;

public class SlimeState : MonsterState
{
    [SerializeField] private float itemDetectionRange = 1.0f;
    private List<GameObject> swallowedItems = new List<GameObject>();
    private float idleMoveTime;
    private float idleChangeDirectionTime;
    protected override void Start()
    {
        base.Start();
        // 슬라임 초기화 로직
    }

    protected override void Update()
    {
        base.Update();
        // 슬라임 특화 업데이트 로직
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

    }

    protected override void AttackPlayer()
    {

    }

    private void OnDeath()
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

    }
    private void SwallowItem(GameObject item)
    {
        swallowedItems.Add(item);
        Destroy(item);
    }
    private void DropSwallowedItems()
    {
        foreach (var item in swallowedItems)
        {

        }
        swallowedItems.Clear();
    }
}
