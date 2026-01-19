using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int totalFish = 10; // 总鱼数
    public int caughtFish = 0; // 已捉到的鱼数

    // UI 元素
    public GameObject successPanel; // 成功面板
    public GameObject failPanle;
    public TMP_Text temporaryText;
    public TMP_Text fishCountText;
    public TMP_Text Time;

    private Color originColor = new Color(0, 0, 0, 1);
    private Color unusableColor = new Color(0.52f, 0, 0, 1);

    public GameObject rulePanel;
    public GameObject yucha;

    private float countdownTime = 60f; // 倒计时时间

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // 初始化UI状态
        if (successPanel != null) successPanel.SetActive(false);
        if (temporaryText != null) temporaryText.gameObject.SetActive(false);
        if (failPanle != null) failPanle.SetActive(false);
        UpdateFishCountText();
    }

    void Start()
    {
        StartCoroutine(Countdown());
    }

    void Update()
    {
        if (!yucha.activeSelf)
        {
            fishCountText.text = "请从背包中取出鱼叉！";
            fishCountText.color = unusableColor;
        }
        else
        {
            fishCountText.text = $"已捕到：{caughtFish}条";
            fishCountText.color = originColor;
        }
        // 按R键跳转场景
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Village");
        }
    }

    private IEnumerator Countdown()
    {
        while (countdownTime > 0)
        {
            countdownTime -= UnityEngine.Time.deltaTime; // 修正为 UnityEngine.Time.deltaTime
            if (Time != null)
            {
                Time.text = $"{Mathf.CeilToInt(countdownTime)}";
            }
            yield return null;
        }

        if (failPanle != null)
        {
            failPanle.SetActive(true);
        }
    }

    public void CatchFish()
    {
        caughtFish++;
        UpdateFishCountText();

        // 将鱼添加到背包中
        if (InventoryManager.instance != null)
        {
            InventoryManager.instance.AddItemToInventory("鱼");
        }

        if (caughtFish >= 6 && caughtFish < totalFish)
        {
            // 显示成功面板和临时文本
            if (successPanel != null) successPanel.SetActive(true);
            if (temporaryText != null)
            {
                temporaryText.text = "恭喜！你已经捕到足够的鱼了！\n（按左上角返回键离开河岸边）";
                temporaryText.gameObject.SetActive(true);
                StartCoroutine(HideTextAfterDelay(3f));
            }
        }
        else if (caughtFish >= totalFish)
        {
            EndGame();
        }
    }

    // 更新捕鱼计数文本
    private void UpdateFishCountText()
    {
        if (fishCountText != null)
        {
            fishCountText.text = $"已捕到：{caughtFish}条";
        }
    }

    // 隐藏文本的协程
    private IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (temporaryText != null)
        {
            temporaryText.gameObject.SetActive(false);
        }
    }

    // 结束游戏
    public void EndGame()
    {
        Debug.Log("Game Over!");
        // 显示最终成功面板
        if (successPanel != null)
        {
            successPanel.SetActive(true);
            if (temporaryText != null)
            {
                temporaryText.text = "太棒了！你完成了所有捕鱼任务！";
                temporaryText.gameObject.SetActive(true);
            }
        }
    }

    // 重新加载当前场景
    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // 成功面板按钮调用的方法
    public void OnSuccessPanelButtonClick()
    {
        // 可以在这里添加跳转到其他场景的逻辑
        SceneManager.LoadScene("Village");
    }
}