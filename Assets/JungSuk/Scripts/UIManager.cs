using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
//public class Item
//{
//    public Item(string itemType, string name, string description, string Hp, string hunger, string attackDamage, string attackDelay, string denfense,
//        string attackRange, string speed, string stackNumber, bool isEquip)
//    { ItemType = itemType; Name = name; Description = description; HP = Hp; Hunger = hunger; AttackDamage = attackDamage; AttackRange = attackDelay;
//        Defense = denfense; Speed = speed; StackNumber = stackNumber; IsEquip = isEquip;}

//    public string ItemType, Name, Description, HP, Hunger, AttackDamage, AttackDelay, Defense, AttackRange, Speed, StackNumber;
//    public bool IsEquip;
//}
public class UIManager : MonoBehaviour
{
    public TextAsset ItemDatas;
    public List<Item> AllItemList;
    //public Dictionary<int, Item> items = new Dictionary<int, Item>()
    //{
    //    { 1001, new Item("WeaponConect", "검", "근접무기 검" ,"0", "0", "10", "0", "0", "1", "0", "1" ,false) }
    //};

    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();

                if (_instance == null)
                {
                    GameObject UIManager = new GameObject("UIManager");
                    _instance = UIManager.AddComponent<UIManager>();
                }
            }
            return _instance;
        }
    }

    public void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }

        _instance = this;
    }
    // Start is called before the first frame update
    //void Start()
    //{
    //    string[] line = ItemDatas.text.Substring(0, ItemDatas.text.Length - 1).Split('\n');
    //    for (int i = 0; i < line.Length; i++)
    //    {
    //        string[] row = line[i].Split('\t');
    //        items.Add(int.Parse(row[0]), new Item(row[1], row[1], row[2], row[3], row[4], row[5], row[6], row[7], row[8], row[9], row[10], row[11] == "TRUE"));
    //    }
        
    //}

    void Save()
    {
        string itemData = JsonConvert.SerializeObject(AllItemList);
        print(itemData);
        File.WriteAllText(Application.dataPath + "/Plugin/AllItemText.txt", "하이");
    }
    void Load()
    {
        string itemData = File.ReadAllText(Application.dataPath + "/Plugin/AllItem.txt");
        AllItemList = JsonConvert.DeserializeObject<List<Item>>(itemData);
    }
}
