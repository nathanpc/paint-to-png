using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class RayWhenGrabbed : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject lefthand;
    [SerializeField]
    private GameObject righthand;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DisableLeftRay() {
        lefthand.GetComponent<XRInteractorLineVisual>().enabled = false;
    }
    public void EnableLeftRay() {
        lefthand.GetComponent<XRInteractorLineVisual>().enabled = true;
    }
    public void DisableRightRay() {
        righthand.GetComponent<XRInteractorLineVisual>().enabled = false;
    }
    public void EnableRightRay() {
        righthand.GetComponent<XRInteractorLineVisual>().enabled = true;
    }
}
