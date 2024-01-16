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
        
    }

    // Start is called before the first frame update
    private void Start()
    {
        controller.OnAttackEvent += PlayerAttack;
    }

    private void PlayerAttack()
    {
        Debug.Log("°ø°ÝÀÌ´å");
    }
}
