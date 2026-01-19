using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Net.Http;
using Baidu.Aip.Speech;

public static class TipsReference
{
    public const string CANT_FIND_MICROPHONE = "无法找到麦克风";
    public const string NOTHING_RECORD = "未检测到语音";

    public enum RECORD_TYPE
    {
        NoMicroPhone,
        Normal,
        Error,
        None
    }
}
public class BaiduSpeechTranscribe : MonoBehaviour
{
    #region UI面板
    public Button voiceInput;
    public TMP_Text BtnText;
    public TMP_Text voiceText;
    public AudioSource source;
    private AudioClip AClip;
    #endregion

    private ListenButton listenBtn; // 继承重写的Button类
    public bool haveVoiceInput=false;

    //bool isRecording=false;
    //float recordingTime = 0f;
    //const float maxTime = 60f;

    #region 百度语音技术
    // ⚠️ 注意：敏感信息已用星号替换，请替换为您的实际 API 密钥
    const string API_KEY = "****************";
    const string SECRET_KEY = "****************";
    const string authHost = "https://aip.baidubce.com/oauth/2.0/token";
    #endregion

    string accessToken;
    private Asr aipClient;  // 百度语音识别SDK

    void Start()
    {
        // ⚠️ 注意：应用 ID 已用星号替换，请替换为您的实际应用 ID
        aipClient = new Asr("*********",API_KEY, SECRET_KEY);   // 创建SDK的实例
        aipClient.Timeout = 6000;   // 超时时长为6000毫秒
        accessToken = GetAccessToken(); // 保存当前应用的Token

        // 获取自定义Button的实例
        listenBtn = voiceInput.GetComponent<ListenButton>();
        listenBtn.OnStartRecordEvent += StartRecording;
        listenBtn.OnStopRecordEvent += StopRecording;
    }

    void StartRecording()
    {
        SetText("");
        if (Microphone.devices.Length > 0)
        {
            string device = Microphone.devices[0];
            AudioClip clip = Microphone.Start(device, true, 60, 16000);
            source.clip = clip;
            AClip = clip;
            BtnText.text = "正在说话";
        }
        else
        {
            SetText(TipsReference.CANT_FIND_MICROPHONE);
            listenBtn.ReleaseClickEvent(TipsReference.RECORD_TYPE.NoMicroPhone);
        }

    }
    void StopRecording()
    {
        Microphone.End(Microphone.devices[0]);
        StartCoroutine(Recognition(AClip));

        BtnText.text = "长按按钮，开始说话...";
    }

    void SetText(string result)
    {
        voiceText.text = result;
    }

    // 将录音数据发送到百度智能云进行转写
    public IEnumerator Recognition(AudioClip clip)
    {
        if (clip == null)
        {
            SetText("录音数据为空");
            yield break;
        }
        float[] sample = new float[AClip.samples];
        AClip.GetData(sample, 0);
        short[] intData = new short[sample.Length];
        byte[] byteData = new byte[intData.Length * 2];

        for (int i = 0; i < sample.Length; i++)
        {
            intData[i] = (short)(sample[i] * short.MaxValue);
        }

        Buffer.BlockCopy(intData, 0, byteData, 0, byteData.Length);

        if (byteData.Length == 0)
        {
            SetText("录音数据为空");
            yield break;
        }

        var result = aipClient.Recognize(byteData, "pcm", 16000);
        var speaking = result.GetValue("result");

        if (speaking == null)
        {
            SetText(TipsReference.NOTHING_RECORD);
            StopAllCoroutines();
            yield return null;
        }

        string usefulText = speaking.First.ToString();
        SetText(usefulText);
        haveVoiceInput = true;

        yield return 0;
    }

    // 获取 Access Token
    private string GetAccessToken()
    {
        HttpClient client = new HttpClient();
        List<KeyValuePair<string, string>> paraList = new List<KeyValuePair<string, string>>();
        paraList.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
        paraList.Add(new KeyValuePair<string, string>("client_id", API_KEY));
        paraList.Add(new KeyValuePair<string, string>("client_secret", SECRET_KEY));

        HttpResponseMessage response = client.PostAsync(authHost, new FormUrlEncodedContent(paraList)).Result;
        string result = response.Content.ReadAsStringAsync().Result;
        //Debug.Log("result is " + result);
        //if (result != null) tImage.color = tokenGotColor;
        return result;
    }

    //public void DisplayClip()
    //{
    //    source.Play();
    //}
}
    
