using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using TMPro;

public class RandomNameGenerator : MonoBehaviour
{
    private static string[] names;
    private static System.Random nameRandom;

    public Button btn; // 生成按钮
    public TMP_InputField nameInputField; 
    public Button saveBtn; // 保存按钮

    public static string savedName="小端";

    void Start()
    {
        LoadPlayerNameText();
        btn.onClick.AddListener(GenerateRandomName);
        saveBtn.onClick.AddListener(SaveName);
    }

    private void LoadPlayerNameText()
    {
        // 从 Resources 读取 txt 文件
        TextAsset namesAsset = (TextAsset)Resources.Load("name", typeof(TextAsset));

        if (namesAsset.text != null && namesAsset.text.Length > 0)
        {
            string content = namesAsset.text;
            names = content.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries); 
        }

        if (names != null && names.Length > 0)
        {
            nameRandom = new System.Random();
        }
    }

    // 生成随机名字
    private void GenerateRandomName()
    {
        if (nameRandom != null )
        {
            int nameIndex = nameRandom.Next(0, names.Length);

            string playerName = names[nameIndex];
            nameInputField.text = playerName;
        }
    }

    // 保存名字
    private void SaveName()
    {
        string inputName = nameInputField.text;
        savedName = inputName;
        Debug.Log("保存的名字：" + savedName);
    }
}