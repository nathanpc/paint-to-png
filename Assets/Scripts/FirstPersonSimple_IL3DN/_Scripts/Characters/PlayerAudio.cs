using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerAudio : MonoBehaviour
{
    private AudioSource audioS;
    public AudioClip jumpSound;
    public AudioClip WaterSplash;
    public AudioClip pickUpSound;

    public AudioMixerSnapshot MainSnapshot;
    public AudioMixerSnapshot AuxSnapshot;

    private void Start() 
    {
        audioS = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            audioS.PlayOneShot(jumpSound);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            audioS.PlayOneShot(WaterSplash); 
        }

        if (other.CompareTag("PickUp")) 
        {
            audioS.PlayOneShot(pickUpSound); 
        }


        if (other.CompareTag("EnemyZone"))
        {
            AuxSnapshot.TransitionTo(2);
        }

    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            audioS.PlayOneShot(WaterSplash); 
        }

        if (other.CompareTag("EnemyZone"))
        {
            MainSnapshot.TransitionTo(2);
        }


    }



}
