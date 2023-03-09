using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Handles surfaces that are sprayable and paints its texture.
/// </summary>
public class SprayableSurface : MonoBehaviour
{
	public int brushDiameter = 10;
	public int textureResolutionMultiplier = 10;

	private Texture2D sprayTexture;

	// Start is called before the first frame update
	void Start()
    {
		// Create our runtime texture.
        Vector3 meshSize = GetComponent<MeshFilter>().mesh.bounds.size *
            textureResolutionMultiplier;
        sprayTexture = new Texture2D((int)meshSize.x, (int)meshSize.z);

		// Set our texture to the renderer.
		GetComponent<Renderer>().material.mainTexture = sprayTexture;
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void HitSurface(RaycastHit hit)
	{
		// Determine where the texture got hit.
		Vector2 pixelUV = hit.textureCoord;
		pixelUV.x *= sprayTexture.width;
		pixelUV.y *= sprayTexture.height;

		for (int i = 0; i < brushDiameter; i++)
		{
			int x = (int)pixelUV.x;
			int y = (int)pixelUV.y;

			//Increment the X and Y
			x += i;
			y += i;

			//Apply
			sprayTexture.SetPixel(x, y, Color.red);

			//De-increment the X and Y
			x = (int)pixelUV.x;
			y = (int)pixelUV.y;

			x -= i;
			y -= i;

			//Apply
			sprayTexture.SetPixel(x, y, Color.red);
		}
		sprayTexture.Apply();
	}
}
