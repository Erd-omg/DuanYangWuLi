using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentAIChat : MonoBehaviour
{
    private static PersistentAIChat instance;
    public static PersistentAIChat Instance
    {
        get
        {
            instance=FindObjectOfType<PersistentAIChat>();
            if (instance == null)
            {
                GameObject presistentObj = new GameObject("PresistentAIChat");
                instance=presistentObj.AddComponent<PersistentAIChat>();
                DontDestroyOnLoad(presistentObj);
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

}
