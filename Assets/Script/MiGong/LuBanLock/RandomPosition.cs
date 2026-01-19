using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPosition : MonoBehaviour
{
    public List<GameObject> parts;
    public List<Vector3 > positions;

    void Awake()
    {
        PlacePartsRandomly();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlacePartsRandomly()
    {
        //存储未放置的物体索引
        List<int> availableParts= new List<int>();
        for(int i = 0; i < parts.Count; i++)
        {
            availableParts.Add(i);
        }

        //存储未使用的位置索引
        List<int>availablePositions=new List<int>();
        for(int i = 0;i<positions.Count;i++)
        {
            availablePositions.Add(i);
        }

        //随机放置物体
        while(availableParts.Count > 0)
        {
            //随机选一个未放置的物体
            int randomPart=Random.Range(0, availableParts.Count);
            int PartIndex = availableParts[randomPart];
            availableParts.RemoveAt(randomPart);

            //随机选一个未使用的位置
            int randomPostion=Random.Range(0, availablePositions.Count);
            int PositionIndex = availablePositions[randomPostion];
            availablePositions.RemoveAt(randomPostion);

            //移动物体到随机位置
            parts[PartIndex].transform.localPosition = positions[PositionIndex];
        }
    }
}
