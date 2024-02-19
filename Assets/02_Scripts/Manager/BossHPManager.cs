using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHPManager : MonoBehaviour
{
    [SerializeField] private GameObject BossHPUI;
    [SerializeField] private Slider BossHPBar;
    [SerializeField] private Slider BossEaseBar;
    [SerializeField] private TextMeshProUGUI BossName;
    private GameObject SlimeBoss;
    private HealthSystem bossHealthSystem;
    private float lerpSpeed = 0.05f;
    private float CurrentHealth;
    private float MaxHealth;
    // Start is called before the first frame update
    void Start()
    {
        SlimeBoss = GameObject.FindGameObjectWithTag("Boss");
        bossHealthSystem = SlimeBoss.GetComponent<HealthSystem>();
        BossHPBar.value = bossHealthSystem.MonsterMaxHealth;
        BossName.text = SlimeBoss.name;
        bossHealthSystem.OnDamage += OnDamageBoss;
        bossHealthSystem.OnDeath += BossHPZero;

        float CurrentHealth = bossHealthSystem.CurrentMHealth;
        float MaxHealth = bossHealthSystem.MonsterMaxHealth;
        BossEaseBar.value = 1;
        BossHPBar.value = CurrentHealth / MaxHealth;
    }

    private void Update()
    {
        OnChangeBossHP();
    }
    private void BossHPZero()
    {
        BossHPUI.SetActive(false);
        // 리젠 하면 리젠 시간에 따라 다시 HP 설정해주기
    }

    // Update is called once per frame    

    private void OnDamageBoss()
    {
        BossHPUI.SetActive(true);
    }
    private void OnChangeBossHP()
    {
        float CurrentHealth = bossHealthSystem.CurrentMHealth;
        float MaxHealth = bossHealthSystem.MonsterMaxHealth;        
        BossHPBar.value = CurrentHealth / MaxHealth;
        if (BossEaseBar.value != BossHPBar.value)
        {
            BossEaseBar.value = Mathf.Lerp(BossEaseBar.value, CurrentHealth / MaxHealth, lerpSpeed);
        }
    }


}
