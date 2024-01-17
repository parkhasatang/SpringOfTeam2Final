using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipObject : MonoBehaviour
{
    private CharacterController controller;
    public GameObject[] quitSlots;
    public GameObject heldItem;
    public Transform SpawnTrans;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        controller.OnEquipEvent += EquipItemInQuitSlot;

    }

    private void EquipItemInQuitSlot()
    {
        for (int i = 1; i <= 8; i++)
        {
            KeyCode key = KeyCode.Alpha0 + i;

            if (Input.GetKeyDown(key))
            {
                Debug.Log(i);
                Vector3 currentPostion = heldItem.transform.position;
                Quaternion currentRotation = heldItem.transform.rotation;
                

                GameObject newGameObj = Instantiate(quitSlots[i - 1], currentPostion, currentRotation, SpawnTrans);

                heldItem.SetActive(false);
            }
        }
    }
            // Update is called once per frame
     private void Update()
     {
        EquipItemInQuitSlot();
     }
        
    }