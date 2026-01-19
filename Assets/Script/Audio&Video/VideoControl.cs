using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoControl : MonoBehaviour
{
    private float countTime = 0f;
    public float videoTime;//视频时长

    public GameObject rawimage;

    //视频播放完后
    public GameObject NextPanel;
    public bool changeScene=false;
    public string SceneName;

    bool isChanged=false;

    void Start()
    {
        if( NextPanel != null ) 
            NextPanel.SetActive(false);
    }

    
    void Update()
    {
        if (rawimage.activeSelf && !isChanged)
        {
            countTime += Time.deltaTime;
            if (countTime > videoTime)
            {
                if (changeScene)
                {
                    SceneManager.LoadScene(SceneName);
                }
                else 
                {
                    rawimage.SetActive(false);
                    if (NextPanel != null)
                    {
                        NextPanel.SetActive(true);
                        isChanged = true;
                    }
                }
            }
        }
    }
}
