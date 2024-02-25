using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<SlotData> slots = new();// SlotData를 리스트로 만들어주자.
    public SlotNum[] invenSlot;

    public void Awake()
    {
        for(int i = 0; i < invenSlot.Length; i++)
        {
            SlotData slot = new()// 객체 초기화 단순화
            {
                isEmpty = true,
                isChoose = false,
                item = null,
                stack = 0
            };
            slots.Add(slot);
        }
    }

    // 인벤토리 아이템의 스택검사.
    public bool CheckStackAmount(int itemCode, int requiredStack)
    {
        // 인벤토리 스캔
        for (int i = 0; i < invenSlot.Length - 3; i++)
        {
            if (!slots[i].isEmpty && slots[i].item.ItemCode == itemCode)
            {
                if (slots[i].stack >= requiredStack)
                {
                    return true;
                }
                else return false;
            }
        }
        return false;
    }

    // 인벤토리 스택 검사.
    public void StackUpdate(int indexOfInventory)
    {
        if (slots[indexOfInventory].stack == 0)
        {
            slots[indexOfInventory].item = null;
            slots[indexOfInventory].isEmpty = true;
            invenSlot[indexOfInventory].ChangeInventoryImage(0);
            invenSlot[indexOfInventory].OnOffImage(0);
        }
        else if (slots[indexOfInventory].stack > 0)
        {
            slots[indexOfInventory].isEmpty = false;
            invenSlot[indexOfInventory].ChangeInventoryImage(slots[indexOfInventory].item.ItemCode);
            invenSlot[indexOfInventory].OnOffImage(1f);
        }
        invenSlot[indexOfInventory].ItemStackUIRefresh(slots[indexOfInventory].stack);
    }

    // 인벤토리에서 아이템 제거해주기.
    public void RemoveItemFromInventory(int itemCode, int stackNeed)
    {
        // 인벤토리 스캔
        for (int i = 0; i < invenSlot.Length - 3; i++)
        {
            if (slots[i].item != null && slots[i].item.ItemCode == itemCode)
            {
                // 어차피  재료의 Stack이 만드는 것보다 많을 때 실행이될거라 음수로 갈 걱정은 없어서 if문으로 안넣어줘도됌.
                slots[i].stack -= stackNeed;
                StackUpdate(i);
                break;
            }
        }
    }

    public void GiveItemToEmptyInv(Item itemData, int stack)
    {
        // 데이터 임시보관소에 보내주기.
        UIManager.Instance.giveTemporaryItemData = itemData;
        UIManager.Instance.giveTemporaryItemStack = stack;

        for (int i = 0; i < invenSlot.Length - 3; i++)
        {
            if (slots[i].isEmpty)
            {
                slots[i].item = UIManager.Instance.giveTemporaryItemData;
                slots[i].stack = UIManager.Instance.giveTemporaryItemStack;
                StackUpdate(i);


                UIManager.Instance.giveTemporaryItemData = null;
                UIManager.Instance.giveTemporaryItemStack = 0;
                break;
            }
            else
            {
                if (slots[i].item.ItemCode == itemData.ItemCode)
                {
                    slots[i].stack += stack;
                    StackUpdate(i);
                    break;
                }
                continue;
            }
        }
    }
}
