using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private Mission scrt_mission;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") )
        {
           scrt_mission.OnCollision();
        }

    }
    private void OnTriggerExit(Collider other)
    {
        scrt_mission.entered = false;
    }
    private void Start()
    {
        
    }
}
