using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canScript : MonoBehaviour
{
    [SerializeField] private GameObject CanSpot;

    public Color CanColor = Color.green;

    [Range(0.01f, 90f)] public float canTap = 15.0f;

    public float range = 1.5f;


    //[SerializeField]
    private Transform controller ;

    //[SerializeField] 
    private Transform controller2;
    
    private Outline outline;

     private float ControllerDistance ;

    [SerializeField]
    private float rayDistance = 1.35f;

    [SerializeField]
    private Transform rayOrigin = null;

    [SerializeField]
    private GameObject hitOnWall;

    private PlayerMovement movementScript = null;

    private float firstCircleSize;
    private float dist;
    private float dist2;

    public bool Player_close = false;

    public bool deactivateMesh = false;

    public bool onMission = false;

    // Start is called before the first frame update
    void Start()
    {
       
        if (GameObject.FindGameObjectWithTag("Player").name == "Player_PC")
        {
            controller = GameObject.FindGameObjectWithTag("Player").transform;
            controller2 = GameObject.FindGameObjectWithTag("Player").transform;
            movementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
            ControllerDistance = movementScript.closeDistance;
        }
        

        //outline = this.GetComponent<Outline>();
        //outline.enabled = false;
        outline = gameObject.AddComponent<Outline>();

        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = CanColor;
        outline.OutlineWidth = 10f;
        outline.enabled = false;

        rayDistance = range + 1.76f;

    }

    // Update is called once per frame
    void Update()
    {
        changeVariable();

        if (movementScript != null)
        {
            dist = Vector3.Distance(transform.position, controller.position);
            dist2 = Vector3.Distance(controller2.position, transform.position);

            enableOutline();

            Vector3 startPoint = rayOrigin.position;

            RaycastHit objectHit;
            Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);

            if (Physics.Raycast(ray, out objectHit, rayDistance) && movementScript.IsCanGrabbed && movementScript.currentCan == this.gameObject)
            {
                Debug.DrawLine(startPoint, objectHit.point, Color.green);

                hitOnWall.transform.position = objectHit.point + objectHit.normal * 0.10f;
                hitOnWall.transform.forward = objectHit.normal;
                hitOnWall.transform.localScale = new Vector3(1, 1, 1) * (objectHit.distance * firstCircleSize / 0.768f);
                hitOnWall.SetActive(true);
            }
            else
            {
                hitOnWall.SetActive(false);
            }
        }
    }

    private void changeVariable()   // function to change the Size of the cap in the can in both cans (god can and normal can)
    {
        if (this.gameObject.name == "GodCan")
        {
            if (canTap == 6f)
            {
                firstCircleSize = 0.72752f;
            }
            else if (canTap == 15f)
            {
                firstCircleSize = 1.313395f;
            }
            else if (canTap == 23f)
            {
                firstCircleSize = 2.211007f;
            }
        }
        else
        {
            if (canTap == 6f)
            {
                firstCircleSize = 1.7f;
            }
            else if (canTap == 15f)
            {
                firstCircleSize = 3.3f;
            }
            else if (canTap == 23f)
            {
                firstCircleSize = 5f;
            }
        }

        
    }
    private void enableOutline()
    {


        if (   ((dist <= ControllerDistance || dist2 <= ControllerDistance) && !movementScript.IsCanGrabbed) || onMission)
        {
            Player_close = true;
            outline.enabled = true;

        }
        else if (  ((dist > ControllerDistance && dist2 > ControllerDistance) || movementScript.IsCanGrabbed)   )
        {
            outline.enabled = false;
            Player_close = false;


        }
    }
    public void VR_enableOutline()
    {
        outline.enabled = true;
    }
    public void VR_disableOutline()
    {
        outline.enabled = false;
    }
    public void VR_OnCanGrabbed()
    {
        Vector3 startPoint = rayOrigin.position;

        RaycastHit objectHit;
        Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);

        if (Physics.Raycast(ray, out objectHit, rayDistance))
        {
            Debug.DrawLine(startPoint, objectHit.point, Color.green);

            hitOnWall.transform.position = objectHit.point + objectHit.normal * 0.10f;
            hitOnWall.transform.forward = objectHit.normal;
            hitOnWall.transform.localScale = new Vector3(1, 1, 1) * (objectHit.distance * firstCircleSize / 0.768f);
            hitOnWall.SetActive(true);
        }
       
    }
    public void VR_OnCanNOTGrabbed()
    {
        hitOnWall.SetActive(false);
    }
}
