using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerCheck1_1 : MonoBehaviour
{
    public GameObject shiwu, transparency, wrong, right, part;
    public static triggerCheck1_1 instance;
    public bool IsEnterTrans = false;

    private void Awake()
    {
        instance = this;

    }

    //进入碰撞
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "parent_11")
        {
            Debug.Log("碰到了1_1");
            right.SetActive(true); 

            IsEnterTrans = true;
        }
        else if (other.gameObject.name =="parent_12"||other.gameObject.name=="parent_13")
        {
            Debug.Log("1_1碰撞物体错误");
            wrong.SetActive(true);
            
            IsEnterTrans = false;
        }
    }

    //离开碰撞
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "parent_11")
        {
            Debug.Log("离开了1_1");
            right.SetActive(false); 
            
            IsEnterTrans = false;
        }
        else if(other.gameObject.name == "parent_12" || other.gameObject.name == "parent_13")
        {
            wrong.SetActive(false);
            IsEnterTrans = false;
        }
    }
}
