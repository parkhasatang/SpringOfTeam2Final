using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    public int ItemIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            Inventory inven = collision.GetComponent<Inventory>();
            for (int i = 0; i < inven.invenSlot.Length; i++)
            {
                if (inven.slots[i].isEmpty)
                {
                    inven.invenSlot[i].ChangeInventoryImage(gameObject.GetComponent<SpriteRenderer>());
                    inven.invenSlot[i].OnOffImage(true);
                    SetItemInfo(ItemIndex);
                    gameObject.SetActive(false);
                    
                    break;
                }
            }
        }
    }

    private void SetItemInfo(int Index)
    {
       string name = UIManager.Instance.AllItemList[Index].Name;
       string description = UIManager.Instance.AllItemList[Index].Description;
        Debug.Log(name);
        Debug.Log(description);
    }

}
