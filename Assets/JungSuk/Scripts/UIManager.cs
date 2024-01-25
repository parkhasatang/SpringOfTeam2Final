using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject Player;
    [SerializeField] private Slider HPSlider;
    [SerializeField] private Slider HungerSilder;

    [SerializeField] private TextMeshProUGUI HpTxt;
    [SerializeField] private TextMeshProUGUI HungerTxt;

    private HealthSystem playerHealthSystem;

    // 인벤토리 데이터
    public Inventory playerInventoryData;
    public Item takeTemporaryItemData;
    public Item giveTemporaryItemData;
    public Sprite temporaryItemImg;
    public int takeTemporaryItemStack;
    public int giveTemporaryItemStack;

    private void Awake()
    {
        Instance = this;

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
}
