using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // 자기가 몇번 째 인벤토리인지 알고싶음.
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
        UIManager.instance.giveTemporaryItemData = UIManager.instance.playerInventoryData.slots[inventoryIndex].item;
        UIManager.instance.playerInventoryData.slots[inventoryIndex].item = null;

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
            UIManager.instance.playerInventoryData.slots[inventoryIndex].isEmpty = true;
        }
        else if (eventData.pointerDrag.GetComponent<Image>().sprite == eventData.pointerDrag.GetComponent<Image>().sprite)
        {
            // 다른 곳에 놓았을 때, 그대로라면 다시 가져오기.
            UIManager.instance.playerInventoryData.slots[inventoryIndex].item = UIManager.instance.giveTemporaryItemData;
            UIManager.instance.giveTemporaryItemData = null;
        }
        else
        {
            UIManager.instance.playerInventoryData.slots[inventoryIndex].item = UIManager.instance.takeTemporaryItemData;
            UIManager.instance.takeTemporaryItemData = null;
        }
    }
}
