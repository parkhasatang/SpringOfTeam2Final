using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using UnityEngine;


// 저장하는 방법
// 1. 저장할 데이터가 존재
// 2. 데이터를 제이슨으로 변환
// 3. 제이슨을 외부에 저장


// 불러오는 법
// 1. 외부에 저장된 제이슨을 가져옴
// 2. 제이슨을 데이터 형태로 변환
// 3. 불러온 데이터를 사용

public class GameData
{
    // 이름, 레벨, 코인, 착용중인 무기    
    public Vector3 playerPosition;
    public SlotNum[] _slotNum;
    public List<SlotData> _slots;   
    
    public GameData(int num)
    {
        _slotNum = new SlotNum[num];
        _slots = new List<SlotData>();
        for (int i = 0; i < num; i++)
        {
            _slotNum[i] = new SlotNum();
            SlotData slot = new SlotData()
            {
                isEmpty = true,
                isChoose = false,
                item = null,
                stack = 0
            };
            _slots.Add(slot);
        }
    }
}
public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public GameObject player;
    private Inventory inventory;
    string path;
    string fileName = "SaveFile";
        
    

    
    private void Awake()
    {        
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        path = Application.persistentDataPath + "/";
        inventory = player.GetComponent<Inventory>();                
    }
    // Start is called before the first frame update
    void Start()
    {   
        if(File.Exists(path + fileName))
        {
            LoadData();
        }            
    }   
    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveData()
    {
        GameData newGameData = new GameData(26);        
        newGameData.playerPosition = player.transform.position;
        
        for (int i = 0; i < 26; i++)
        {
            newGameData._slotNum[i] = inventory.invenSlot[i];
            newGameData._slots[i] = inventory.slots[i];
        }
        string data = JsonUtility.ToJson(newGameData);
        File.WriteAllText(path + fileName, data);
    }

    public void LoadData()
    {
        string data = File.ReadAllText(path + fileName);
        GameData loadGameData = JsonUtility.FromJson<GameData>(data);

        player.transform.position = loadGameData.playerPosition;

        for (int i = 0; i < 26; i++)
        {
            inventory.invenSlot[i] = loadGameData._slotNum[i];
            inventory.slots[i] = loadGameData._slots[i];
            inventory.StackUpdate(i);
        }
    }
}
