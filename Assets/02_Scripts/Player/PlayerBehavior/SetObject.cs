using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SetObject : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private TileMapControl tileMapControl;
    private EquipObject equipObject;
    private List<SlotData> inventorySlot;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        equipObject = GetComponent<EquipObject>();
        inventorySlot = UIManager.Instance.playerInventoryData.slots;
    }
    // Start is called before the first frame update
    private void Start()
    {
        controller.OnSetEvent += BuildObject;
    }

    private void BuildObject()
    {
        for (int i = 0; i < 8; i++)
        {
            // 퀵슬롯이 선택되었으면 안으로 진입.
            if ((inventorySlot[i].isChoose == true) && (inventorySlot[i].item != null) && (inventorySlot[i].item.RightClick == true))
            {
                
                // charactercontroller에서 설치가능한 아이템인지 판단하는게 있는가? 있으면 위에 ItemType으로 구별해주었던 것을 지우고 아래에 판별가능한 bool값을 설정
                if (inventorySlot[i].stack > 0) // 우클릭을 사용할 수 있는 아이템인가
                {
                    // 갯수가 0이상이면.
                    if (inventorySlot[i].item.ItemType == 1)
                    {
                        // 벽이라면
                        if (inventorySlot[i].item.ItemCode == 2101)
                        {
                            SetWall(i);
                        }
                        else if (inventorySlot[i].item.ItemCode == 2101)
                        {

                        }
                    }
                    // 마실 수 있는 것이라면
                    else if (inventorySlot[i].item.ItemType == 8)
                    {
                        if (inventorySlot[i].item.ItemCode == 1701)
                        {
                            // 플레이어 체력 회복
                            /*StackUpdate(i);*///쓸때 조심.
                        }
                    }
                    // 괭이라면
                    else if (inventorySlot[i].item.ItemType == 13)
                    {
                        // 밭으 소환하는 로직 필요
                        Debug.Log("밭소환");
                        SetField();
                    }
                    // 물뿌리개
                    else if(inventorySlot[i].item.ItemType == 14)
                    {
                        SetWater();
                    }
                }
            }
            else
            {
                // 브레이크하면 for문이 중간에 끝나는지?
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

    private void SetField()
    {
        Vector2 mousPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        float distance = Vector2.Distance(mousPosition, transform.position);

        if (distance < 2)
        {
            RaycastHit2D hit = Physics2D.Raycast(mousPosition, Vector2.zero, 0.1f, 1 << LayerMask.NameToLayer("Field") | 1 << LayerMask.NameToLayer("Wall"));
            int layer = hit.collider.gameObject.layer;
            if (layer == LayerMask.NameToLayer("Field"))
            {
                return;
                // 나중에 작물이 자라고 있지 않다면. = 자식오브젝트가 꺼져있다면으로 if문 조건 걸어주기.
            }
            else if (layer == LayerMask.NameToLayer("Wall"))
            {
                Debug.Log("생성완료");
                Vector3 spawnPosition = new Vector3(Mathf.FloorToInt(mousPosition.x) + 0.5f, Mathf.FloorToInt(mousPosition.y) + 0.5f);
                FarmManager.instance.FieldSpawn(spawnPosition);
            }
        }
    }

    private void SetWater()
    {
        Vector2 mousPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        float distance = Vector2.Distance(mousPosition, transform.position);

        if (distance < 2)
        {
            RaycastHit2D hit = Physics2D.Raycast(mousPosition, Vector2.zero, 0.1f, 1 << LayerMask.NameToLayer("Field"));
            if (hit)
            {
                Field field = hit.collider.gameObject.GetComponent<Field>();
                if (field.isWatering == false)
                {
                    field.isWatering = true;
                }
            }
        }
    }
}
