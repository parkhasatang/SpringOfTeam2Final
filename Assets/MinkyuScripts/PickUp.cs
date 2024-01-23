using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    public int itemIndex;
    public ItemManager itemManager;

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
                    inven.slots[i].item = itemManager.items[itemIndex];
                    //inven.slots[i].item = UIManager.Instance.items[1001];                  
                    /*inven.slots[i].item = new Item(SetItemInfo(itemIndex));*/
                    gameObject.SetActive(false);
                    break;                   
                }
            }
        }
    }
}
