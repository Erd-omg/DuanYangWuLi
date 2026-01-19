using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class openTalkPanel : MonoBehaviour
{
    public GameObject talk_LaoWeng2;
    public GameObject talk_HuoBan1;
    public GameObject talk_HuoBan2;
    public GameObject talk_self;
    public GameObject talk_laoban;
    public GameObject HuoBan;

    void Start()
    {
        if (InventoryManager.instance != null)
        {
            // previousScene=="catchfish" 
            if (InventoryManager.instance.HasItem("蓝色线团") && !InventoryManager.instance.HasItem("青色线团"))
            {
                talk_LaoWeng2.SetActive(true);
            }
            else talk_LaoWeng2.SetActive(false);

            // previousScene=="MiGong"
            if (InventoryManager.instance.HasItem("青色线团") && !InventoryManager.instance.HasItem("黄色线团"))
            {
                talk_self.SetActive(true);
            }
            else talk_self.SetActive(false);

            // previousScene == "kitchen"
            if (InventoryManager.instance.HasItem("黄色线团"))
            {
                HuoBan.SetActive(true);
                if(!InventoryManager.instance.HasItem("红色线团"))
                {
                    talk_HuoBan1.SetActive(true);
                }
                else
                    talk_HuoBan1.SetActive(false);

            }

            // previousScene=="shejian"
            if (InventoryManager.instance.HasItem("红色线团") && !InventoryManager.instance.HasItem("白色线团"))
            {
                talk_laoban.SetActive(true);
            }
            else
                talk_laoban.SetActive(false);

            // previousScene=="poetry"
            if (InventoryManager.instance.HasItem("白色线团"))
            {
                talk_HuoBan2.SetActive(true);
            }
            else
                talk_HuoBan2.SetActive(false);

        }
        
    }
}
