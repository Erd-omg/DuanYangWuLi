using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    public bool NeedAdd = false;
    public string ItemName;
    public string nextScene;
    public GameObject player;

    void Start()
    {
        // 获取按钮组件
        Button button = GetComponent<Button>();
        if (button != null)
        {
            // 为按钮的点击事件添加监听器
            button.onClick.AddListener(SwitchScene);
        }
        //if (SceneManager.GetActiveScene().name == "Village")
        //    player = GameObject.FindGameObjectWithTag("Player");
        
    }
    private void Update()
    {
        //if (player != null)
        //    player.GetComponent<SavePlayerData>().Save();
    }

    // 新增重载方法
    /*public void SwitchScene(string targetSceneName)
    {
       
        // 保存当前场景作为"上一个场景"
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);

        SceneLoader sceneLoader = CloudManager.Instance?.GetComponent<SceneLoader>();
        if (sceneLoader != null)
        {
            sceneLoader.LoadScene(targetSceneName);
        }
        else
        {
            Debug.LogError("场景加载器未找到！");
        }

   
    }*/
    public void SwitchScene()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1; // 恢复时间流速
        }
        if (NeedAdd)
        {
            if (InventoryManager.instance != null)
            {
                InventoryManager.instance.AddItemToInventory(ItemName);
            }
        }
        //if (player != null)
        //    player.GetComponent<SavePlayerData>().Save();

        // 保存当前场景作为“上一个场景”
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
        // 加载指定名称的场景
        //SceneManager.LoadScene(nextScene);
        SceneLoader sceneLoader = CloudManager.Instance.GetComponent<SceneLoader>();
        if (sceneLoader != null)
        {
            sceneLoader.LoadScene(nextScene);
        }
    }
}
