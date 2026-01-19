using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public Vector3 position;
    public Quaternion rotation;

    public PlayerData(Vector3 pos, Quaternion rot)
    {
        position = pos;
        rotation = rot;
    }
}