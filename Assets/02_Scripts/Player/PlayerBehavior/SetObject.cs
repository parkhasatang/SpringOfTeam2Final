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
            // 퀵슬롯이 선택되었으면 안으로 진입, 우클릭을 사용할 수 있는 아이템인가
            if ((inventorySlot[i].isChoose == true) && (inventorySlot[i].item != null) && (inventorySlot[i].item.RightClick == true))
            {
                // 갯수가 0이상이면.
                if (inventorySlot[i].stack > 0)
                {
                    // 아이템 타입이 벽이라면
                    if (inventorySlot[i].item.ItemType == 1)
                    {
                        // TODO 나중에 벽마다 들어가는 타입을 다르게 설정.
                        SetWall(i);
                    }
                    // 먹을 수 있는 것이라면
                    else if (inventorySlot[i].item.ItemType == 8)
                    {
                        UsePotionForChangeStats(i);
                        AudioManager.instance.PlaySffx(AudioManager.Sfx.PlayerEat);
                    }
                    // 괭이라면
                    else if (inventorySlot[i].item.ItemType == 13)
                    {
                        // 밭으 소환하는 로직 필요
                        Debug.Log("밭소환");
                        SetField();
                        AudioManager.instance.PlaySffx(AudioManager.Sfx.PlayerHarvest);
                    }
                    // 물뿌리개
                    else if (inventorySlot[i].item.ItemType == 14)
                    {
                        SetWater();
                        AudioManager.instance.PlaySffx(AudioManager.Sfx.PlayerPulling);
                    }
                    // 씨앗이라면
                    else if (inventorySlot[i].item.ItemType == 15)
                    {
                        SetSeed(i);
                    }
                }
                break;
            }
            else
            {
                continue;
            }
        }

    }

    private void SetWall(int inventoryIndex)
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
                inventorySlot[inventoryIndex].stack--;
                UIManager.Instance.StackUpdate(inventoryIndex);
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
            RaycastHit2D hit = Physics2D.Raycast(mousPosition, Vector2.zero, 0.1f, 1 << LayerMask.NameToLayer("Field") | 1 << LayerMask.NameToLayer("Ground"));
            int layer = hit.collider.gameObject.layer;
            if (layer == LayerMask.NameToLayer("Field"))
            {
                Field field = hit.collider.gameObject.GetComponent<Field>();
                // 나중에 작물이 자라고 있지 않다면. = 자식오브젝트가 꺼져있다면으로 if문 조건 걸어주기.
                // 자라고 있다면
                if (field.isGrowing == true)
                {
                    // 다 자랐다면
                    if (field.isGrowFinish)
                    {
                        ItemManager.instance.itemPool.ItemSpawn(field.seedData.ItemCode - 40 , hit.point);
                        field.ReadyHarvest();
                    }
                }
                // 안자라고 있다면.
                else
                {
                    //씨앗이 있다면
                    if (field.isSeed)
                    {
                        ItemManager.instance.itemPool.ItemSpawn(field.seedData.ItemCode, hit.point);
                    }
                    // 밭 오브젝트 꺼주기.
                    hit.collider.gameObject.SetActive(false);
                }
            }
            else if (layer == LayerMask.NameToLayer("Ground"))
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
                    field.CheckIsSeed();
                }
            }
        }
    }

    private void SetSeed(int inventoryIndex)
    {
        Vector2 mousPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        float distance = Vector2.Distance(mousPosition, transform.position);

        if (distance < 2)
        {
            RaycastHit2D hit = Physics2D.Raycast(mousPosition, Vector2.zero, 0.1f, 1 << LayerMask.NameToLayer("Field"));
            if (hit)
            {
                Field field = hit.collider.gameObject.GetComponent<Field>();
                if (field.isSeed == false)
                {
                    field.isSeed = true;
                    field.seedData = inventorySlot[inventoryIndex].item;
                    inventorySlot[inventoryIndex].stack--;
                    UIManager.Instance.StackUpdate(inventoryIndex);
                    field.CheckIsSeed();
                }
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
