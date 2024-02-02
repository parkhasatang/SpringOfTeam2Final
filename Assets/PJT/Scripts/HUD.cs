using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    Slider myslider;
    HealthSystem healthSystemInstance;

    private void Awake()
    {
        myslider = GetComponent<Slider>();
        healthSystemInstance = FindObjectOfType<HealthSystem>();
    }

    private void LateUpdate()
    {
        float maxhp = healthSystemInstance.MaxHealth;
        float curhp = healthSystemInstance.CurrentHealth;
        myslider.value = curhp / maxhp;
    }
}