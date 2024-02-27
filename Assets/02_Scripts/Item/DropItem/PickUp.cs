using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    public int itemCode;
    public Item item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        item = ItemManager.instance.SetItemData(itemCode); // itemIndex로 아이템이 무엇인지 정해준다.
        if (collision.CompareTag("Player"))
        {
            Inventory inven = collision.GetComponent<Inventory>();
            for (int i = 0; i < inven.invenSlot.Length - 3; i++)
            {
                // 인벤토리에 같은 아이템 코드의 아이템이 있다면, stack을 올려주고 꺼짐.
                if (!inven.slots[i].isEmpty && inven.slots[i].item.ItemCode == itemCode)
                {
                    inven.slots[i].stack++;
                    inven.invenSlot[i].ItemStackUIRefresh(inven.slots[i].stack);
                    inven.StackUpdate(i);
                    gameObject.SetActive(false);
                    return;
                }
            }
            for (int i = 0; i < inven.invenSlot.Length - 3; i++)
            {
                if (inven.slots[i].isEmpty)
                {
                    inven.invenSlot[i].ChangeInventoryImage(itemCode);
                    inven.invenSlot[i].OnOffImage(1f);
                    inven.slots[i].isEmpty = false;
                    inven.slots[i].item = item; // 정해준 아이템의 데이터를 넣어준다.
                    inven.slots[i].stack = 1;
                    inven.invenSlot[i].ItemStackUIRefresh(inven.slots[i].stack);
                    inven.StackUpdate(i);
                    if (inven.slots[i].isChoose)
                    {
                        // 빈 곳으로 아이템이 들어가면 손에 이미지 나타나게 해줌.
                        collision.GetComponent<EquipObject>().heldItem.sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
                    }
                    gameObject.SetActive(false);
                    return;
                }
            }
        }
    }
}
