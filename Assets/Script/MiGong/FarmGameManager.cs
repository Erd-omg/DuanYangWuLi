using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Linq;

[System.Serializable]
public class RiceData
{
    public float x;
    public float y;
    public float z;
    public bool isCollected;
}

[System.Serializable]
public class RiceDataWrapper
{
    public List<RiceData> data;
}

public class FarmGameManager : MonoBehaviour
{
    public static FarmGameManager Instance;

    [Header("农田设置")]
    public GameObject ricePrefab; // 稻子预制体
    public int minRice = 5;       // 最小稻子数量
    public int maxRice = 10;      // 最大稻子数量
    public LayerMask obstacleLayer;
    public Vector3 fieldSize = new Vector3(40, 0, 40);
    public float gridSize = 0.3f;

    [Header("参考对象")]
    public GameObject farmExit; // 农场出口

    [Header("UI设置")]
    public TMP_Text riceCounterText; // 改为TMP_Text类型
    public GameObject victoryUI; // 新增胜利UI
    public GameObject rulePanel;

    [Header("镰刀")]
    public GameObject liandao;

    [Header("玩家")]
    public GameObject player;

    [Header("机关场景")]
    public string sceneName;
    public SceneSwitcher sceneSwitcher;
    public GameObject door1;
    public GameObject door2;
    public GameObject door3;
    public int level;

    private string firstLoadFlagFile = "firstLoadMiGongFlag.txt";
    private List<RiceData> riceDataList = new List<RiceData>();

    private List<Vector3> walkablePositions = new List<Vector3>();
    private int totalRice;
    public int collectedRice;
    private bool isAllCollected;

