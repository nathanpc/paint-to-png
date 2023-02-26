﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingController : MonoBehaviour {

    public SpotDrawer spot;
    public SpotDrawer pinSpot;
    public Drawable[] drawables;

    
	// Update is called once per frame
	void Update () {
        spot.color = Color.HSVToRGB(Mathf.Repeat(Time.time * 0.05f, 1f), 1f, 1f);

        spot.UpdateDrawingMat();
        foreach (var drawable in drawables)
            spot.Draw(drawable);

        pinSpot.color = Color.HSVToRGB(Mathf.Repeat(Time.time * 0.5f, 1f), 1f, 1f);
        //pinSpot.color = Color.white;
        pinSpot.UpdateDrawingMat();
        foreach (var drawable in drawables)
            pinSpot.Draw(drawable);

        //if (Input.GetMouseButton(0))
        //{
        //    var cam = Camera.main;
        //    var pos = Input.mousePosition;
        //    pos.z = 5f;
        //    pos = cam.ScreenToWorldPoint(pos);

        //    pinSpot.transform.position = cam.transform.position;
        //    pinSpot.transform.LookAt(pos);

        //    pinSpot.color = Color.HSVToRGB(Mathf.Repeat(Time.time * 0.5f, 1f), 1f, 1f);
        //    //pinSpot.color = Color.white;
        //    pinSpot.UpdateDrawingMat();
        //    foreach (var drawable in drawables)
        //        pinSpot.Draw(drawable);
        //}
	}
}
