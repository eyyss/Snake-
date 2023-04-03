using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBounds 
{
    private Vector2 bounds;
    public ScreenBounds(Camera cam)
    {
		bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
	}
	public Vector2 GetBounds()
    {
        return bounds;
    }
}
