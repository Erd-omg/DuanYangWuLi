using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SavePlayerData : MonoBehaviour
{
    public string saveFilePath = "/PlayerData1.json";
    public string firstLoadFlagFile = "firstLoadFlag.txt";

    void Start()
    {
        if (File.Exists(firstLoadFlagFile))
        {
            Load();
        }
        else
        {
            File.WriteAllText(firstLoadFlagFile, "First load flag");
            Debug.Log("First load flag");
        }

    }

    private void Update()
    {
        Debug.Log("玩家所在位置："+this.gameObject.transform.position);
        Save();
    }

    public void Load()
    {
        string fullPath = Application.persistentDataPath + saveFilePath;

        if (File.Exists(fullPath))
        {
            // 读取保存的数据
            string jsonData = File.ReadAllText(fullPath);
            PlayerData data = JsonUtility.FromJson<PlayerData>(jsonData);

            // 加载玩家位置和朝向
            transform.position = data.position;
            transform.rotation = data.rotation;


            Debug.Log("Player data loaded from " + fullPath);
            //Debug.Log("Loadname:" + name);
            //Debug.Log(data.position + " " + data.rotation);
        }
        else
        {
            Debug.Log("No saved player data found. Using initial position.");
        }

    }
    public void Save()
    {
        // 获取玩家当前位置和朝向
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;
        //Debug.Log("Save name:" + name);
        Debug.Log("保存的位置"+position + " " + rotation);
        
        // 创建 PlayerData 对象
        PlayerData data = new PlayerData(position, rotation);

        // 使用 JsonUtility 序列化数据
        string jsonData = JsonUtility.ToJson(data);

        // 保存到文件
        string fullPath = Application.persistentDataPath + saveFilePath;
        File.WriteAllText(fullPath, jsonData);

        Debug.Log("Player data saved to " + fullPath);
    }
}
