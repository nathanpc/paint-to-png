using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCapSizes : MonoBehaviour
{

    private GameObject[] cans;
    private float _canTap;
    // Start is called before the first frame update
    void Start()
    {
        cans = GameObject.FindGameObjectsWithTag("Can");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CanTapSmall()
    {
        _canTap = 6f;
        foreach (GameObject can in cans)
        {
            can.GetComponent<canScript>().canTap = _canTap;
        }

    }
    public void CanTapMedium()
    {
        _canTap = 15f;
        foreach (GameObject can in cans)
        {
            can.GetComponent<canScript>().canTap = _canTap;
        }

    }
    public void CanTapBig()
    {
        _canTap = 23f;
        foreach (GameObject can in cans)
        {
            can.GetComponent<canScript>().canTap = _canTap;
        }

    }
}
