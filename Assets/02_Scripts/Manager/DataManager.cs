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
    public Item[] slotItemData;

}
public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public GameObject player;
    string path;
    string fileName = "SaveFile";
        
    GameData newGameData = new GameData();

    
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        path = Application.persistentDataPath + "/";
        LoadData();
    }
    // Start is called before the first frame update
    void Start()
    {
        player.transform.position = newGameData.playerPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveData()
    {
        newGameData.playerPosition = player.transform.position;
        string data = JsonUtility.ToJson(newGameData);
        File.WriteAllText(path + fileName, data);
    }

    public void LoadData()
    {
        string data = File.ReadAllText(path + fileName);
        newGameData = JsonUtility.FromJson<GameData>(data);
    }
}
