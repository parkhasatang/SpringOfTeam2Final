using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnionState : MonsterState
{   
    private Collider2D myCollider;
    private CharacterStatHandler stathandler;

    private void Start()
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
            ItemManager.instance.itemPool.ItemSpawn(1713, originalPosition);
        ItemManager.instance.itemPool.ItemSpawn(1723, originalPosition);
    }
}