using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // 자기가 몇번 째 인벤토리인지 알고싶음.
    // 나중에 Awake로 번호 찾아오기 구현.
    [SerializeField] internal int inventoryIndex;

    private Transform canvas;
    private Transform previousParent;
    private RectTransform uiItemTransform;
    private CanvasGroup itemImg;

    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>().transform;
        uiItemTransform = GetComponent<RectTransform>();
        itemImg = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        previousParent = transform.parent;
        transform.SetParent(canvas);
        transform.SetAsLastSibling();

        // 데이터 임시로 맡겨두기.
        UIManager.Instance.giveTemporaryItemData = UIManager.Instance.playerInventoryData.slots[inventoryIndex].item;
        UIManager.Instance.giveTemporaryItemStack = UIManager.Instance.playerInventoryData.slots[inventoryIndex].stack;
        UIManager.Instance.playerInventoryData.slots[inventoryIndex].item = null;
        UIManager.Instance.playerInventoryData.slots[inventoryIndex].stack = 0;

        itemImg.alpha = 0.6f;
        itemImg.blocksRaycasts = false;
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
}
