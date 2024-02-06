using System;
using System.Collections;
using Unity.VisualScripting;
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
    public float MonsterMaxHealth => _statusHandler.CurrentMonsterStats.maxHP;
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
        CurrentMHealth = MonsterMaxHealth;
        StartCoroutine(ChangeCurrentHunger(-2));
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

    public void ChangeHunger(float change)
    {               
        CurrentHunger += change;
        CurrentHunger = Mathf.Clamp(CurrentHunger, 0, MaxHunger);               
    }

    IEnumerator ChangeCurrentHunger(float change)
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            ChangeHunger(change);
            Debug.Log(change);
            Debug.Log(CurrentHunger);
        }
    }
    public bool ChangeMHealth(float change)
    {
        if (change == 0 || _healthLastChange < healthChangeDelay) return false;

        _healthLastChange = 0f;
        CurrentMHealth += change;
        CurrentMHealth = Mathf.Clamp(CurrentMHealth, 0, MonsterMaxHealth);

        if (change < 0 && CurrentMHealth > 0)
        {
            OnDamage?.Invoke();
            Debug.Log("데미지를 입었다");
        }
        else if (change > 0)
        {
            OnHeal?.Invoke();
        }

        if (CurrentMHealth <= 0f)
        {
            OnDeath?.Invoke();
            Destroy(gameObject);
            Debug.Log("죽었다.");
        }
        return true;
    }   
}
