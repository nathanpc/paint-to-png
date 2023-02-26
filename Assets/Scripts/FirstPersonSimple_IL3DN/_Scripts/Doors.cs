using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    private bool inTrigger;
    private Animator anim;
    private AudioSource audioS;
    public AudioClip doorOpen;
    public AudioClip doorClose;
    private bool isDoorOpen;
    public GameObject textInteract;



    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        audioS = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            if (inTrigger)
            {
                anim.SetTrigger("Trigger");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inTrigger = true;
            //Debug.Log("Player Close");
            textInteract.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other) //1
    {
        if (other.CompareTag("Player"))
        {
            inTrigger = false;
            //Debug.Log("Player Far");
            textInteract.gameObject.SetActive(false);
        }
    }

    public void doorSound() 
    {
        if (isDoorOpen)
        {
            //Debug.Log("CloseDoor");
            isDoorOpen = false;
            audioS.PlayOneShot(doorClose); //----------------------SOM--------------------------
        }
        else
        {
            //Debug.Log("OpenDoor");
            isDoorOpen = true;
            audioS.PlayOneShot(doorOpen); //----------------------SOM--------------------------
        }
    }
}



