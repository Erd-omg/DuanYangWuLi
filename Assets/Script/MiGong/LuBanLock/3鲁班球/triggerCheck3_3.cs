using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerCheck3_3 : MonoBehaviour
{
    public GameObject shiwu, transparency, wrong, right, part;
    public static triggerCheck3_3 instance;
    public bool IsEnterTrans = false;

    private void Awake()
    {
        instance = this;

    }

    //进入碰撞
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "parent_33")
        {
            Debug.Log("碰到了3_3");
            right.SetActive(true);

            IsEnterTrans = true;
        }
        else if (other.gameObject.name == "parent_31" ||
                 other.gameObject.name == "parent_32" ||
                 other.gameObject.name == "parent_34" ||
                 other.gameObject.name == "parent_35" ||
                 other.gameObject.name == "parent_36")
        {
            Debug.Log("3_3碰撞物体错误");
            wrong.SetActive(true);

            IsEnterTrans = false;
        }
    }

    //离开碰撞
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "parent_33")
        {
            Debug.Log("离开了3_3");
            right.SetActive(false);

            IsEnterTrans = false;
        }
        else if (other.gameObject.name == "parent_31" ||
                 other.gameObject.name == "parent_32" ||
                 other.gameObject.name == "parent_34" ||
                 other.gameObject.name == "parent_35" ||
                 other.gameObject.name == "parent_36")
        {
            wrong.SetActive(false);
            IsEnterTrans = false;
        }
    }
}
