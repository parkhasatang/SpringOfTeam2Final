using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnionState : MonsterState
{
    protected override void Start()
    {
        animator = GetComponent<Animator>();
        currentState = State.Idle;
        healthSystem = GetComponent<HealthSystem>();

        statHandler = GetComponent<CharacterStatHandler>();
        if (statHandler == null)
        {
            statHandler = gameObject.AddComponent<CharacterStatHandler>();
        }

        statHandler.UpdateCharacterStats();
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
        ItemManager.instance.itemPool.ItemSpawn(1723, originalPosition);
        ItemManager.instance.itemPool.ItemSpawn(1713, originalPosition);
        Debug.Log("À¸¾Æ¾Ç");
    }
}