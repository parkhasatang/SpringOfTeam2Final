using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnionState : MonsterState
{
    private CharacterStatHandler stathandler;

    private void Start()
    {
        base.Start();
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
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        HandleMonsterDeath();
        Destroy(gameObject);
    }

    private void HandleMonsterDeath()
    {
        Vector3 originalPosition = transform.position;
        if (ItemManager.instance != null && ItemManager.instance.itemPool != null)
        {
            ItemManager.instance.itemPool.ItemSpawn(1713, originalPosition);
            ItemManager.instance.itemPool.ItemSpawn(1723, originalPosition);
        }
        else
        {
            Debug.LogWarning("ItemManager 또는 itemPool이 null입니다. 아이템을 드랍할 수 없습니다.");
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