using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Unity.VisualScripting;

[System.Serializable]
public class Message
{
    public string role;
    public string content;
}

[System.Serializable]
public class ErnieRequest
{
    public string system;
    public List<Message> messages;
}

[System.Serializable]
public class ErnieResponse
{
    public string result;
    public int error_code;
    public string error_msg;
}

public class BaiduTranslate : MonoBehaviour
{
    #region UI���
    //�������
    public TMP_Text outputText;
    public TMP_InputField wordInput;
    public GameObject voiceInput;
    public TMP_Text voiceInputText;
    private string inputText;

    //�����
    public TMP_Text chatText;
    public ScrollRect scrollRect;
    public string ainame;
    #endregion

    public bool isSpeeching = false;
    public bool haveVoiceInput = false;

    //���������¼
    public GameObject chatPanel;
    ControlChatRoom controlChatRoom;

    #region �ٶ��Ʒ���
    BaiduSpeechTranscribe baiduSpeechTranscribe;

    private string GetToken_Url;
    private string SendHttp_Url = "https://aip.baidubce.com/rpc/2.0/ai_custom/v1/wenxinworkshop/chat/ernie-4.0-turbo-128k";
    private string grant_type = "client_credentials";
    // ⚠️ 注意：敏感信息已用星号替换，请替换为您的实际 API 密钥
    private string client_id = "****************";
    private string client_secret = "****************";
    private string access_token;
    #endregion

    private List<Message> messagesHistory = new List<Message>();

    void Start()
    {
        controlChatRoom = chatPanel.GetComponent<ControlChatRoom>();
        baiduSpeechTranscribe=this.GetComponent<BaiduSpeechTranscribe>();

        GetToken_Url = "https://aip.baidubce.com/oauth/2.0/token?grant_type="
        + grant_type + "&client_id=" + client_id + "&client_secret=" + client_secret;
        StartCoroutine(GetToken(GetToken_Url));
    }

    void Update()
    {
        isSpeeching = voiceInput.activeSelf;
        haveVoiceInput = baiduSpeechTranscribe.haveVoiceInput;

        inputText = isSpeeching?voiceInputText.text:wordInput.text;
        inputText = inputText.Replace("\n", "").Replace("\r", "");
        inputText = inputText.Replace("\"", "\\\"");

        //Debug.Log("wordInput.text: " + wordInput.text);
        //Debug.Log("inputText: " + inputText);

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if(!isSpeeching || (isSpeeching && haveVoiceInput))
            {
                //Debug.Log("test"+inputText);
                StartSend();
                inputText = "";
                voiceInputText.text = "";
                wordInput.text="";
            }
        }
    }

    private void StartSend()
    {
        // �����û���Ϣ����ʷ
        Message userMessage = new Message();
        userMessage.role = "user";
        userMessage.content = inputText;
        messagesHistory.Add(userMessage);

        outputText.text = "����˼����,���Ե�...";
        StartCoroutine(SendHttp(SendHttp_Url));
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
    IEnumerator SendHttp(string SendHttp_Url)
    {
        //Debug.Log(inputText);
        string systemPrompt = string.Format(
            "���{0}������һ���й��Ŵ�����֪ʶѧϰ���֣����Է��Ϊ�ŷ磬ÿ�λش���100�����ҡ����user���������й��Ŵ������޹أ����������ص��й��Ŵ��������⡣",
            ainame
        );

        // ����������
        ErnieRequest requestBody = new ErnieRequest();
        requestBody.system = systemPrompt;
        requestBody.messages = messagesHistory;

        string Sessagejson = JsonUtility.ToJson(requestBody);
        byte[] SendJson = Encoding.UTF8.GetBytes(Sessagejson);

        UnityWebRequest sendrequest = new UnityWebRequest(SendHttp_Url, "POST");
        sendrequest.uploadHandler = new UploadHandlerRaw(SendJson);
        sendrequest.downloadHandler = new DownloadHandlerBuffer();
        sendrequest.SetRequestHeader("Content-Type", "application/json");
        yield return sendrequest.SendWebRequest();

        if (sendrequest.result == UnityWebRequest.Result.Success)
        {
            ErnieResponse response = JsonUtility.FromJson<ErnieResponse>(sendrequest.downloadHandler.text);
            if (response.error_code == 0)
            {
                string aiResponse = response.result.Replace("\\n", "\n");

                // ����AI�ظ�����ʷ
                Message assistantMessage = new Message();
                assistantMessage.role = "assistant";
                assistantMessage.content = aiResponse;
                messagesHistory.Add(assistantMessage);

                // �����������
                if (!string.IsNullOrEmpty(aiResponse))
                {
                    if (isSpeeching) chatText.text += "\n";
                    string addText = "\n<color=green>" + ainame + "</color>: " + aiResponse;
                    chatText.text += addText;
                    Canvas.ForceUpdateCanvases();
                    scrollRect.verticalNormalizedPosition = 0f;
                    Canvas.ForceUpdateCanvases();
                    if (controlChatRoom.needSave)
                    {
                        controlChatRoom.chatHistory.Add(addText);
                        controlChatRoom.SaveChatHistory();
                    }
                }
            }
        }
        else
        {
            Debug.LogError(sendrequest.error);
        }
        sendrequest.Dispose();
        outputText.text = "";
    }
    
}
