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
    private HealthSystem healthSystem;

    float PotionCoolTime = 2f;
    float MaxCoolTime = 10f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        equipObject = GetComponent<EquipObject>();
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
            if ((UIManager.Instance.playerInventoryData.slots[i].isChoose == true) && (UIManager.Instance.playerInventoryData.slots[i].item != null) && (UIManager.Instance.playerInventoryData.slots[i].item.RightClick == true))
            {
                // 갯수가 0이상이면.
                if (UIManager.Instance.playerInventoryData.slots[i].stack > 0)
                {
                    // 아이템 타입이 벽이라면
                    if (UIManager.Instance.playerInventoryData.slots[i].item.ItemType == 1)
                    {
                        // TODO 나중에 벽마다 들어가는 타입을 다르게 설정.
                        SetWall(i);
                    }
                    // 먹을 수 있는 것이라면
                    else if (UIManager.Instance.playerInventoryData.slots[i].item.ItemType == 8)
                    {
                        UsePotionForChangeStats(i);
                        AudioManager.instance.PlaySffx(AudioManager.Sfx.PlayerEat);
                    }
                    // 괭이라면
                    else if (UIManager.Instance.playerInventoryData.slots[i].item.ItemType == 13)
                    {
                        // 밭으 소환하는 로직 필요
                        Debug.Log("밭소환");
                        SetField();
                    }
                    // 물뿌리개
                    else if (UIManager.Instance.playerInventoryData.slots[i].item.ItemType == 14)
                    {
                        SetWater();
                    }
                    // 씨앗이라면
                    else if (UIManager.Instance.playerInventoryData.slots[i].item.ItemType == 15)
                    {
                        SetSeed(i);
                    }
                    UIManager.Instance.playerInventoryData.StackUpdate(i);
                    if (UIManager.Instance.playerInventoryData.slots[i].item == null)
                    {
                        equipObject.heldItem.sprite = null;
                    }
                    else equipObject.heldItem.sprite = ItemManager.instance.GetSpriteByItemCode(UIManager.Instance.playerInventoryData.slots[i].item.ItemCode);
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
            // Ground에 Collider가 없어서 Layer예외처리를 다 해줘야댐.
            RaycastHit2D hit = Physics2D.Raycast(mousPosition, Vector2.zero, 0.1f, 1 << LayerMask.NameToLayer("Wall") | 1 << LayerMask.NameToLayer("Monster") | 1 << LayerMask.NameToLayer("Field") | 1 << LayerMask.NameToLayer("Box") | 1 << LayerMask.NameToLayer("Item") | 1 << LayerMask.NameToLayer("Pyramid") | 1 << LayerMask.NameToLayer("Default"));

            // Ray가 맞았으면, 그곳에는 무언가 있다는 거니 설치 X
            if (hit)
            {
                Debug.Log(hit.transform.name);
                return;
            }
            // 아무것도 맞지 않았다면.
            else
            {
                Vector3Int cellPosition = new Vector3Int(Mathf.FloorToInt(mousPosition.x), Mathf.FloorToInt(mousPosition.y), 0);

                TilemapManager.instance.SetWallInfo(cellPosition, tileMapControl.wallTile);
                tileMapControl.CreateTile(cellPosition.x, cellPosition.y);

                UIManager.Instance.playerInventoryData.slots[inventoryIndex].stack--;
            }
        }
    }

    private void SetField()
    {
        Vector2 mousPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        float distance = Vector2.Distance(mousPosition, transform.position);

        if (distance < 2)
        {
            RaycastHit2D hit = Physics2D.Raycast(mousPosition, Vector2.zero, 0.1f, 1 << LayerMask.NameToLayer("Field") | 1 << LayerMask.NameToLayer("Wall") | 1 << LayerMask.NameToLayer("Pyramid") | 1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("Box"));
            
            if (!hit)
            {
                Debug.Log("생성완료");
                Vector3 spawnPosition = new Vector3(Mathf.FloorToInt(mousPosition.x) + 0.5f, Mathf.FloorToInt(mousPosition.y) + 0.5f);
                FarmManager.instance.FieldSpawn(spawnPosition);
                AudioManager.instance.PlaySffx(AudioManager.Sfx.PlayerHarvest);
            }
            else
            {
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
                            ItemManager.instance.itemPool.ItemSpawn(field.seedData.ItemCode - 40, hit.point);
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
                        AudioManager.instance.PlaySffx(AudioManager.Sfx.PlayerHarvest);
                        // 밭 오브젝트 꺼주기.
                        hit.collider.gameObject.SetActive(false);
                    }
                }
                
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
                    AudioManager.instance.PlaySffx(AudioManager.Sfx.PlayerPulling);
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
                    field.seedData = UIManager.Instance.playerInventoryData.slots[inventoryIndex].item;
                    UIManager.Instance.playerInventoryData.slots[inventoryIndex].stack--;
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
            healthSystem.ChangeHealth(UIManager.Instance.playerInventoryData.slots[i].item.HP);
            healthSystem.ChangeHunger(UIManager.Instance.playerInventoryData.slots[i].item.Hunger);            
            UIManager.Instance.playerInventoryData.slots[i].stack--;
        }             
    }
}
