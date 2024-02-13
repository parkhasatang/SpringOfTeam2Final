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
    [SerializeField] private TextMeshProUGUI BossName;
    private GameObject SlimeBoss;
    private HealthSystem bossHealthSystem;
    // Start is called before the first frame update
    void Start()
    {
        SlimeBoss = GameObject.FindGameObjectWithTag("Boss");
        bossHealthSystem = SlimeBoss.GetComponent<HealthSystem>();
        BossHPBar.value = bossHealthSystem.MonsterMaxHealth;
        BossName.text = SlimeBoss.name;
        bossHealthSystem.OnDamage += BossHPChanger;
        bossHealthSystem.OnDeath += BossHPZero;
    }

    private void BossHPZero()
    {
        BossHPUI.SetActive(false);
        // 리젠 하면 리젠 시간에 따라 다시 HP 설정해주기
    }

    // Update is called once per frame    

    private void BossHPChanger()
    {
        Debug.Log(BossHPBar.value + "남았습니다 공격을 지속하세요");
        BossHPUI.SetActive(true);
        BossHPBar.value = bossHealthSystem.CurrentMHealth/bossHealthSystem.MonsterMaxHealth;
    }

}
