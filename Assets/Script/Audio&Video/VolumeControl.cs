using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public AudioSource audiosource;
    public Scrollbar scrollbar;

    private void Start()
    {
        scrollbar.value = audiosource.volume;
    }

    void Update()
    {
        music_volume();
    }

    public void music_volume()
    {
        audiosource.volume = scrollbar.value;
    }
}
