using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FoodResult : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] internal int inventoryIndex;

    private Transform canvas;
    private Transform previousParent;
    private RectTransform uiItemTransform;
    private CanvasGroup itemImg;
    
    private int firstItem;
    private int secondItem;

    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>().transform;
        uiItemTransform = GetComponent<RectTransform>();
        itemImg = GetComponent<CanvasGroup>();

        firstItem = UIManager.Instance.playerInventoryData.slots[26].item.ItemType;
        secondItem = UIManager.Instance.playerInventoryData.slots[27].item.ItemType;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        // DraggableUI에 있는 스크립트 일부만 사용.
        previousParent = transform.parent;
        transform.SetParent(canvas);
        transform.SetAsLastSibling();

        itemImg.alpha = 0.6f;
        itemImg.blocksRaycasts = false;
        // 여기까지 DraggableUI에 있는 스크립트 일부


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
    }

    public void OnDrag(PointerEventData eventData)
    {
        uiItemTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(previousParent);
        uiItemTransform.position = previousParent.GetComponent<RectTransform>().position;

        itemImg.alpha = 1f;
        itemImg.blocksRaycasts = true;

        if (eventData.pointerDrag.GetComponent<Image>().sprite == null)
        {
            Color imageColor = eventData.pointerDrag.GetComponent<Image>().color;
            imageColor.a = 0f;
            eventData.pointerDrag.GetComponent<Image>().color = imageColor;

            // 데이터 비었는지 bool값 설정.
            UIManager.Instance.playerInventoryData.slots[inventoryIndex].isEmpty = true;
        }
        // 다른 곳에 놓았을 때, 그대로라면 다시 가져오기.
        else if (eventData.pointerDrag.GetComponent<Image>().sprite == eventData.pointerDrag.GetComponent<Image>().sprite)
        {
            UIManager.Instance.playerInventoryData.slots[inventoryIndex].item = UIManager.Instance.giveTemporaryItemData;
            UIManager.Instance.playerInventoryData.slots[inventoryIndex].stack = UIManager.Instance.giveTemporaryItemStack;
            UIManager.Instance.giveTemporaryItemData = null;
            UIManager.Instance.giveTemporaryItemStack = 0;
        }
        // 옮겨졌다면 바꾸었던 데이터 가져오기.
        else
        {
            UIManager.Instance.playerInventoryData.slots[inventoryIndex].item = UIManager.Instance.takeTemporaryItemData;
            UIManager.Instance.playerInventoryData.slots[inventoryIndex].stack = UIManager.Instance.takeTemporaryItemStack;
            UIManager.Instance.takeTemporaryItemData = null;
            UIManager.Instance.giveTemporaryItemStack = 0;
        }
        UIManager.Instance.StackUpdate(inventoryIndex);
    }

    private void FoodResultStack(bool succes)
    {
        if (succes)
        {
            UIManager.Instance.playerInventoryData.slots[26].stack--;
            UIManager.Instance.playerInventoryData.slots[27].stack--;

            UIManager.Instance.StackUpdate(26);
            UIManager.Instance.StackUpdate(27);
        }
        else
        {
            UIManager.Instance.playerInventoryData.slots[26].stack++;
            UIManager.Instance.playerInventoryData.slots[27].stack++;

            UIManager.Instance.StackUpdate(26);
            UIManager.Instance.StackUpdate(27);
        }
    }
}
