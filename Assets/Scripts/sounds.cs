using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sounds : MonoBehaviour
{
    private Slider sounds_slider;
    private AudioSource[] audios;
    
    // Start is called before the first frame update
    void Start()
    {
        sounds_slider = GetComponent<Slider>();
        audios = FindObjectsOfType<AudioSource>();
       
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public void OnValueChange()
    {
        for (int i = 0; i < audios.Length; i++)
        {
            if (!audios[i].CompareTag("Music"))
            {
                audios[i].volume = sounds_slider.value;
            }
            
        }
        
    }
    
}
