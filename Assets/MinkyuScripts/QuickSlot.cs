using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlot : MonoBehaviour
{
    private Inventory inventory;
    private void Awake()
    {
        inventory = GetComponent<Inventory>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            inventory.invenSlot[0].QuickSlotItemChoose(1f);
        }
    }

    public void SwitchChoose()
    {

    }
}
