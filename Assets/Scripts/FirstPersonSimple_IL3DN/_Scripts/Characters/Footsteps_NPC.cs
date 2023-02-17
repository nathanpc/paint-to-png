using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps_NPC : MonoBehaviour
{
    public CheckIfGrounded checkIfGrounded;
    public CheckTerrainTexture checkTerrainTexture;

    private AudioSource audioS;

    public int numberOfFootstepClips; //o numero de sons para passos deve ser consistente

    public AudioClip[] grassFootsteps;
    public AudioClip[] puddleFootsteps;
    public AudioClip[] stoneFootsteps;
    public AudioClip[] woodFootsteps;

    private void Start()
    {
        audioS = GetComponent<AudioSource>();
    }

    public void footsteps() //activado por um Animation Event
    {
        if (checkIfGrounded.isGrounded) //apenas continua se estiver no chão
        {

            int r = Random.Range(0, numberOfFootstepClips); //escolhe um numero aleatório


            if (checkIfGrounded.isOnTerrain) //Se detectar que está num terreno
            {
                //Debug.Log("isOnTerrain!");
                checkTerrainTexture.GetTerrainTexture();

                if (checkTerrainTexture.textureValues[0] > 0)
                {
                    audioS.PlayOneShot(grassFootsteps[r]); //----------------------SOMs--------------------------
                }

                if (checkTerrainTexture.textureValues[1] > 0)
                {
                    audioS.PlayOneShot(puddleFootsteps[r]); //----------------------SOMs--------------------------
                }

                /*if (checkTerrainTexture.textureValues[2] > 0) //Apenas usar se tiver 3 Layers (texturas) de Terreno
                {
                    audioS.PlayOneShot(stoneFootsteps[r]);
                }

                if (checkTerrainTexture.textureValues[3] > 0) //Apenas usar se tiver 3 Layers (texturas) de Terreno
                {
                    audioS.PlayOneShot(stoneFootsteps[r]);
                }*/
            }

            else //Se não estiver num terreno, dá o som dependendo da TAG do objecto onde está
            {
                RaycastHit hit;
                Ray ray = new Ray(transform.position, -transform.up);
                if (Physics.Raycast(ray, out hit, 1f)) //raycast para baixo distancia 1m e grava a informação em "hit"
                {
                    switch (hit.transform.tag) //analiza a variavel e dá opções do que fazer com ela 
                    {
                        case "StoneFloor":
                            audioS.PlayOneShot(stoneFootsteps[r]); //----------------------SOMs--------------------------
                            break;

                        case "WoodFloor":
                            audioS.PlayOneShot(woodFootsteps[r]); //----------------------SOMs--------------------------
                            break;

                        default: //Som caso a TAG não seja uma das TAGS em cima
                            audioS.PlayOneShot(stoneFootsteps[r]); //----------------------SOMs--------------------------
                            break;
                    }
                }
            }




        }

    }

}
