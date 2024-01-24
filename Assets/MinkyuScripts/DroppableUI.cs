using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DroppableUI : MonoBehaviour, IDropHandler
{
    private int inventoryIndex;
    Image inventoryImg;

    private void Awake()
    {
        inventoryIndex = GetComponent<DraggableUI>().inventoryIndex;
        inventoryImg = GetComponent<Image>();
    }

    // 기존에 있던 위치의 UI를 지금 있는 것과 바꿔주어야댐
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            // 이미지 임시 저장소로 보내기.
            MinUIManager.instance.temporaryItemImg = inventoryImg.sprite;

            // 이미지 드래그 했던 것으로 바꾸어 주기.
            inventoryImg.sprite = eventData.pointerDrag.GetComponent<Image>().sprite;

            // 드래그 했던 오브젝트의 이미지를 임시저장소에 있는 이미지로 바꾸어주기.
            eventData.pointerDrag.GetComponent<Image>().sprite = MinUIManager.instance.temporaryItemImg;
            MinUIManager.instance.temporaryItemImg = null;


            // 오브젝트의 이미지로 알파값 조절. 추가로 데이터도 옮김.
            if (inventoryImg.sprite != null)
            {
                Color imageColor = inventoryImg.color;
                imageColor.a = 1f;
                inventoryImg.color = imageColor;

                // 데이터 비었는지 bool값 설정.
                MinUIManager.instance.playerInventoryData.slots[inventoryIndex].isEmpty = false;

                // 데이터 임시저장소에 올리기.
                MinUIManager.instance.takeTemporaryItemData = MinUIManager.instance.playerInventoryData.slots[inventoryIndex].item;
                MinUIManager.instance.playerInventoryData.slots[inventoryIndex].item = null;

                // 데이터 받아오기.
                MinUIManager.instance.playerInventoryData.slots[inventoryIndex].item = MinUIManager.instance.giveTemporaryItemData;
                MinUIManager.instance.giveTemporaryItemData = null;
            }
        }
    }
}
