using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotNum : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text itemStack;

    public void ChangeInventoryImage(Sprite _image)
    {
        itemImage.sprite = _image;
    }

    public void OnOffImage(bool isOn)
    {
        if (isOn)
        {
            Color imageColor = itemImage.gameObject.GetComponent<Image>().color;
            imageColor.a = 1f;
            itemImage.gameObject.GetComponent<Image>().color = imageColor;
        }
        else
        {
            Color imageColor = itemImage.gameObject.GetComponent<Image>().color;
            imageColor.a = 0f;
            itemImage.gameObject.GetComponent<Image>().color = imageColor;
        }
        
    }

    public void QuickSlotItemChoose(bool isOn)
    {
        if (isOn)
        {
            Color imageColor = gameObject.GetComponent<Image>().color;
            imageColor.a = 1f;
            GetComponent<Image>().color = imageColor;
        }
        else
        {
            Color imageColor = gameObject.GetComponent<Image>().color;
            imageColor.a = 0f;
            GetComponent<Image>().color = imageColor;
        }
    }

    public void ItemStackUIRefresh(int stack)
    {
        if (stack <= 0)
        {
            itemStack.gameObject.SetActive(false);
            stack = 0;
        }
        else if (stack > 0)
        {
            itemStack.gameObject.SetActive(true);
            itemStack.text = $"{stack}";
        }
        // stack이 음수가 될 수는 없음. 0이 되면 데이터를 없애줄 것이기 때문.
    }
}
