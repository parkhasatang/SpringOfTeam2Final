using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SetObject : MonoBehaviour, IUsePotion
{
    private CharacterController controller;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private TileMapControl tileMapControl;
    private EquipObject equipObject;
    private List<SlotData> inventorySlot;
    private HealthSystem healthSystem;

    float PotionCoolTime = 2f;
    float MaxCoolTime = 10f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        equipObject = GetComponent<EquipObject>();
        inventorySlot = UIManager.Instance.playerInventoryData.slots;
        healthSystem = GetComponent<HealthSystem>();
    }
    // Start is called before the first frame update
    private void Start()
    {
        controller.OnSetEvent += BuildObject;        
    }

    private void Update()
    {
        MaxCoolTime += Time.deltaTime;
    }

    private void BuildObject()
    {
        for (int i = 0; i < 8; i++)
        {
            // 퀵슬롯이 선택되었으면 안으로 진입.
            if ((inventorySlot[i].isChoose == true) && (inventorySlot[i].item != null) && (inventorySlot[i].item.ItemType != null))
            {
                
                // charactercontroller에서 설치가능한 아이템인지 판단하는게 있는가? 있으면 위에 ItemType으로 구별해주었던 것을 지우고 아래에 판별가능한 bool값을 설정
                if (inventorySlot[i].item.ItemType == 1) // 우클릭을 사용할 수 있는 아이템인가
                {
                    // 갯수가 0이상이면.
                    if (inventorySlot[i].stack > 0)
                    {
                        // 벽이라면
                        if (inventorySlot[i].item.ItemCode == 2101)
                        {
                            SetWall(i);
                        }
                        // 물약이라면
                        else if (inventorySlot[i].item.ItemType == 8)
                        {
                            // 플레이어 체력 회복
                            /*StackUpdate(i);*///쓸때 조심.                           

                        }
                    }
                }
                else if(inventorySlot[i].item.ItemType == 8)
                {
                    UsePotionForChangeStats(i);                 
                }
            }
            else
            {
                break;
            }
        }
        
    }

    private void SetWall(int i)
    {
        Vector2 mousPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        float distance = Vector2.Distance(mousPosition, transform.position);
        if (distance < 2)
        {
            if (TilemapManager.instance.wallDictionary.ContainsKey(new Vector3Int(Mathf.FloorToInt(mousPosition.x), Mathf.FloorToInt(mousPosition.y), 0)))
            {
                return;
            }
            else
            {
                tileMapControl.CreateTile(Mathf.FloorToInt(mousPosition.x), Mathf.FloorToInt(mousPosition.y));
                // 새롭게 생성해준 Tile을 Dictionary에 추가.
                TilemapManager.instance.wallDictionary[new Vector3Int(Mathf.FloorToInt(mousPosition.x), Mathf.FloorToInt(mousPosition.y), 0)] = new TileInfo
                {
                    tile = tileMapControl.wallTile,
                    HP = 100f
                };
                inventorySlot[i].stack--;
                UIManager.Instance.StackUpdate(i);
                // 들고있는 아이템 null로 만들기.
                equipObject.heldItem.sprite = null;
                // 벽의 타입이 바꿔지는 것에 따라 TileInfo를 바꿔주자.
                Debug.Log("딕셔너리에 추가");
            }
        }
    }

    public void UsePotionForChangeStats(int i)
    {
        if(PotionCoolTime < MaxCoolTime)
        {
            MaxCoolTime = 0;
            healthSystem.ChangeHealth(inventorySlot[i].item.HP);
            healthSystem.ChangeHunger(inventorySlot[i].item.Hunger);
            UIManager.Instance.playerInventoryData.slots[i].stack--;
            UIManager.Instance.StackUpdate(i);
        }             
    }
}
