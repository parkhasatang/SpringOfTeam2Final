using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private CharacterController controller;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        controller.OnAttackEvent += OnAttack;
    }

    private void OnAttack()
    {
        
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
