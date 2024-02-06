using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FoodResult : MonoBehaviour
{
    private CanvasGroup itemImg;

    private void Awake()
    {
        itemImg = GetComponent<CanvasGroup>();
    }

    public void ClickButtonOnFood()
    {
        // 아이템 제작
        MakeFood();

        // 인벤토리로 보내기 시작. 데이터 임시보관소에 보내주기.
        UIManager.Instance.giveTemporaryItemData = UIManager.Instance.playerInventoryData.slots[28].item;
        UIManager.Instance.giveTemporaryItemStack = 1;

        for (int i = 0; i < UIManager.Instance.playerInventoryData.invenSlot.Length - 3; i++)
        {
            // 비어있거나 아이템 코드가 같다면
            if (UIManager.Instance.playerInventoryData.slots[i].isEmpty || UIManager.Instance.playerInventoryData.slots[i].item.ItemCode == UIManager.Instance.playerInventoryData.slots[28].item.ItemCode)
            {
                UIManager.Instance.playerInventoryData.slots[i].item = UIManager.Instance.giveTemporaryItemData;
                UIManager.Instance.playerInventoryData.slots[i].stack += UIManager.Instance.giveTemporaryItemStack;
                UIManager.Instance.StackUpdate(i);


                UIManager.Instance.giveTemporaryItemData = null;
                UIManager.Instance.giveTemporaryItemStack = 0;
                break;
                // 메서드가 종료되고싶은게 아니니깐 break;
            }
        }

        if ((UIManager.Instance.playerInventoryData.slots[26].stack == 0) || (UIManager.Instance.playerInventoryData.slots[27].stack == 0))
        {
            itemImg.blocksRaycasts = false;
            UIManager.Instance.playerInventoryData.slots[28].stack = 0;
            UIManager.Instance.StackUpdate(28);
        }
        // 아직 재료들이 남았다면
        else
        {
            itemImg.blocksRaycasts = true;
        }
    }


    /*public void OnBeginDrag(PointerEventData eventData)
    {


        // 안에 데이터들이 들어있다면
        if ((firstItem == 8) && (secondItem == 8))
        {
            // 해당 데이터의 Stack이 0보다 높으면
            if ((UIManager.Instance.playerInventoryData.slots[26].stack > 0) && (UIManager.Instance.playerInventoryData.slots[27].stack > 0))
            {
                Debug.Log("조합완료");
                FoodResultStack(true);
                // 임시저장소로 Item데이터 복사
                UIManager.Instance.giveTemporaryItemData = UIManager.Instance.playerInventoryData.slots[inventoryIndex].item;
                // 임시저장소 스택 +1
                UIManager.Instance.giveTemporaryItemStack++;
                // 음식재료 스택이 0이면
                if ((UIManager.Instance.playerInventoryData.slots[26].stack == 0) || (UIManager.Instance.playerInventoryData.slots[27].stack == 0))
                {
                    UIManager.Instance.playerInventoryData.slots[inventoryIndex].item = null;
                    UIManager.Instance.playerInventoryData.slots[inventoryIndex].stack = 0;
                    UIManager.Instance.StackUpdate(inventoryIndex);
                }
                // 아직 재료들이 남았다면
                else
                {
                    UIManager.Instance.playerInventoryData.slots[inventoryIndex].stack--;
                    UIManager.Instance.StackUpdate(inventoryIndex);
                }
            }
        }
    }*/

    private void MakeFood()
    {
        UIManager.Instance.playerInventoryData.slots[26].stack--;
        UIManager.Instance.playerInventoryData.slots[27].stack--;

        UIManager.Instance.StackUpdate(26);
        UIManager.Instance.StackUpdate(27);
    }
}
