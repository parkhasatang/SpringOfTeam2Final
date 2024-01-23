using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    public int itemIndex;
    public Item item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SetItemInfo(itemIndex); // itemIndex로 아이템이 무엇인지 정해준다.
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
                    inven.slots[i].item = item; // 정해준 아이템의 데이터를 넣어준다.
                    gameObject.SetActive(false);
                   
                    break;
                }
            }
        }
    }

    private void SetItemInfo(int Index)
    {
        item = ItemManager.instacne.items[Index];
    }
}
