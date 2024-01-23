using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item
{
    public Item(string itemCode, string itemType, string name, string description, string Hp, string hunger, string attackDamage, string attackDelay, string denfense,
        string attackRange, string speed, string stackNumber, bool isEquip)
    {
        ItemCode = itemCode; ItemType = itemType; Name = name; Description = description; HP = Hp; Hunger = hunger; AttackDamage = attackDamage; AttackRange = attackDelay;
        Defense = denfense; Speed = speed; StackNumber = stackNumber; IsEquip = isEquip;
    }
 
    public string Name, Description, HP, Hunger, AttackDamage, AttackDelay, Defense, AttackRange, Speed, ItemCode, ItemType, StackNumber;
    public bool IsEquip;
}

public class ItemManager : MonoBehaviour
{
    public TextAsset ItemDatas;
    public Dictionary<int, Item> items = new Dictionary<int, Item>() { };


    void Start()
    {
        string[] line = ItemDatas.text.Substring(0, ItemDatas.text.Length - 1).Split('\n');
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');
            items.Add(int.Parse(row[0]), new Item(row[0], row[1], row[2], row[3], (row[4]), (row[5]), (row[6]), 
                (row[7]), (row[8]), (row[9]), (row[10]), row[11], row[12] == "TRUE"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
