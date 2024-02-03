using UnityEngine;
using System.Collections.Generic;

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
        healthSystem.OnDeath += HandleMonsterDeath;
        if (stathandler == null)
        {
            Debug.LogError("½ºÅÈÇÚµé·¯°¡¾ø»ï");
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

    protected override void OnDeath()
    {
        base.OnDeath();
        HandleMonsterDeath();
    }

    private void HandleMonsterDeath()
    {
        Vector3 originalPosition = transform.position;
        if (ItemManager.instance != null && ItemManager.instance.itemPool != null)
        {
            foreach (int itemCode in swallowedItems)
            {
                ItemManager.instance.itemPool.ItemSpawn(itemCode, originalPosition);
            }
        }
        else
        {
            Debug.LogWarning("ItemManager or itemPool is null.");
        }
    }
}
