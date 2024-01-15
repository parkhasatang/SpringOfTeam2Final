using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    protected CharacterStatHandler statsHandler {  get; private set; }

    public event Action<Vector2> OnMoveEvent;
    public event Action<Vector2> OnLookEvent;
    public event Action OnAttackEvent;
    public event Action OnSetEvent;
    public event Action OnInteractEvent;
    public bool IsAttacking { get; set; }
    public bool IsSetting {  get; set; }

    protected virtual void Awake()
    {
       statsHandler = GetComponent<CharacterStatHandler>();
    }
    protected virtual void Update()
    {
        AttackDelay();
    }

    private void AttackDelay()
    {
        if(statsHandler.CurrentStats.playerBaseStatsSO.attackDelay <= 0.1)
        {

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
