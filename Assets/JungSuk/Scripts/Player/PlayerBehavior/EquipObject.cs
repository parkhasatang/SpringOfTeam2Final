using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class EquipObject : MonoBehaviour, IEquipedItem
{
    private CharacterController controller;
    public Image[] quitSlots;
    public SpriteRenderer heldItem;
    public Transform SpawnTrans;
    private CharacterStatHandler statHandler;

    private Inventory inventory;

    private int selectedIndexNum = 1;

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
            if (inventory.slots[selectedIndexNum].item != null && inventory.slots[selectedIndexNum].item.IsEquip == true)
            {
                UnEquipItemForChangeStats(inventory.slots[selectedIndexNum].item);
            }
        }

        for (int i = 1; i <= 8; i++)
        {
            KeyCode key = KeyCode.Alpha0 + i;            
            if (Input.GetKeyDown(key))
            {
                if (inventory.slots[i - 1].isChoose == false) // isChoose로 두번 눌러도 안에 있는 메서드는 실행안댐.
                {
                    EquipItem(i - 1); // 아이템 들기
                    if (inventory.slots[i - 1].item != null)
                    {
                        EquipItemForChangeStats(inventory.slots[i - 1].item);
                        selectedIndexNum = i - 1;
                    }                    
                    break;
                }
                // 여기 else를 써주면 같은 키를 두번 눌렀을 때 실행됌.
            }
            else
            {
               
            }
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
    
    public void EquipItemForChangeStats(Item item)
    {
        item.IsEquip = true;
        if(item.IsEquip == true)
        {
            if(int.Parse(item.ItemType) == 10 || int.Parse(item.ItemType) == 11)
            {
                statHandler.CurrentStats.attackDamage += float.Parse(item.AttackDamage);
            }
            else if(int.Parse(item.ItemType) == 12)
            {
                statHandler.CurrentStats.miningAttack += float.Parse(item.AttackDamage);
            }
            else
            {
                return;
            }
        }        
    }

    public void UnEquipItemForChangeStats(Item item)
    {
        item.IsEquip = false;
        if (int.Parse(item.ItemType) == 10 || int.Parse(item.ItemType) == 11)
        {
            statHandler.CurrentStats.attackDamage -= float.Parse(item.AttackDamage);
        }
        else if (int.Parse(item.ItemType) == 12)
        {
            statHandler.CurrentStats.miningAttack -= float.Parse(item.AttackDamage);
        }
        else
        {
            return;
        }
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