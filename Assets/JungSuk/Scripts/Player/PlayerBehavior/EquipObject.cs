using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipObject : MonoBehaviour
{
    private CharacterController controller;
    public Image[] quitSlots; // πŒ±‘¥‘¿Ã ∏∏µÈæÓ ¡÷Ω« ≈£ΩΩ∑‘¿∏∑Œ ∫Ø∞Ê
    public SpriteRenderer heldItem;
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

    private void EquipItemInQuitSlot()
    {
        for (int i = 1; i <= 8; i++)
        {
            KeyCode key = KeyCode.Alpha0 + i;

            if (Input.GetKeyDown(key))
            {
                equippedSlotIndex = i - 1;
                Debug.Log(i);
                if (!IsEquipedItem)
                {
                    EquipItem(i - 1);
                }
                else
                {
                    UnequipItem();
                    EquipItem(i - 1);
                }
            }
        }
    }

    private void EquipItem(int slotIndex)
    {
        heldItem.sprite = quitSlots[slotIndex].sprite;
        Debug.Log(slotIndex);
        IsEquipedItem = true;
       //ChangeStatByItem(slotIndex);
        
    }

    private void UnequipItem()
    {
        heldItem.sprite = null;
        IsEquipedItem = false;
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