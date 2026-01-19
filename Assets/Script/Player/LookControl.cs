using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookControl : MonoBehaviour
{
    // 鼠标灵敏度
    public float MouseSensitivity = 200.0f;
    // 存储摄像机x轴方向得角度
    float CameraX = 0.0f;
    // 视角上下移动需要设定最大、最小角度
    float CameraXMax = 30f;
    float CameraXMin = 0f;

    // 玩家
    public Transform playerT = null;

    private bool canRotate = true;
    public GameObject talkCanvas;
    private void Start()
    {
        Cursor.visible = true;
        // 鼠标锁定
        Cursor.lockState = 0;
    }

    // Update is called once per frame
    void Update()
    {
        canRotate = true;
        foreach (Transform child in talkCanvas.transform)
        {
            // 如果任何一个子物体被激活，则禁止旋转
            if (child.gameObject.activeSelf)
            {
                canRotate = false;
                break;
            }
        }

        if (canRotate)
        {
            // 获取鼠标得偏移
            float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * MouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * MouseSensitivity;


            // 视角得上下移动，是由鼠标得y轴改变，摄像机需要绕x轴旋转
            CameraX -= mouseY;   // +-能够决定方向
                                 // 在最大值、最小值之间取值
            CameraX = Mathf.Clamp(CameraX, CameraXMin, CameraXMax);
            // 摄像机旋转:localRotation相对于父物体旋转
            transform.localRotation = Quaternion.Euler(CameraX, 0, 0);

            // 视角得左右移动，一般都是360度旋转，不需要额外得变量存储

            // 由鼠标的x坐标决定，摄像机需要绕y轴旋转，人物也需要转
            // 但是摄像机是玩家的子物体，玩家旋转，摄像机就会跟着转
            if (playerT != null)
            {
                // 在现有的基础之上旋转
                playerT.Rotate(playerT.up, mouseX);
            }
        }
    }
}
