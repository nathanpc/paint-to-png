using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfGrounded : MonoBehaviour
{
    public Collider characterCollider;

    public bool isGrounded;
    public bool isOnTerrain;

    private bool landed;
    private bool firstLanded;

    RaycastHit hit;

    private void Update()
    {   
        isGrounded = PlayerGrounded();
        isOnTerrain = CheckOnTerrain();
        //characterCollider = gameObject.GetComponent<CapsuleCollider>();


        if (!isGrounded)
        {
            firstLanded = true;
        }
        else
        {            
            if (firstLanded)
            {
                firstLanded = false;
                //Debug.Log("landed");
                StartCoroutine(delay());
                //----------------------SOMs-------------------------
            }
        }


    }

    bool PlayerGrounded() //TRUE se ao enviar um raio para baixo, detectar um objecto
    {
        return Physics.Raycast(transform.position, Vector3.down, out hit, characterCollider.bounds.extents.y + 0.5f);
    }

    bool CheckOnTerrain() //TRUE se o objecto detectado tiver a TAG "Terreno" || FALSE se tiver outra TAG
    {
        if (hit.collider != null && hit.collider.tag == "Terrain")
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    IEnumerator delay()
    {
        yield return new WaitForSeconds(0.5f);
        //Debug.Log("delayed landed");
        //----------------------SOMs-------------------------
    }
}
