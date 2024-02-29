using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EquipObject : MonoBehaviour, IEquipedItem
{
    private CharacterController controller;
    public Image[] quitSlots;
    public SpriteRenderer heldItem;
    public Transform SpawnTrans;
    private CharacterStatHandler statHandler;

    private Item previousEquipItemData;
    public Item usedPreviousEquipItemData { get { return previousEquipItemData; } private set { previousEquipItemData = value; } }
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
            // UI꺼주기
            inventory.invenSlot[i].QuickSlotItemChoose(false);
            inventory.slots[i].isChoose = false;
        }
        if (previousEquipItemData != null)
        {
            UnEquipItemForChangeStats(previousEquipItemData.ItemCode);
        }

        for (int i = 1; i <= 8; i++)
        {
            KeyCode key = KeyCode.Alpha0 + i;            
            if (Input.GetKeyDown(key))
            {
                inventory.slots[i - 1].isChoose = true;

                if (inventory.slots[i - 1].isChoose) 
                {
                    // 플레이어가 아무것도 선택하지않은 상태에서 아이템이 들어온걸 처음 선택했을 때
                    if (previousEquipItemData == null)
                    {
                        previousEquipItemData = inventory.slots[i - 1].item;
                    }
                    // 퀵슬롯에서 선택한 아이템의 옮겨진 자리를 다시 선택할 떄.
                    if (inventory.slots[i - 1].item == null)
                    {
                        previousEquipItemData = null;
                    }
                    // 기존에 들었던 아이템이랑 같다면
                    if (previousEquipItemData == inventory.slots[i - 1].item)
                    {
                        EquipItem(i - 1);
                        inventory.invenSlot[i - 1].QuickSlotItemChoose(true);
                        inventory.slots[i - 1].isChoose = true;
                        EquipItemForChangeStats(i - 1);
                        break;
                    }
                    // 다르다면
                    else
                    {
                        EquipItem(i - 1); // 아이템 들기

                        if (inventory.slots[i-1].item != null)
                        {
                            // 장착 가능한 아이템이면은 스탯을 올려주면 안됀다.
                            if (!inventory.slots[i - 1].item.IsEquip)
                            {
                                EquipItemForChangeStats(i - 1);
                            }
                            previousEquipItemData = inventory.slots[i - 1].item;
                        }
                        break;

                    }
                }
            }
        }
    }

    private void EquipItem(int slotIndex)
    {
        heldItem.sprite = quitSlots[slotIndex].sprite;
        inventory.invenSlot[slotIndex].QuickSlotItemChoose(true);
    }

    public void EquipItemForChangeStats(int itemIndex)
    {
        if (inventory.slots[itemIndex].item == null)
        {
            return;            
        }
        else
        {            
             if (inventory.slots[itemIndex].item.ItemType == 10 || inventory.slots[itemIndex].item.ItemType == 11)
             {
                statHandler.CurrentStats.attackDamage += inventory.slots[itemIndex].item.AttackDamage;
             }

             else if (inventory.slots[itemIndex].item.ItemType == 12)
             {
                statHandler.CurrentStats.miningAttack += inventory.slots[itemIndex].item.AttackDamage;
             }
             else
             {
                return;
             }
            UIManager.Instance.UpdatePlayerStatTxt();
        }
    }

    public void UnEquipItemForChangeStats(int itemCode)
    {
        if (previousEquipItemData.ItemType == 10)
        {
            statHandler.CurrentStats.attackDamage -= previousEquipItemData.AttackDamage;
        }
        else if (previousEquipItemData.ItemType == 12)
        {
            statHandler.CurrentStats.miningAttack -= previousEquipItemData.AttackDamage;
        }
        else
        {
            return;
        }                  
        UIManager.Instance.UpdatePlayerStatTxt();
    }

    /*private void UnEquipItem(int slotIndex)// todo 기존에 있던 아이템 정보를 빼고넣기.
    {*//*
        heldItem.sprite = null;
        inventory.invenSlot[slotIndex].QuickSlotItemChoose(false);*//*
    }*/


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