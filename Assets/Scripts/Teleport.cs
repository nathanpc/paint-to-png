using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] GameObject locationPrefab;

    private bool toTeleport = true;
    [SerializeField] private float rayDistance = 6.0f;
    private Transform rayOrigin;
    private GameObject Player;
    private Vector3 pointToTeleport;
    // Start is called before the first frame update
    void Start()
    {
        rayOrigin = this.gameObject.transform;
        locationPrefab = Instantiate(locationPrefab);
        locationPrefab.SetActive(false);
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (toTeleport)
        {
            Teleportation();
            
        }
    }
    public void EnableTeleport()
    {
        toTeleport = true;
    }
    public void DisableTeleport()
    {
        toTeleport = false;
    }

    private void Teleportation()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            locationPrefab.SetActive(true);
            Vector3 startPoint = rayOrigin.position;
            RaycastHit objectHit;
            Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);

            if (Physics.Raycast(ray, out objectHit, rayDistance) )
            {
                Debug.DrawLine(startPoint, objectHit.point, Color.green);
                locationPrefab.transform.position = objectHit.point;
                pointToTeleport = objectHit.point;
            }
            else
            {
                Vector3 startPoint_ = rayOrigin.position + (rayOrigin.forward * rayDistance);
                RaycastHit objectHit_;
                Ray ray_ = new Ray(startPoint_, Vector3.down);

                if (Physics.Raycast(ray_, out objectHit_))
                {
                    Debug.DrawLine(startPoint, startPoint_, Color.green);
                    Debug.DrawLine(startPoint_, objectHit_.point, Color.yellow);
                    locationPrefab.transform.position = objectHit_.point;
                    pointToTeleport = objectHit_.point;
                }
               
            }
          
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            locationPrefab.SetActive(false);


            Player.transform.position = pointToTeleport + new Vector3(0, 1.27f, 0);
        }
    }
   
}
