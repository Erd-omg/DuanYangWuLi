using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    public Vector3 kReferenceVector = new Vector3(0, 0, 1);

    public Transform targetTransform;
    public Transform mPlayerTransform;

    private Vector3 _mTempVector;
    private float _mTempAngle;

    private void Update()
    {

        // 获取玩家的transform
        _mTempVector = mPlayerTransform.forward;
        _mTempVector.y = 0f;
        _mTempVector = _mTempVector.normalized;

        // 获取到参考点的距离
        _mTempVector = _mTempVector - kReferenceVector;
        _mTempVector.y = 0f;
        _mTempVector = _mTempVector.normalized;

        if (_mTempVector == Vector3.zero)
        {
            _mTempVector = new Vector3(1, 0, 0);
        }

        // 计算旋转角度
        _mTempAngle = Mathf.Atan2(_mTempVector.x, _mTempVector.z);
        _mTempAngle = (_mTempAngle * Mathf.Rad2Deg + 90f) * 2f;

        // 设置旋转
        transform.rotation = Quaternion.AngleAxis(-_mTempAngle, kReferenceVector);

    }
}


