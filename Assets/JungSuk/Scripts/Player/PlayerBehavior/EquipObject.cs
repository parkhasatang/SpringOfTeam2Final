using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipObject : MonoBehaviour
{
    private CharacterController controller;
    public GameObject[] quitSlots;
    public GameObject heldItem;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        controller.OnEquipEvent += EquipItemInQuitSlot;
    }

    private void EquipItemInQuitSlot(int obj)
    {
        Debug.Log(obj + "¿‘¥œ¥Ÿ.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
