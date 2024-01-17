using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetObject : MonoBehaviour
{
    private CharacterController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    // Start is called before the first frame update
    private void Start()
    {
        controller.OnSetEvent += BuildObject;
    }

    private void BuildObject()
    {
        Debug.Log("∫ÙµÂ ø¿∫Í¡ß∆Æ¿Ã¥Â");
    }

}
