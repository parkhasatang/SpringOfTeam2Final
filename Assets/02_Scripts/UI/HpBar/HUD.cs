using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
using System;
using Unity.VisualScripting;

public class HUD : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider hungerSlider;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI hungerText;
    [SerializeField] private GameObject noHungerUI;
    private HealthSystem healthSystemInstance;
    private CharacterStatHandler statHandler;
    private float speed1;
    private float speed2;
    
    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            healthSystemInstance = player.GetComponent<HealthSystem>();
            statHandler = player.GetComponent<CharacterStatHandler>();
        }        
    }

    private void Start()
    {
        speed1 = statHandler.CurrentStats.speed / 3 * 2;
        speed2 = statHandler.CurrentStats.speed;
        healthSystemInstance.OnZeroHunger += SetDeBuffUI;
        healthSystemInstance.OnNoZeroHunger += CancleBuffUI;
        
    }

    private void CancleBuffUI()
    {
        noHungerUI.SetActive(false);
        statHandler.CurrentStats.speed = speed2;
    }

    private void SetDeBuffUI()
    {        
        noHungerUI.SetActive(true);
        statHandler.CurrentStats.speed = speed1;        
    }

    private void LateUpdate()
    {
        if (healthSystemInstance == null) return;

        UpdateHealthSlider();
        UpdateHungerSlider();
        UpdateHealthText();
        UpdateHungerText();
    }

    private void UpdateHealthSlider()
    {
        if (healthSlider != null && healthSystemInstance != null)
        {
            float maxHealth = healthSystemInstance.MaxHealth;
            float currentHealth = healthSystemInstance.CurrentHealth;
            healthSlider.value = currentHealth / maxHealth;
        }
    }

    private void UpdateHungerSlider()
    {
        if (hungerSlider != null && healthSystemInstance != null)
        {
            float maxHunger = healthSystemInstance.MaxHunger;
            float currentHunger = healthSystemInstance.CurrentHunger;
            hungerSlider.value = currentHunger / maxHunger;
        }
    }

    private void UpdateHealthText()
    {
        if (healthText != null && healthSystemInstance != null)
        {
            float maxHealth = healthSystemInstance.MaxHealth;
            float currentHealth = healthSystemInstance.CurrentHealth;
            healthText.text = $"{currentHealth}/{maxHealth}";
        }
    }

    private void UpdateHungerText()
    {
        if (hungerText != null && healthSystemInstance != null)
        {
            float maxHunger = healthSystemInstance.MaxHunger;
            float currentHunger = healthSystemInstance.CurrentHunger;
            hungerText.text = $"{currentHunger}/{maxHunger}";
        }
    }


}