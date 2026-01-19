using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Unity.VisualScripting;
using Newtonsoft.Json;


public class poetryTalk : MonoBehaviour
{
    #region UI面板
    //输入输出
    public TMP_Text outputText;
    public TMP_InputField wordInput;
    public GameObject voiceInput;
    public TMP_Text voiceInputText;
    private string inputText;

    //聊天框
    public TMP_Text chatText;
    public ScrollRect scrollRect;
    public string ainame;
    #endregion

    public bool isSpeeching = false;
    public bool haveVoiceInput = false;

    #region 猜诗系统
    //public string Prompt;
    public Button start;
    public TMP_Text physics_clue;
    public TMP_Text verse_clue;
    public Button[] optionButtons;
    public TMP_Text countText;
    public TMP_Text wrongText;
    public GameObject SuccessPanel;
    public GameObject FailPanel;
    public GameObject endCanvas;
    #endregion

    #region 百度云服务
    BaiduSpeechTranscribe baiduSpeechTranscribe;

    private string GetToken_Url;
    private string SendHttp_Url = "https://aip.baidubce.com/rpc/2.0/ai_custom/v1/wenxinworkshop/chat/ernie-4.0-turbo-128k";
    private string grant_type = "client_credentials";
    // ⚠️ 注意：敏感信息已用星号替换，请替换为您的实际 API 密钥
    private string client_id = "****************";
    private string client_secret = "****************";
    private string access_token;
    #endregion

    #region 状态管理
    private List<object> messageHistory = new List<object>(); //对话历史记录
    public bool isGaming = false;  // 答题状态                
    private int count = 3;   // 提问次数                
    public string answer = "";      
    #endregion

    void Start()
    {
        messageHistory.Clear();

        baiduSpeechTranscribe = this.GetComponent<BaiduSpeechTranscribe>();

        GetToken_Url = "https://aip.baidubce.com/oauth/2.0/token?grant_type="
        + grant_type + "&client_id=" + client_id + "&client_secret=" + client_secret;
        StartCoroutine(GetToken(GetToken_Url));

        start.onClick.AddListener(OnStartGame);

        for (int i = 0; i < optionButtons.Length; i++)
        {
            int index = i;
            optionButtons[i].onClick.AddListener(() => OnOptionClick(index));
        }

        StartCoroutine(StartFirstGame());
    }

