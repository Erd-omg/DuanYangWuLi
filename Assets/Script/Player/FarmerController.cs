using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
public class FarmerController : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rice")) // 修改标签为Rice
        {
            Destroy(other.gameObject);
            FarmGameManager.Instance.CollectRice(other.transform.position);

            // 可以添加收割音效
            // AudioManager.Instance.PlayHarvestSound();
        }
        else if (other.CompareTag("FarmExit")) // 修改出口标签
        {
            FarmGameManager.Instance.CheckHarvestComplete();
        }
    }
}