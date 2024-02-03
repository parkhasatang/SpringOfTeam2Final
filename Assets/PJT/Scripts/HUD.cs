using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider hungerSlider;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI hungerText;

    private HealthSystem healthSystemInstance;
    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            healthSystemInstance = player.GetComponent<HealthSystem>();
        }
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