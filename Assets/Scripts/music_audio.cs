using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class music_audio : MonoBehaviour
{
    private AudioSource music;

    private Slider Music_slider;

    // Start is called before the first frame update
    void Start()
    {
        Music_slider = GetComponent<Slider>();
        music = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void changedValues()
    {
        music.volume = Music_slider.value;
    }
}
