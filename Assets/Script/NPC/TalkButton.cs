using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkButton : MonoBehaviour
{
    public GameObject TalkPanel;
    public GameObject tipImg;
    public bool isOpen=false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isOpen)
            tipImg.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        tipImg.SetActive(false);
    }

    void Update()
    {
        if (tipImg.activeSelf && Input.GetKeyUp(KeyCode.E) && !isOpen)
        {
            TalkPanel.SetActive(true);
            isOpen = true;
        }
    }
}

