using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftItemDrag : MonoBehaviour
{
    // 인스펙터 창에서 아이템 코드 적어줘야댐.
    [SerializeField] internal int itemCode;
    // 드래그 했을 때 얘가 무슨 아이템인지 알고 데이터를 전달해줘야해서, Item클래스를 받아옴.
    private Item storeItemData;
    private Inventory inventory;

    private CanvasGroup itemImg;

    private CraftItemUI craftItemUI;

    private void Awake()
    {
        inventory = UIManager.Instance.playerInventoryData;
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

        UIManager.Instance.playerInventoryData.GiveItemToEmptyInv(storeItemData, 1);

        itemImg.blocksRaycasts = false;

        // 다시 스캔하기.
        craftItemUI.ReFreshCraftingUI();
    }

    


    

    public void CreateFromStore()
    {
        // 여기에 조합식 다 적어주면됌.
        switch (itemCode)
        {
            case 1001:
                inventory.RemoveItemFromInventory(3011, 1);
                inventory.RemoveItemFromInventory(3101, 1);
                break;
            case 1301:
                inventory.RemoveItemFromInventory(3011, 1);
                inventory.RemoveItemFromInventory(3101, 1);
                inventory.RemoveItemFromInventory(3001, 1);
                break;
        }
    }
}
