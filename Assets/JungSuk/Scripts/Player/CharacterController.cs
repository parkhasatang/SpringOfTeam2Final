using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    protected CharacterStatHandler statsHandler { get; private set; }

    public event Action<Vector2> OnMoveEvent;
    public event Action<Vector2> OnLookEvent;
    public event Action OnAttackEvent;
    public event Action OnSetEvent;
    public event Action OnInteractEvent;

    private float timeSinceLastAttack = float.MaxValue;
    public bool IsAttacking { get; set; }
    public bool IsSetting { get; set; }

    public bool IsInteracting { get; set; }

    protected virtual void Awake()
    {
        statsHandler = GetComponent<CharacterStatHandler>();
    }
    protected virtual void Update()
    {
        AttackDelay();
        CanInteract();
        CanSet();
    }

    private void AttackDelay() // 공격 딜레이 효과 
    {
        if(timeSinceLastAttack <= statsHandler.CurrentStats.playerBaseStatsSO.attackDelay)
        {
            timeSinceLastAttack += Time.deltaTime;
        }
        if(IsAttacking && timeSinceLastAttack > statsHandler.CurrentStats.playerBaseStatsSO.attackDelay)
        {
            timeSinceLastAttack = 0;
            CallAttackEvent();
        }
    }

    private void CanInteract() // 여기서 상호작용 가능 오브젝트 체크하고 거리 설정까지
    {
        if (IsInteracting)
        {
            CallInteractEvent();
        }
    }

    private void CanSet() // 여기서 설치 가능한 오브젝트 조건 등 처리 해야함
    {
        if (IsSetting)
        {
            CallSetEvent();
        }
    }

    public void CallMoveEvent(Vector2 value)
    {
        OnMoveEvent?.Invoke(value);
    }

    public void CallLookEvent(Vector2 value)
    {
        OnLookEvent?.Invoke(value);
    }

    public void CallAttackEvent()
    {
        OnAttackEvent?.Invoke();
    }

    public void CallSetEvent()
    {
        OnSetEvent?.Invoke();
    }

    public void CallInteractEvent()
    {
        OnInteractEvent?.Invoke();
    }
}
