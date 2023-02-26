using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTerrainTexture : MonoBehaviour
{
    public Transform characterTransform;
    public Terrain terrain;

    public int posX;
    public int posZ;
    public float[] textureValues;

    void Start()
    {
        terrain = Terrain.activeTerrain; //SE existir mais que um objecto de terreno, comentar esta linha e escolhar manualmente que terreno usar
        characterTransform = gameObject.transform;

    }

    void Update()
    {
        //GetTerrainTexture(); //idealmente,por questoes de performance, só deve ser chamado quando é preciso um footstep
    }

    public void GetTerrainTexture()
    {
        ConvertPosition(characterTransform.position);
        CheckTexture();
    }

    void ConvertPosition(Vector3 playerPosition)
    {
        Vector3 terrainPosition = playerPosition - terrain.transform.position;

        Vector3 mapPosition = new Vector3
        (terrainPosition.x / terrain.terrainData.size.x, 0,
        terrainPosition.z / terrain.terrainData.size.z);

        float xCoord = mapPosition.x * terrain.terrainData.alphamapWidth;
        float zCoord = mapPosition.z * terrain.terrainData.alphamapHeight;

        posX = (int)xCoord;
        posZ = (int)zCoord;
    }

    void CheckTexture()
    {
        float[,,] aMap = terrain.terrainData.GetAlphamaps(posX, posZ, 1, 1);
        textureValues[0] = aMap[0, 0, 0];
        textureValues[1] = aMap[0, 0, 1];
        //textureValues[2] = aMap[0, 0, 2]; //Apenas usar se tiver 3 Layers (texturas) de Terreno
        //textureValues[3] = aMap[0, 0, 3]; //Apenas usar se tiver 4 Layers (texturas) de Terreno
    }
}