    void Update()
    {
        isSpeeching = voiceInput.activeSelf;
        haveVoiceInput = baiduSpeechTranscribe.haveVoiceInput;

        inputText = isSpeeching ? voiceInputText.text : wordInput.text;
        inputText = inputText.Replace("\n", "").Replace("\r", "");
        inputText = inputText.Replace("\"", "\\\"");

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (!isSpeeching || (isSpeeching && haveVoiceInput))
            {
                if(isGaming)
                {
                    StartSend();
                    inputText = "";
                    voiceInputText.text = "";
                    wordInput.text = "";
                }
              
            }
        }

    }

    private void StartSend()
    {
        outputText.text = "正在思考中,请稍等...";
        Debug.Log("inputText:" + inputText);
        StartCoroutine(SendHttp(SendHttp_Url,inputText));
    }

    IEnumerator GetToken(string GetToken_Url)
    {
        UnityWebRequest request = new UnityWebRequest(GetToken_Url, "POST");
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError)
        {
            string pattern = "\"access_token\":\"(.*?)\"";
            Match match = Regex.Match(request.downloadHandler.text, pattern);
            access_token = match.Groups[1].Value;
            SendHttp_Url += "?access_token=" + access_token;
        }
        else
        {
            Debug.LogError(request.error);
        }
        request.Dispose();
    }
    IEnumerator SendHttp(string SendHttp_Url,string userInput)
    {
        string payload = BuildRequest(userInput);
        byte[] postData = Encoding.UTF8.GetBytes(payload);

        UnityWebRequest request = new UnityWebRequest(SendHttp_Url, "POST");
        request.uploadHandler = new UploadHandlerRaw(postData);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            // 使用Newtonsoft解析完整响应
            var response = JsonConvert.DeserializeObject<Dictionary<string, object>>(request.downloadHandler.text);
            if (response.TryGetValue("result", out object aiResponseObj))
            {
                string aiResponse = aiResponseObj.ToString()
                    .Replace("\\n", "\n")
                    .Replace("\\\"", "\"");

                // 存储到历史记录中
                messageHistory.Add(new { role = "user", content = userInput });
                messageHistory.Add(new { role = "assistant", content = aiResponse });

                Debug.Log("send:"+userInput);

                if (userInput.Contains("开始游戏"))
                {
                    HandleGameState(true);
                    ExtractCurrentQuestion(aiResponse); //解析题目信息

                    string askRule = "你可以向我提三个关于此诗句的问题，我将回答你是与不是。";
                    UpdateChatDisplay(askRule);
                }
                else if (isGaming)
                {
                    count--;
                    //HandleAnswerResponse(aiResponse); // 处理答题反馈
                    UpdateChatDisplay(aiResponse);
                }
                else if (userInput.Contains("解析选项"))
                {
                    HandleGameState(false);
                    UpdateChatDisplay(aiResponse);
                }

            }
        }
        else
        {
            Debug.LogError($"Request failed: {request.error}");
        }
        request.Dispose();

        outputText.text = "";
    }

    IEnumerator StartFirstGame()
    {
        // 等待获取 Token 完成
        while (string.IsNullOrEmpty(access_token))
        {
            yield return new WaitForSeconds(0.1f);
        }

        OnStartGame();
    }

    private void HandleGameState(bool newState)
    {
        isGaming = newState;

        // 控制按钮状态
        start.interactable = !newState;
        foreach (var btn in optionButtons)
        {
            btn.interactable = newState;
        }

        // 重置次数
        if (newState)
        {
            count = 3;
            countText.text = $"剩余次数：{count}";
        }
    }

    //构建请求体
    string BuildRequest(string userInput)
    {
        var messages = new List<object>();

        //加载系统提示
        TextAsset promptFile = Resources.Load<TextAsset>("Prompt");
        string systemPrompt = promptFile.text.Replace("\n", "\\n").Replace("\"", "\\\"");

        //格式约束
        string enhancedPrompt = systemPrompt + "\n请严格使用以下格式：\n" +
            "【双线索提示】\n" +
            "1. 物理线索：[不超过15字]\n" +
            "2. 诗句线索：[不超过15字]\n" +
            "【选项】\n" +
            "A. [诗句片段]\n" +
            "B. [诗句片段]\n" +
            "C. [诗句片段]\n" +
            "D. [诗句片段]\n" +
            "【剩余提问次数】\n" +
            "3/3\n" +
            "（正确项为[大写字母]）";

        messages.Add(new { role = "user", content = enhancedPrompt });
        messages.Add(new { role = "assistant", content = "好的，请回复“开始游戏”。" });
        

        // 添加历史对话
        foreach (var msg in messageHistory)
        {
            messages.Add(msg);
        }

        // 添加当前用户输入
        if (!string.IsNullOrEmpty(userInput) && !userInput.Equals("开始游戏"))
        {
            messages.Add(new { role = "user", content = userInput + "（请只回答“是”或“不是”）" });
        }
        Debug.Log("Sending messages:\n" + JsonConvert.SerializeObject(messages, Formatting.Indented));
        return JsonConvert.SerializeObject(new { messages = messages });
    }

    private void UpdateChatDisplay(string aiResponse)
    {
        // 空响应检查
        if (string.IsNullOrEmpty(aiResponse))
        {
            Debug.LogWarning("收到空响应");
            return;
        }

        string formattedResponse = aiResponse
            .Replace("\\n", "\n")      // 处理换行符
            .Replace("\\\"", "\"");    // 处理引号

        string displayText = $"\n<color=#800080>{ainame}</color>: {formattedResponse}";
        chatText.text += displayText;
        Canvas.ForceUpdateCanvases();// UI刷新
        scrollRect.verticalNormalizedPosition = 0f;// 滚动到底部
        Canvas.ForceUpdateCanvases();

        countText.text = $"剩余提问：{count}";// 剩余次数
    }

    // 开始游戏
    private void OnStartGame()
    {
        foreach (Transform child in endCanvas.transform)
        {
            child.gameObject.SetActive(false);
        }
        chatText.text = "";

        inputText = "开始游戏";
        messageHistory.Add(new { role = "user", content = inputText });
        StartSend();

        foreach (var btn in optionButtons)
        {
            btn.GetComponentInChildren<TMP_Text>().text = "<rotate=90>正在出题中...";
            Image buttonImage = btn.GetComponent<Image>();
            if (buttonImage != null)
            {
                buttonImage.color = new Color(1, 1, 1, 0.215f); 
            }
        }

        physics_clue.text = "物理线索加载中...";
        verse_clue.text = "诗句线索加载中...";
        countText.text = "剩余提问：3";

        start.interactable = false;
    }

    // 提取题目信息
    private void ExtractCurrentQuestion(string response)
    {
        // 使用多行正则表达式
        Regex regex = new Regex(
            @"【双线索提示】\s*\r?\n" +
            @"1[\.。]\s*物理线索[：:]?\s*(?<physics>.{0,20})\s*\r?\n" +
            @"2[\.。]\s*诗句线索[：:]?\s*(?<verse>.{0,20})\s*\r?\n" +
            @"【选项】\s*\r?\n" +
            @"[AＡ][\.。]\s*(?<A>.+?)\s*\r?\n" +
            @"[BＢ][\.。]\s*(?<B>.+?)\s*\r?\n" +
            @"[CＣ][\.。]\s*(?<C>.+?)\s*\r?\n" +
            @"[DＤ][\.。]\s*(?<D>.+?)\s*\r?\n" +
            @"(【剩余提问次数】|正确项为\s*[A-D])",
            RegexOptions.Singleline
        );

        Match match = regex.Match(response);
        if (match.Success)
        {
            // 更新线索
            physics_clue.text ="物理线索："+ match.Groups["physics"].Value.Trim();
            verse_clue.text ="诗句线索："+ match.Groups["verse"].Value.Trim();

            // 更新选项按钮
            optionButtons[0].GetComponentInChildren<TMP_Text>().text = "<rotate=90>" + match.Groups["A"].Value.Trim().Replace("，", "<br>");
            optionButtons[1].GetComponentInChildren<TMP_Text>().text = "<rotate=90>" + match.Groups["B"].Value.Trim().Replace("，", "<br>");
            optionButtons[2].GetComponentInChildren<TMP_Text>().text = "<rotate=90>" + match.Groups["C"].Value.Trim().Replace("，", "<br>");
            optionButtons[3].GetComponentInChildren<TMP_Text>().text = "<rotate=90>" + match.Groups["D"].Value.Trim().Replace("，", "<br>");

            // 激活选项按钮
            foreach (var btn in optionButtons)
            {
                btn.interactable = true;
            }
        }
        else
        {
            Debug.LogError($"解析失败！响应内容：{response}");
        }

        // 解析正确答案
        Match answerMatch = Regex.Match(response, @"正确项为\s*([A-D])");
        if (answerMatch.Success)
        {
            answer = answerMatch.Groups[1].Value;
        }
    }

    // 处理答案
    private void HandleAnswerResponse(string response)
    {
        // 检测是否包含答案标识
        if (response.Contains("妙哉") || response.Contains("恭喜"))
        {
            isGaming = false;
            count = 0;
        }
        else if (count <= 0)
        {
            isGaming = false;
            chatText.text += "\n<color=red>提问次数已用尽，请点击按钮获取新题</color>";
        }

        // 自动滚动到底部
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }

    // 检查答案
    private void OnOptionClick(int optionIndex)
    {
        //Debug.Log("Option clicked: " + optionIndex);
        if (!isGaming) return;

        char selectedChar = (char)('A' + optionIndex);
        string selectedOption = selectedChar.ToString();

        if (selectedOption == answer)
        {
            // 正确处理
            SuccessPanel.SetActive(true);
            isGaming = false;
            //start.interactable = true;

            // 请求详细解析
            StartCoroutine(RequestAnalysis());
        }
        else
        {
            // 错误处理
            count--;
            countText.text = $"剩余次数：{count}";
            Invoke("StartFadeOut", 1f);
            wrongText.text = $"谜底并非该诗句。";

            // 禁用按钮并改变颜色
            optionButtons[optionIndex].interactable = false;
            Image buttonImage = optionButtons[optionIndex].GetComponent<Image>();
            if (buttonImage != null)
            {
                buttonImage.color = new Color(0.5f, 0.5f, 0.5f,0.215f); // 设置为灰色
            }

            if (count <= 0)
            {
                //chatText.text += $"<br><color=red>机会用尽，谜底为：{answer}。请点击按钮获取新题。</color>";
                FailPanel.SetActive(true);
                isGaming = false;
                start.interactable = true;
            }
        }

        // 滚动到底部
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }

    IEnumerator RequestAnalysis()
    {
        string analysisQuery = $"请解析选项{answer}，包含以下内容：\n" +
                              "1. 诗句完整原文及出处\n" +
                              "2. 对应的物理原理\n" +
                              "3. 相关古代科技应用实例";

        inputText = analysisQuery;
        yield return StartCoroutine(SendHttp(SendHttp_Url,inputText));
    }

    private void StartFadeOut()
    {
        StartCoroutine(FadeOutText());
    }

    private IEnumerator FadeOutText()
    {
        float duration = 0.5f; // 渐隐持续时间
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float alpha = 1f - (elapsedTime / duration);
            wrongText.color = new Color(wrongText.color.r, wrongText.color.g, wrongText.color.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        wrongText.text = ""; // 清空文本内容
        wrongText.color = new Color(wrongText.color.r, wrongText.color.g, wrongText.color.b, 1f); // 恢复初始透明度
    }
}
