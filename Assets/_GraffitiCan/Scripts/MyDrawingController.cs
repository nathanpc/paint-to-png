using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDrawingController : MonoBehaviour {

    private MySpotDrawer spot;
    private MyDrawable[] drawablesInScene;
    [SerializeField] private GameObject sprayPaint;
    private GameObject drawables;
    public AudioSource Paint_Spray;
    public AudioSource Can_shake;


    //private OVRGrabbable ovrGrabable;
    //public OVRInput.Button sprayButton;
    private Vector3 posLastFrame;



    public float magnitudeOfVelocityToSound = 1.0f;

    private PlayerMovement plyrScpt;
    private bool CanGrabbed;
    private GameObject can;

    [SerializeField]
    private GameObject ThisCan;

    private int uno = 1;

    private bool vr_control_enabled = false;

    private void Awake()
    {
        drawables = GameObject.Find("Drawables");
        plyrScpt = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
    void Start()
    {
        spot = GetComponent<MySpotDrawer>();
        drawablesInScene = drawables.GetComponent<Drawables>().AllDrawables;
        //Paint_Spray = GetComponentInParent<AudioSource>();
        //Can_shake = GetComponent<AudioSource>();

        //ovrGrabable = GetComponentInParent<OVRGrabbable>();

        posLastFrame = transform.position;


        spot.UpdateDrawingMat();

        

    }
    // Update is called once per frame
    void Update () {

        
        if (uno == 1) // if function to start the texture into drawables, otherwise the objects start as black until the user uses can
        {
            foreach (var drawable in drawablesInScene)
                spot.Draw(drawable);
            uno++;
        }

        if (Vector3.Distance(posLastFrame , transform.position )/Time.deltaTime > magnitudeOfVelocityToSound) // function to perform the audio of the can shaking
        {
            //Debug.Log("YEss");
            Can_shake.mute = false;

        }
        else
        {
            Can_shake.mute = true;
        }

        posLastFrame = transform.position;



        
        if (plyrScpt != null) // if the game is not being played in VR
        {
            CanGrabbed = plyrScpt.IsCanGrabbed;
            can = plyrScpt.currentCan;
        }
        
        


        if (Input.GetKey(KeyCode.Mouse1) && can == ThisCan && CanGrabbed )
        {
            spot.UpdateDrawingMat();

            foreach (var drawable in drawablesInScene)
                spot.Draw(drawable);

            sprayPaint.SetActive(true);
            Paint_Spray.mute = false;
            

        }
        else
        {
            sprayPaint.SetActive(false);
            Paint_Spray.mute = true;
        }

        // temp hacky prof fix
        if(vr_control_enabled)
        {
            spot.UpdateDrawingMat();

            foreach (var drawable in drawablesInScene)
                spot.Draw(drawable);

            sprayPaint.SetActive(true);
            Paint_Spray.mute = false;

        }

    }
    public void Paint_enabled()
    {
        vr_control_enabled = true;

        spot.UpdateDrawingMat();

        foreach (var drawable in drawablesInScene)
            spot.Draw(drawable);

        sprayPaint.SetActive(true);
        Paint_Spray.mute = false;
    }
    public void Paint_disabled()
    {
        vr_control_enabled = false;

        sprayPaint.SetActive(false);
        Paint_Spray.mute = true;
    }
}
