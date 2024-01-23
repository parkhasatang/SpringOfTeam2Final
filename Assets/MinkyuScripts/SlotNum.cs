using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotNum : MonoBehaviour
{
    [SerializeField] private Image itemImage;

    public void ChangeInventoryImage(SpriteRenderer _image)
    {
        itemImage.sprite = _image.sprite;
    }

    public void OnOffImage(bool isOn)
    {
        itemImage.gameObject.SetActive(isOn);
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
}