    private Color originColor = new Color(0, 0, 0, 1);
    private Color unusableColor = new Color(0.52f, 0, 0, 1);

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError($"发现重复管理器！当前对象：{name} 已有实例：{Instance.name}");
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        Debug.Log($"管理器初始化完成：{name}");


    }

    void Start()
    {
        FindWalkablePositions();

        // 加载基础数据
        if (File.Exists(firstLoadFlagFile))
        {
            Vector3 playerPosition;
            Quaternion playerRotation;
            int riceCount;
            int currentLevel;

            LoadPlayerData(out playerPosition, out playerRotation, out riceCount, out currentLevel);

            collectedRice = riceCount;
            level = currentLevel;

            // 设置玩家初始位置
            if (player != null)
            {
                player.transform.position = playerPosition;
                player.transform.rotation = playerRotation;
            }

            // 加载水稻数据
            LoadRiceData();

            Debug.Log($"初始关卡等级：{level}");
            UpdateDoorStates(level);

            PlantSavedRice();

            liandao.SetActive(true);
            rulePanel.SetActive(false);
        }
        else
        {
            File.WriteAllText(firstLoadFlagFile, "First load flag");
            PlantRice();
            level = 0;
            Debug.Log("新游戏初始化完成");
        }

        farmExit.SetActive(false);
        victoryUI.SetActive(false); // 初始隐藏胜利UI
        UpdateRiceUI(); // 初始化UI显示

        Debug.Log($"门1初始状态：{door1.activeSelf}");
        Debug.Log($"门2初始状态：{door2.activeSelf}");
        Debug.Log($"门3初始状态：{door3.activeSelf}");
    }
    private void Update()
    {
        if (!liandao.activeSelf)
        {
            riceCounterText.text = "请从背包中取出镰刀！";
            riceCounterText.color = unusableColor;
        }
        else
        {
            riceCounterText.text = $"收集进度：{collectedRice}/{totalRice}";
            riceCounterText.color = originColor;
        }
    }

    void FindWalkablePositions()
    {
        walkablePositions.Clear();
        Vector3 startPosition = -fieldSize / 2;

        for (float x = 0; x < fieldSize.x; x += gridSize)
        {
            for (float z = 0; z < fieldSize.z; z += gridSize)
            {
                Vector3 checkPos = startPosition + new Vector3(x, 0.5f, z);
                if (!Physics.CheckSphere(checkPos, 0.3f, obstacleLayer))
                {
                    walkablePositions.Add(checkPos);
                }
            }
        }
    }

    void PlantRice()
    {
        riceDataList.Clear();
        int riceCount = Random.Range(minRice, maxRice + 1);
        riceCount = Mathf.Clamp(riceCount, 0, walkablePositions.Count);

        HashSet<Vector3> usedPositions = new HashSet<Vector3>();

        for (int i = 0; i < riceCount; i++)
        {
            int randomIndex = Random.Range(0, walkablePositions.Count);
            Vector3 spawnPos = walkablePositions[randomIndex];

            // 确保位置不重复
            while (usedPositions.Contains(spawnPos))
            {
                randomIndex = Random.Range(0, walkablePositions.Count);
                spawnPos = walkablePositions[randomIndex];
            }

            usedPositions.Add(spawnPos);
            riceDataList.Add(new RiceData
            {
                x = spawnPos.x,
                y = spawnPos.y,
                z = spawnPos.z,
                isCollected = false
            });
            Instantiate(ricePrefab, spawnPos, Quaternion.identity);
        }

        totalRice = riceCount;
        collectedRice = 0;
        UpdateRiceUI(); // 生成完成后更新UI
        SaveRiceData();
    }

    void PlantSavedRice()
    {
        totalRice = riceDataList.Count;
        collectedRice = riceDataList.Count(d => d.isCollected);

        foreach (var data in riceDataList)
        {
            if (!data.isCollected)
            {
                Vector3 spawnPos = new Vector3(data.x, data.y, data.z);
                Instantiate(ricePrefab, spawnPos, Quaternion.identity);
            }
        }
    }
    public void CollectRice(Vector3 ricePosition)
    {
        var riceData = riceDataList.FirstOrDefault(d =>
            Mathf.Approximately(d.x, ricePosition.x) &&
            Mathf.Approximately(d.y, ricePosition.y) &&
            Mathf.Approximately(d.z, ricePosition.z));
        if (riceData != null && !riceData.isCollected)
        {
            riceData.isCollected = true;
            collectedRice++;
            Debug.Log($"收集稻子！当前进度：{collectedRice}/{totalRice}"); // 新增收集日志
            UpdateRiceUI(); // 每次收集都更新UI
            SaveRiceData();

            // 将稻子添加到背包中
            if (InventoryManager.instance != null)
            {
                InventoryManager.instance.AddItemToInventory("水稻");
            }

            if (collectedRice >= totalRice)
            {
                isAllCollected = true;
                //farmExit.SetActive(true);
                Debug.Log("所有稻子收集完毕！农场出口已开放！");
            }
        }
    }

    void SaveRiceData()
    {
        string riceDataJson = JsonUtility.ToJson(new RiceDataWrapper { data = riceDataList });
        PlayerPrefs.SetString("RiceData", riceDataJson);
        PlayerPrefs.Save();
    }

    void LoadRiceData()
    {
        if (PlayerPrefs.HasKey("RiceData"))
        {
            string riceDataJson = PlayerPrefs.GetString("RiceData");
            riceDataList = JsonUtility.FromJson<RiceDataWrapper>(riceDataJson).data;
        }
    }
    public void TryShowVictory()
    {
        if (isAllCollected)
        {
            victoryUI.SetActive(true);
            Time.timeScale = 0; // 暂停游戏
            Debug.Log("游戏胜利！");
        }
    }

    public void CheckHarvestComplete()
    {
        if (collectedRice >= totalRice)
        {
            Debug.Log("丰收胜利！你完成了所有稻子的收割！");
            // 添加胜利后的逻辑（例如显示丰收界面）
        }
    }

    // 新增UI更新方法
    private void UpdateRiceUI()
    {
        if (riceCounterText != null)
        {
            riceCounterText.text = $"收集进度：{collectedRice}/{totalRice}";
            Debug.Log($"UI已更新 => 当前显示：{collectedRice}/{totalRice}"); // 新增UI更新日志
        }
        else
        {
            Debug.LogError("riceCounterText未赋值！"); // 空引用保护
        }
    }

    // 保存玩家数据
    public void SavePlayerData(Vector3 playerPosition, Quaternion playerRotation, int riceCount, int level)
    {
        PlayerPrefs.SetFloat("PlayerX", playerPosition.x);
        PlayerPrefs.SetFloat("PlayerY", playerPosition.y);
        PlayerPrefs.SetFloat("PlayerZ", playerPosition.z);

        PlayerPrefs.SetFloat("PlayerRotationX", playerRotation.x);
        PlayerPrefs.SetFloat("PlayerRotationY", playerRotation.y);
        PlayerPrefs.SetFloat("PlayerRotationZ", playerRotation.z);
        PlayerPrefs.SetFloat("PlayerRotationW", playerRotation.w);

        PlayerPrefs.SetInt("CollectedRice", riceCount);
        PlayerPrefs.SetInt("PlayerLevel", level);
        PlayerPrefs.Save();
        SaveRiceData();
        Debug.Log("玩家数据已保存");
        Debug.Log("playerPosition" + playerPosition);
    }

    // 加载玩家数据
    public void LoadPlayerData(out Vector3 playerPosition, out Quaternion playerRotation, out int riceCount, out int level)
    {
        playerPosition = new Vector3(
            PlayerPrefs.GetFloat("PlayerX", 0),
            PlayerPrefs.GetFloat("PlayerY", 0),
            PlayerPrefs.GetFloat("PlayerZ", 0)
        );
        playerRotation = new Quaternion(
        PlayerPrefs.GetFloat("PlayerRotationX", 0), // 默认值为 0
        PlayerPrefs.GetFloat("PlayerRotationY", 0),
        PlayerPrefs.GetFloat("PlayerRotationZ", 0),
        PlayerPrefs.GetFloat("PlayerRotationW", 1)
        );

        riceCount = PlayerPrefs.GetInt("CollectedRice", 0);
        level = PlayerPrefs.GetInt("PlayerLevel", 0);
        Debug.Log("玩家数据已加载");
    }

    private void UpdateDoorStates(int level)
    {
        if (door1 != null)
        {
            door1.SetActive(level < 1);
        }

        if (door2 != null)
        {
            door2.SetActive(level < 2);
        }

        if (door3 != null)
        {
            door3.SetActive(level < 3);
        }
    }
}
