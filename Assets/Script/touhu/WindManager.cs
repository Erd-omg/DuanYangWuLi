using UnityEngine;

public class WindManager : MonoBehaviour
{
    public float windInterval = 0.3f;
    public float minWindSpeed = -30f;
    public float maxWindSpeed = 30f;
    public static float currentWindSpeed;
    public static string windDirection;

    private float timer;

    void Start()
    {
        timer = windInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            currentWindSpeed = Random.Range(minWindSpeed, maxWindSpeed);
            windDirection = currentWindSpeed > 0 ? "ÏòÓÒ" : "Ïò×ó";
            timer = windInterval;
        }
    }
}