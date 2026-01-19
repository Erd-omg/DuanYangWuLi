using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDoor : MonoBehaviour
{
    [Header("场景配置")]
    public string SceneName;
    public int levelSetting;

    [Header("迷宫中组件引用")]
    public SceneSwitcher sceneSwitcher;
    public GameObject player;
    public FarmGameManager farmGameManager;

    private void Start()
    {
        if (sceneSwitcher==null)
        {
            sceneSwitcher = GameObject.Find("sceneObj").GetComponent<SceneSwitcher>();
        }

        //string previousScene = PlayerPrefs.GetString("PreviousScene", "");
        //if (previousScene == SceneName)
        //{
        //    this.gameObject.SetActive(false);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (sceneSwitcher != null)
        {
            if (player != null && farmGameManager != null)
            {
                int newLevel = Mathf.Max(farmGameManager.level, levelSetting);
                farmGameManager.level = newLevel;
                farmGameManager.SavePlayerData(player.transform.position, player.transform.rotation, farmGameManager.collectedRice, newLevel);
                Invoke("SwitchSceneDelayed", 0.1f);
            }
            else
            {
                sceneSwitcher.nextScene = SceneName;
                sceneSwitcher.SwitchScene();
            }
        }

    }

    private void SwitchSceneDelayed()
    {
        sceneSwitcher.nextScene = SceneName;
        sceneSwitcher.SwitchScene();
    }
}
