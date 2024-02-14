using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<SlotData> slots = new();// SlotData를 리스트로 만들어주자.
    public SlotNum[] invenSlot;

    public void Start()
    {
        for(int i = 0; i < invenSlot.Length; i++)
        {
            SlotData slot = new()// 객체 초기화 단순화
            {
                isEmpty = true,
                isChoose = false,
                item = null,
                stack = 0
            };
            slots.Add(slot);
        }
    }
}
