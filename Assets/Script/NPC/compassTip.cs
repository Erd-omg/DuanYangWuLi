using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class compassTip : MonoBehaviour
{
    public GameObject compass;
    public TMP_Text tipText;
    public bool showTip = false;
    
    void Update()
    {
        if (!showTip)
        {
            if (!compass.activeSelf)
                tipText.text = "请从背包中取出罗盘！";
            else tipText.text = "请沿罗盘方向行走至捕鱼处";

            //showTip = true;
        }
        
    }
}
