using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinUIManager : MonoBehaviour
{
    public static MinUIManager instance;

    public Inventory playerInventoryData;
    public Item giveTemporaryItemData;
    public Item takeTemporaryItemData;
    public Sprite temporaryItemImg;

    private void Awake()
    {
        instance = this;
    }
}
