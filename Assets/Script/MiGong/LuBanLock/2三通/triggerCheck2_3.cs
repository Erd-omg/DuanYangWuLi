using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerCheck2_3 : MonoBehaviour
{
    public GameObject shiwu, transparency, wrong, right, part;
    public static triggerCheck2_3 instance;
    public bool IsEnterTrans = false;

    private void Awake()
    {
        instance = this;
    }

    //进入碰撞
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "parent_23")
        {
            Debug.Log("碰到了2_3");
            right.SetActive(true); //提示显示

            IsEnterTrans = true;
        }
        else if (other.gameObject.name == "parent_21" || other.gameObject.name == "parent_22")
        {
            Debug.Log("碰撞物体错误");
            wrong.SetActive(true);

            IsEnterTrans = false;
        }
    }

    //离开碰撞
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "parent_23")
        {
            Debug.Log("离开了2_3");
            right.SetActive(false); //提示消失

            IsEnterTrans = false;
        }
        else if (other.gameObject.name == "parent_21" || other.gameObject.name == "parent_22")
        {
            wrong.SetActive(false);
            IsEnterTrans = false;
        }
    }
}
