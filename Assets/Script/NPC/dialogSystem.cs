using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class dialogSystem : MonoBehaviour
{
    [Header("UI组件")]
    public TMP_Text textLabel;
    public Image faceA;
    public Image faceB;
    public bool talkToSelf;

    [Header("文本文件")]
    public TextAsset textFile;
    public int index;
    public float textSpeed;

    [Header("对话后")]
    public GameObject dialogPanel;
    public GameObject nextObj;
    public bool changeScene = false;
    public bool addItem = false;
    public bool needRemove=false;
    public bool needActiveComponent=false;
    public string SceneName;
    public string ItemName;
    public GameObject removeObj;
    public PathFind PathFind;

    public SceneSwitcher sceneSwitcher;

    bool textFinished;//是否完成打字
    bool cancelTyping;//取消逐字输入

    //public string username;

    List<string> textList = new List<string>();

    void Awake()
    {
        GetTextFromFile(textFile);//读取文件
        faceA.enabled = true;
        SetFaceColor(faceA, 100, 100, 100);

        if (!talkToSelf)
        {
            faceB.enabled = true;
            SetFaceColor(faceB, 100, 100, 100);
        }
        else
        {
            faceB.enabled = false; 
        }

        if (needActiveComponent)
        {
            PathFind.enabled = false;
        }
            
    }

    //一开始就直接输出第一行（不用点鼠标）
    private void OnEnable()
    {
        StartCoroutine(SetTextUI());
    }

    void Update()
    {
        //输出结束后关闭文本框（避免报错）
        if (Input.GetMouseButtonDown(0) && index == textList.Count)
        {
            gameObject.SetActive(false);

            if (addItem)
            {
                if (InventoryManager.instance != null)
                {
                    InventoryManager.instance.AddItemToInventory(ItemName);
                }
            }

            if (changeScene)
            {
                //SceneManager.LoadScene(SceneName);
                sceneSwitcher = GameObject.Find("sceneObj").GetComponent<SceneSwitcher>();

                sceneSwitcher.nextScene = SceneName; 
                sceneSwitcher.SwitchScene();
            }

            else if (dialogPanel != null)
            {
                dialogPanel.SetActive(false);
                if (nextObj != null)
                    nextObj.SetActive(true);

                if (needActiveComponent)
                {
                    // 确保组件被激活
                    PathFind.gameObject.SetActive(true);  // 如果物体本身被禁用需要这行
                    PathFind.enabled = true;

                    // 如果需要可以在这里重置导航目标
                    // PathFind.SetNewPath(newWaypoints, newEndTarget);
                }

                if (removeObj != null)
                {
                    removeObj.SetActive(false);
                }
            }

            index = 0;
            return;
        }

        //保证输出完上一行的内容（避免出现乱码）
        if (Input.GetMouseButtonDown(0))
        {
            if (textFinished && !cancelTyping)//如果上一行打字结束，以及这一行没有取消逐字输入
            {
                StartCoroutine(SetTextUI());//逐字输出这一行
            }
            else if (!textFinished)
            {
                cancelTyping = !cancelTyping;//只要按下鼠标左键，就能改变cancelTyping的bool值

            }
        }
    }

    //读取文件（每一行）
    void GetTextFromFile(TextAsset file)
    {
        textList.Clear();
        index = 0;

        var lineData = file.text.Split('\n');

        foreach (var line in lineData)
        {
            textList.Add(line);
        }
    }

    //逐字输出
    IEnumerator SetTextUI()
    {
        textFinished = false;
        textLabel.text = "";//清空上一行的文字

        //识别到文本中的角色，切换头像
        switch (textList[index].Trim().ToString())
        {
            case "A":
                //faceA.sprite = A;
                SetFaceColor(faceA, 255, 255, 255); 
                if(!talkToSelf)
                    SetFaceColor(faceB, 100, 100, 100); 

                index++;
                break;
            case "B":
                //faceB.sprite = B;
                if (!talkToSelf)
                {
                    SetFaceColor(faceB, 255, 255, 255);
                    SetFaceColor(faceA, 100, 100, 100);
                }

                index++;
                break;
        }

        // 检测是否取消逐字输入，以及获得文本每行的每一个字符
        int letter = 0;
        while (!cancelTyping && letter < textList[index].Length - 1)
        {
            textLabel.text += textList[index][letter];
            letter++;
            yield return new WaitForSeconds(textSpeed);//textSpeed即每个字输出的速度
        }
        textLabel.text = textList[index];//若取消逐字输入，直接整行输出
        cancelTyping = false;

        textFinished = true;
        index++;
    }

    // 设置头像的 RGB 值
    void SetFaceColor(Image face, int r, int g, int b)
    {
        Color color = face.color;
        color.r = r / 255.0f; // 将 RGB 值从 0-255 转换为 0-1
        color.g = g / 255.0f;
        color.b = b / 255.0f;
        face.color = color;
    }
}



