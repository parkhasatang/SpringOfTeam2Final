using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public Item(int itemCode, int itemType, string name, string description, float Hp, float hunger, float attackDamage, float attackDelay, float denfense,
        float attackRange, float speed, int stackNumber, bool isEquip)
    {
        ItemCode = itemCode; ItemType = itemType; Name = name; Description = description; HP = Hp; Hunger = hunger; AttackDamage = attackDamage; AttackRange = attackDelay;
        Defense = denfense; Speed = speed; StackNumber = stackNumber; IsEquip = isEquip;
    }
    public int ItemCode, ItemType, StackNumber;
    public float HP, Hunger, AttackDamage, AttackDelay, Defense, AttackRange, Speed;
    public string Name, Description;
    public bool IsEquip;
}

public class ItemManager : MonoBehaviour
{
    private static ItemManager _instance;

    public static ItemManager instacne
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ItemManager>();

                if (_instance == null)
                {
                    GameObject ItemManager = new GameObject("ItemManager");
                    _instance = ItemManager.AddComponent<ItemManager>();
                }
            }
            return _instance;
        }
    }

    public TextAsset ItemDatas;
    public Dictionary<int, Item> items = new Dictionary<int, Item>() { };
    
    public ItemPool itemPool;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        _instance = this;
    }

    void Start()
    {
        string[] line = ItemDatas.text.Substring(0, ItemDatas.text.Length - 1).Split('\n');
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');
            items.Add(int.Parse(row[0]), new Item(int.Parse(row[0]), int.Parse(row[1]), row[2], row[3], float.Parse(row[4]), float.Parse(row[5]), float.Parse(row[6]), 
                float.Parse(row[7]), float.Parse(row[8]), float.Parse(row[9]),float.Parse(row[10]), int.Parse(row[11]), row[12] == "TRUE"));
        }
    }
}
