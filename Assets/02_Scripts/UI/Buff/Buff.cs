using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buff : MonoBehaviour
{    
    public GameObject noHungerUI;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        HealthSystem healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnZeroHunger += SetBuffUI;
    }

    private void SetBuffUI()
    {
        noHungerUI.SetActive(noHungerUI.activeSelf);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
