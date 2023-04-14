using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.UI.Image;

/// <summary>
/// Handles surfaces that are sprayable and paints its texture.
/// </summary>
public class SprayableSurface : MonoBehaviour
{
	public int textureResolutionMultiplier = 10;
	
    private Texture2D sprayTexture;

	// Start is called before the first frame update
	void Start()
	{
        // Create our runtime texture.
        Vector3 meshSize = GetComponent<MeshFilter>().mesh.bounds.size *
			textureResolutionMultiplier;
		sprayTexture = new Texture2D((int)meshSize.x, (int)meshSize.z);
        Color[] fill = new Color[sprayTexture.width * sprayTexture.height];
        for (int i = 0; i < fill.Length; ++i)
            fill[i] = new Color(0, 0, 0, 0);
        sprayTexture.SetPixels(fill);
        sprayTexture.Apply();

		// Set our texture to the renderer.
		GetComponent<Renderer>().material.mainTexture = sprayTexture;
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	/// <summary>
	/// Paints the surface based on a raycast hit.
	/// </summary>
	/// <param name="hit">Raycast that generated the collision.</param>
    /// <param name="color">Color of the paint.</param>
    /// <param name="diameter">Diameter of the circle to be painted.</param>
	public void PaintSurface(RaycastHit hit, Color color, int diameter)
	{
		// Determine where the texture got hit.
		Vector2 pixelUV = hit.textureCoord;
		pixelUV.x *= sprayTexture.width;
		pixelUV.y *= sprayTexture.height;

		// Draw a filled circle.
		DrawFilledCircle(pixelUV, color, diameter);

        // Apply the changes to the texture itself.
        sprayTexture.Apply();
	}

    /// <summary>
    /// Draws a filled circle in the object's texture.
    /// </summary>
    /// <param name="origin">Center of the circle.</param>
    /// <param name="color">Circle's color.</param>
    /// <param name="diameter">Circle's diameter diameter.</param>
	/// <see cref="https://stackoverflow.com/a/14976268/126353"/>
    protected void DrawFilledCircle(Vector2 origin, Color color, int diameter)
    {
		int x0 = (int)origin.x;
		int y0 = (int)origin.y;
		int radius = diameter / 2;

        int x = radius;
        int y = 0;
        int xChange = 1 - (radius << 1);
        int yChange = 0;
        int radiusError = 0;

        while (x >= y)
        {
			// Paint Y displacement.
            for (int i = x0 - x; i <= x0 + x; i++)
            {
                sprayTexture.SetPixel(i, y0 + y, color);
                sprayTexture.SetPixel(i, y0 - y, color);
            }

			// Paint X displacement.
            for (int i = x0 - y; i <= x0 + y; i++)
            {
                sprayTexture.SetPixel(i, y0 + x, color);
                sprayTexture.SetPixel(i, y0 - x, color);
            }

			// Stuff I don't understand.
            y++;
            radiusError += yChange;
            yChange += 2;
            if (((radiusError << 1) + xChange) > 0)
            {
                x--;
                radiusError += xChange;
                xChange += 2;
            }
        }
    }
}
