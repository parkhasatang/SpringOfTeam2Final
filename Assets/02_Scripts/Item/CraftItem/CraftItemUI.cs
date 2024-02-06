using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftItemUI : MonoBehaviour
{
    // CanvasGroup의 BlockRaycats를 활용하여 Drag & Drop으로 가져갈려고 BlockRaycats만 켜주면 가져갈 수 있게 해준다.
    [SerializeField] private CanvasGroup[] craftItem;

    internal List<int> stuffGather;
    internal int inventoryLength;

    private void Awake()
    {
        inventoryLength = UIManager.Instance.playerInventoryData.invenSlot.Length - 3;
    }

    private void OnEnable()
    {
        // 인벤토리 한번 스캔하기
        ReFreshCraftingUI();
    }

    public void ReFreshCraftingUI()
    {
        // 켜질 때 stuffGather 안에 비워주기
        stuffGather = new List<int>();

        for (int i = 0; i < inventoryLength; i++)
        {
            if (!UIManager.Instance.playerInventoryData.slots[i].isEmpty)
            {
                stuffGather.Add(UIManager.Instance.playerInventoryData.slots[i].item.ItemCode);
                Debug.Log("스캔완료");
            }
            else continue;
        }

        for (int j = 0; j < craftItem.Length; j++)
        {
            craftItem[j].alpha = 0.2f;
            craftItem[j].blocksRaycasts = false;
        }
        // 조건에 맞으면 제작대에 있는 아이템 켜주기.
        Debug.Log("초기화 완료");
        CraftingRecipe();
    }

    public void CraftingRecipe()
    {
        Debug.Log("레시피 검토");
        if (stuffGather.Contains(3011) && stuffGather.Contains(3101))
        {
            Debug.Log("레시피 있음.");
            // 스택 필요도 보다 많을 때, 아이템 활성화
            if (CheckStackAmount(3011, 1) && CheckStackAmount(3101, 1))
            {
                Debug.Log("검 활성화");
                craftItem[0].alpha = 1f;
                craftItem[0].blocksRaycasts = true;
            }
        }
        if (stuffGather.Contains(3011) && stuffGather.Contains(3101) && stuffGather.Contains(3001))
        {
            if (CheckStackAmount(3011, 1) && CheckStackAmount(3101, 1) && CheckStackAmount(3001, 1))
            {
                Debug.Log("곡괭이 활성화");
                craftItem[1].alpha = 1f;
                craftItem[1].blocksRaycasts = true;
            }
        }
    }

    // 스택 검사.
    public bool CheckStackAmount(int itemCode, int requiredStack)
    {
        // 인벤토리 스캔
        for (int i = 0; i < inventoryLength; i++)
        {
            if (!UIManager.Instance.playerInventoryData.slots[i].isEmpty && UIManager.Instance.playerInventoryData.slots[i].item.ItemCode == itemCode)
            {
                if (UIManager.Instance.playerInventoryData.slots[i].stack >= requiredStack)
                {
                    return true;
                }
                else return false;
            }
        }
        return false;
    }
}
