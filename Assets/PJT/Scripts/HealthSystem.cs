using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = .5f;

    private CharacterStatHandler _statusHandler;
    private float _healthLastChange = float.MaxValue;

    public float CurrentHealth { get; private set; }
    public float CurrentMHealth { get; private set; }
    public float CurrentHunger { get; private set; }
    public float MaxHealth => _statusHandler.CurrentStats.maxHP;
    public float MosterMaxHealth => _statusHandler.CurrentMonsterStats.maxHP;
    public float MaxHunger => _statusHandler.CurrentStats.specificSO.hunger;

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
        CurrentHunger = MaxHunger;
        CurrentMHealth = MosterMaxHealth;
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
    public bool ChangeMHealth(float change)
    {
        if (change == 0 || _healthLastChange < healthChangeDelay) return false;

        _healthLastChange = 0f;
        CurrentMHealth += change;
        CurrentMHealth = Mathf.Clamp(CurrentMHealth, 0, MaxHealth);

        if (change < 0 && CurrentMHealth > 0)
        {
            OnDamage?.Invoke();
        }
        else if (change > 0)
        {
            OnHeal?.Invoke();
        }

        if (CurrentMHealth <= 0f)
        {
            OnDeath?.Invoke();
        }
        return true;
    }
}
