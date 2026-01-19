using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class ControlChatRoom : MonoBehaviour
{
    #region UI面板
    //输入部分
    public TMP_InputField wordInput;
    public TMP_Text voiceInputText;
    public GameObject voiceInput;
    //聊天框部分
    public TMP_Text chatText;
    public ScrollRect scrollRect;
    public GameObject input;
    public string username;
    #endregion

    public bool isSpeeching =false;
    public bool haveVoiceInput =false;

    // 保存聊天记录
    public bool needSave=true;
    public string saveFilePath = "chat_history.txt";
    public List<string> chatHistory= new List<string>();

    BaiduSpeechTranscribe baiduSpeechTranscribe;

    void Start()
    {
        baiduSpeechTranscribe=input.GetComponent<BaiduSpeechTranscribe>();
        wordInput.text = "";
        voiceInputText.text = "";
        chatText.text = "";
        if(needSave)
            LoadChatHistory();
    }

    void Update()
    {
        isSpeeching = voiceInput.activeSelf;
        haveVoiceInput = baiduSpeechTranscribe.haveVoiceInput;

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            //string inputText = isSpeeching ? voiceInputText.text : wordInput.text;
            if (!isSpeeching)
            {
                chatText.text += "\n";
                //Debug.Log("输入："+wordInput.text);
                AddChatText(wordInput.text);
                //wordInput.text = "";
            }
            else if (haveVoiceInput)
            {
                chatText.text += "\n";
                AddChatText(voiceInputText.text);
                //voiceInputText.text = "";
            }
        }
        username = RandomNameGenerator.savedName;
    }

    // 更新对话框
    void AddChatText(string message)
    {
        string addText = "\n" + "<color=blue>" + username + "</color>: " + message;
        chatText.text += addText;
        scrollRect.verticalNormalizedPosition = 0f;
        Canvas.ForceUpdateCanvases();
        if (needSave)
        {
            chatHistory.Add(addText);
            SaveChatHistory();
        }
    }

    // 存储聊天记录
    public void SaveChatHistory()
    {
        File.WriteAllLines(saveFilePath,chatHistory);
    }

    // 加载聊天记录
    void LoadChatHistory()
    {
        if (File.Exists(saveFilePath))
        {
            chatHistory=new List<string> (File.ReadAllLines(saveFilePath));
        }
        chatText.text=string.Join("\n", chatHistory);
    }
}
