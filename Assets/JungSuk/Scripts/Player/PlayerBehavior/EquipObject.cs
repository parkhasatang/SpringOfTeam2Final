using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipObject : MonoBehaviour
{
    private CharacterController controller;
    public Image[] quitSlots;
    public SpriteRenderer heldItem;
    public Transform SpawnTrans;
    private CharacterStatHandler statHandler;

    private Inventory inventory;

    private void Awake()
    {        
        controller = GetComponent<CharacterController>();
        statHandler = GetComponent<CharacterStatHandler>();
        inventory = GetComponent<Inventory>();
    }
    
    void Start()
    {
        controller.OnEquipEvent += SelectItemInQuikSlot;
    }

    private void SelectItemInQuikSlot()
    {
        for (int i = 0; i < 8; i++)
        {
            inventory.invenSlot[i].QuickSlotItemChoose(false);
            inventory.slots[i].isChoose = false;
        }

        for (int i = 1; i <= 8; i++)
        {
            KeyCode key = KeyCode.Alpha0 + i;            
            if (Input.GetKeyDown(key))
            {
                if (inventory.slots[i - 1].isChoose == false) // isChoose로 두번 눌러도 안에 있는 메서드는 실행안댐.
                {
                    EquipItem(i - 1); // 아이템 들기
                    break;
                }
                // 여기 else를 써주면 같은 키를 두번 눌렀을 때 실행됌.
            }
            /*else
            {
                inventory.slots[i - 1].isChoose = false;
                inventory.invenSlot[i - 1].QuickSlotItemChoose(false);
            }*/
        }
    }

    private void EquipItem(int slotIndex)
    {
        heldItem.sprite = quitSlots[slotIndex].sprite;
        inventory.slots[slotIndex].isChoose = true;
        inventory.invenSlot[slotIndex].QuickSlotItemChoose(true);
        
    }

    private void UnEquipItem(int slotIndex)// todo 기존에 있던 아이템 정보를 빼고넣기.
    {/*
        heldItem.sprite = null;
        inventory.invenSlot[slotIndex].QuickSlotItemChoose(false);*/
    }


    /*private void ChangeStatByItem(int Index)
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
    }*/
}