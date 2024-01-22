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

    public void QuickSlotItemChoose(float value)
    {
        Color imageComponent = gameObject.GetComponent<Image>().color;
        imageComponent.a = value;
    }
}
