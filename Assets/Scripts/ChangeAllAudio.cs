using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAllAudio : MonoBehaviour
{
    private AudioSource[] audios;
    // Start is called before the first frame update
    void Start()
    {
        audios = FindObjectsOfType<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TurnAudioON()
    {
        for (int i = 0; i < audios.Length; i++)
        {
            audios[i].enabled = true;

        }
    }
    public void TurnAudioOFF()
    {
        for (int i = 0; i < audios.Length; i++)
        {
            audios[i].enabled = false;

        }
    }
}
