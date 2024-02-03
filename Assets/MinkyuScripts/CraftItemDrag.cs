using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftItemDrag : MonoBehaviour
{
    // 인스펙터 창에서 아이템 코드 적어줘야댐.
    [SerializeField] private int itemCode;
    // 드래그 했을 때 얘가 무슨 아이템인지 알고 데이터를 전달해줘야해서, Item클래스를 받아옴.
    private Item storeItemData;

    private CanvasGroup itemImg;

    private CraftItemUI craftItemUI;

    private void Awake()
    {
        craftItemUI = GetComponentInParent<CraftItemUI>();

        // 아이템 무엇인지 결정
        storeItemData = ItemManager.instance.SetItemData(itemCode);

        itemImg = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        gameObject.GetComponent<Image>().sprite = ItemManager.instance.GetSpriteByItemCode(itemCode);
    }

    public void ClickButtonOnStore()
    {
        // 아이템 제작
        CreateFromStore();

        // 데이터 임시보관소에 보내주기.
        UIManager.Instance.giveTemporaryItemData = storeItemData;
        UIManager.Instance.giveTemporaryItemStack = 1;

        for (int i = 0; i < UIManager.Instance.playerInventoryData.invenSlot.Length - 3; i++)
        {
            if (UIManager.Instance.playerInventoryData.slots[i].isEmpty)
            {
                UIManager.Instance.playerInventoryData.slots[i].item = UIManager.Instance.giveTemporaryItemData;
                UIManager.Instance.playerInventoryData.slots[i].stack = UIManager.Instance.giveTemporaryItemStack;
                UIManager.Instance.StackUpdate(i);


                UIManager.Instance.giveTemporaryItemData = null;
                UIManager.Instance.giveTemporaryItemStack = 0;
                break;
                // 메서드가 종료되고싶은게 아니니깐 break;
            }
        }

        itemImg.blocksRaycasts = false;

        // 다시 스캔하기.
        craftItemUI.ReFreshCraftingUI();
    }

    


    public void RemoveItemFromInventory(int itemCode, int stackNeed)
    {
        // 인벤토리 스캔
        for (int i = 0; i < craftItemUI.inventoryLength; i++)
        {
            if (UIManager.Instance.playerInventoryData.slots[i].item != null && UIManager.Instance.playerInventoryData.slots[i].item.ItemCode == itemCode)
            {
                // 어차피  재료의 Stack이 만드는 것보다 많을 때 실행이될거라 음수로 갈 걱정은 없어서 if문으로 안넣어줘도됌.
                UIManager.Instance.playerInventoryData.slots[i].stack -= stackNeed;
                if (UIManager.Instance.playerInventoryData.slots[i].stack <= 0)
                {
                    // 데이터 지워주고 이미지 지워주기.
                    UIManager.Instance.StackUpdate(i);
                    // stuffGather에 들어간 ItemCode지워주기
                    craftItemUI.stuffGather.Remove(itemCode);
                }
                break;
            }
        }
    }

    public void CreateFromStore()
    {
        // 여기에 조합식 다 적어주면됌.
        switch (itemCode)
        {
            case 1001:
                RemoveItemFromInventory(3011, 1);
                RemoveItemFromInventory(3101, 1);
                break;
            case 1301:
                RemoveItemFromInventory(3011, 1);
                RemoveItemFromInventory(3101, 1);
                RemoveItemFromInventory(3001, 1);
                break;
        }
    }
}
