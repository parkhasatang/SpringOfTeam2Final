using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static UnityEditor.Progress;

[System.Serializable]
public class Item
{
    public Item(int itemCode, int itemType, string name, string description, float Hp, float hunger, float attackDamage, float attackDelay, float denfense,
        float attackRange, float speed, int stackNumber, bool isEquip, bool rightClick)
    {
        ItemCode = itemCode; ItemType = itemType; Name = name; Description = description; HP = Hp; Hunger = hunger; AttackDamage = attackDamage; AttackRange = attackDelay;
        Defense = denfense; Speed = speed; StackNumber = stackNumber; IsEquip = isEquip; RightClick = rightClick;
    }
    public int ItemCode, ItemType, StackNumber;
    public float HP, Hunger, AttackDamage, AttackDelay, Defense, AttackRange, Speed;
    public string Name, Description;
    public bool IsEquip, RightClick;
}

public class ItemManager : MonoBehaviour
{
    private static ItemManager _instance;

    public static ItemManager instance
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

    public Dictionary<int, Sprite> spriteDictionary;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        _instance = this;

        spriteDictionary = new Dictionary<int, Sprite>();
        SpriteMapping();
    }

    void Start()
    {
        string[] line = ItemDatas.text.Substring(0, ItemDatas.text.Length - 1).Split('\n');
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');
            Debug.Log(row[13]);
            items.Add(int.Parse(row[0]), new Item(int.Parse(row[0]), int.Parse(row[1]), row[2], row[3], float.Parse(row[4]), float.Parse(row[5]), float.Parse(row[6]),
                float.Parse(row[7]), float.Parse(row[8]), float.Parse(row[9]), float.Parse(row[10]), int.Parse(row[11]), bool.Parse(row[12]), bool.Parse(row[13])));
        }
    }




    public Sprite GetSpriteByItemCode(int itemCode)
    {
        // Dictionary에서 해당 인트 값에 대응하는 Sprite를 가져오기
        if (spriteDictionary.TryGetValue(itemCode, out Sprite sprite))
        {
            return sprite;
        }
        else
        {
            Debug.Log("해당 키값에 이미지없음.");
            return null;
        }
    }

    public Item SetItemData(int itemCode)
    {
        return items[itemCode];
    }

    public void SpriteMapping()
    {
        // Resource폴더안 ItemSprite폴더안의 모든 파일을 긁어와서 배열에 저장.
        Sprite[] itemSprite = Resources.LoadAll<Sprite>("ItemSprite");

        for (int i = 0; i < itemSprite.Length; i++)
        {
            int itemCode = int.Parse(itemSprite[i].name);
            spriteDictionary.Add(itemCode, itemSprite[i]);
        }
        Debug.Log("이미지 로드 완료");
    }

    

    /*public void SpriteMapping() 리펙토링 전
    {
        for (int i = 0; i < )
        spriteDictionary.Add(1001, Resources.Load<Sprite>("ItemSprite/1001"));
        spriteDictionary.Add(1301, Resources.Load<Sprite>("ItemSprite/1301"));
        spriteDictionary.Add(2101, Resources.Load<Sprite>("ItemSprite/2101"));
        spriteDictionary.Add(1703, Resources.Load<Sprite>("ItemSprite/1703"));
        spriteDictionary.Add(1713, Resources.Load<Sprite>("ItemSprite/1713"));
        spriteDictionary.Add(1723, Resources.Load<Sprite>("ItemSprite/1723"));
        spriteDictionary.Add(3101, Resources.Load<Sprite>("ItemSprite/3101"));
        spriteDictionary.Add(3011, Resources.Load<Sprite>("ItemSprite/3011"));
        spriteDictionary.Add(3001, Resources.Load<Sprite>("ItemSprite/3001"));
        Debug.Log("이미지 로드 완료");
    }*/
}
