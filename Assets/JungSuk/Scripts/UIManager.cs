using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject Player;
    [SerializeField] private Slider HPSlider;
    [SerializeField] private Slider HungerSilder;

    [SerializeField] private TextMeshProUGUI HpTxt;
    [SerializeField] private TextMeshProUGUI HungerTxt;

    private string Traget = "Player";
    private HealthSystem playerHealthSystem;

    private void Awake()
    {
        playerHealthSystem = Player.GetComponent<HealthSystem>();
        playerHealthSystem.OnDamage += UpdateUI;
        playerHealthSystem.OnHeal += UpdateUI;
    }

    private void UpdateUI()
    {
        HPSlider.value = playerHealthSystem.CurrentHealth / playerHealthSystem.MaxHealth;
        HpTxt.text = playerHealthSystem.CurrentHealth.ToString() + "/" + playerHealthSystem.MaxHealth.ToString();

        HungerSilder.value = playerHealthSystem.CurrentHunger / playerHealthSystem.MaxHunger;
        HungerTxt.text = playerHealthSystem.CurrentHunger.ToString() + "/" + playerHealthSystem.MaxHealth.ToString();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
