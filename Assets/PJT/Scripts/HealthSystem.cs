using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = .5f;

    private CharacterStatHandler _statusHandler;
    private float _healthLastChange = float.MaxValue;

    public float CurrentHealth { get; private set; }
    public float MaxHealth => _statusHandler.CurrentStats.maxHP;

    public event Action OnDamage;
    public event Action OnHeal;
    public event Action OnDeath;

    private void Awake()
    {
        _statusHandler = GetComponent<CharacterStatHandler>();
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    private void Update()
    {
        if (_healthLastChange < healthChangeDelay)
        {
            _healthLastChange += Time.deltaTime;
        }
    }

    public bool ChangeHealth(float change)
    {
        if (change == 0 || _healthLastChange < healthChangeDelay) return false;

        _healthLastChange = 0f;
        CurrentHealth += change;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        if (change < 0 && CurrentHealth > 0)
        {
            OnDamage?.Invoke();
        }
        else if (change > 0)
        {
            OnHeal?.Invoke();
        }

        if (CurrentHealth <= 0f)
        {
            OnDeath?.Invoke();
        }
        return true;
    }
}
