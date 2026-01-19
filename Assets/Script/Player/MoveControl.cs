using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveControl : MonoBehaviour
{
    // 移动的速度
    public float walkSpeed = 3.0f;
    // 旋转速度
    public float rotateSpeed = 1.0f; // 适当调整这个值来控制旋转速度

    // 角色控制器
    public CharacterController PcharacterController = null;

    public GameObject prop;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");
        PcharacterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(prop != null) 
        {
            if (!prop.activeSelf)
                return;
        }
        // 获取水平、垂直轴偏移
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 确定前方向
        Vector3 moveDir = transform.right * h + transform.forward * v;

        // 移动
        PcharacterController.Move(moveDir * walkSpeed * Time.deltaTime);

        // 旋转
        if (moveDir != Vector3.zero)
        {
            // 计算目标旋转方向
            Vector3 targetForward = moveDir.normalized;
            // 使用 Slerp 进行平滑旋转
            transform.forward = Vector3.Slerp(transform.forward, targetForward, Time.deltaTime * rotateSpeed);
        }

        // 手动更新 transform.position
        transform.position = PcharacterController.transform.position;
    }
}