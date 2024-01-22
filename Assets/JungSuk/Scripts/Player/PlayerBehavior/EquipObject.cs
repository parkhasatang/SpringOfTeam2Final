using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipObject : MonoBehaviour
{
    private CharacterController controller;
    public GameObject[] quitSlots; // 민규님이 만들어 주실 큇슬롯으로 변경
    public GameObject heldItem;
    public Transform SpawnTrans;
    private CharacterStatHandler statHandler;

    private bool IsEquipedItem = false;

    private int equippedSlotIndex = -1;

    private void Awake()
    {        
        controller = GetComponent<CharacterController>();
        statHandler = GetComponent<CharacterStatHandler>();
    }
    // Start is called before the first frame update
    void Start()
    {
        controller.OnEquipEvent += EquipItemInQuitSlot;
    }

    // Update is called once per frame
    private void Update()
    {
        EquipItemInQuitSlot();
    }

    private void EquipItemInQuitSlot()
    {
        for (int i = 1; i <= 8; i++)
        {           
                KeyCode key = KeyCode.Alpha0 + i;

                if (Input.GetKeyDown(key))
                {
                    if (quitSlots[i - 1] != null)
                    {
                         equippedSlotIndex = i - 1;
                        if (heldItem == null)
                        {
                            EquipItem(i - 1);
                        }
                        else
                        {   
                            UnequipItem();
                            EquipItem(i - 1);
                        }
                    }
                    else
                    {
                        Debug.Log("등록된 아이템이 없습니다.");
                        return;
                    }
                }
            }
    }

    private void EquipItem(int slotIndex)
    {
        Vector3 currentPosition = SpawnTrans.position;
        Quaternion currentRotation = SpawnTrans.rotation;

        heldItem = Instantiate(quitSlots[slotIndex], currentPosition, currentRotation, SpawnTrans);
        IsEquipedItem = true;
       //ChangeStatByItem(slotIndex);
        
    }

    private void UnequipItem()
    {
        if(heldItem != null)
        {
            Destroy(heldItem);
            IsEquipedItem = false;
        }
    }

    private void ChangeStatByItem(int Index)
    {
        if (UIManager.Instance.AllItemList[Index - 1].ItemType == "WeaponRange")
        {
            if (IsEquipedItem == true)
            {
                statHandler.CurrentStats.attackDamage += float.Parse(UIManager.Instance.AllItemList[Index - 1].AttackDamage);
                Debug.Log(statHandler.CurrentStats.attackDamage);
            }
            else
                return;
        }
        else
            return;        
    }
}