using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    public int itemIndex;

    public string 
        itemType, 
        itemName, 
        itemDescription, 
        itemHP, 
        itemHunger, 
        itemAttackDamage, 
        itemAttackDelay, 
        itemDefense, 
        itemAttackRange, 
        itemSpeed, 
        itemStackNumber;

    public bool isEquip;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*SetItemInfo(itemIndex);*/
        if (collision.CompareTag("Player"))
        {
            Inventory inven = collision.GetComponent<Inventory>();
            for (int i = 0; i < inven.invenSlot.Length; i++)
            {
                if (inven.slots[i].isEmpty)
                {
                    inven.invenSlot[i].ChangeInventoryImage(gameObject.GetComponent<SpriteRenderer>());
                    inven.invenSlot[i].OnOffImage(true);
                    inven.slots[i].isEmpty = false;
                    /*inven.slots[i].item = new Item(SetItemInfo(itemIndex));*/
                    gameObject.SetActive(false);
                    break;
                }
            }
        }
    }

    private void SetItemInfo(int Index)
    {
        itemType = UIManager.Instance.AllItemList[Index].ItemType;
        itemName = UIManager.Instance.AllItemList[Index].Name;
        itemDescription = UIManager.Instance.AllItemList[Index].Description;
        itemHP = UIManager.Instance.AllItemList[Index].HP;
        itemHunger = UIManager.Instance.AllItemList[Index].Hunger;
        itemAttackDamage = UIManager.Instance.AllItemList[Index].AttackDamage;
        itemAttackDelay = UIManager.Instance.AllItemList[Index].AttackDelay;
        itemDefense = UIManager.Instance.AllItemList[Index].Defense;
        itemAttackRange = UIManager.Instance.AllItemList[Index].AttackRange;
        itemSpeed = UIManager.Instance.AllItemList[Index].Speed;
        itemStackNumber = UIManager.Instance.AllItemList[Index].StackNumber;
    }

}
