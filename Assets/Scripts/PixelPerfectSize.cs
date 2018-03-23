using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfectSize : MonoBehaviour {

    public Camera theCamera;
    public float height;
    public float width;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        height = Screen.height;
        width = Screen.width;

        theCamera.orthographicSize = height / 32f * 0.5f;
    }
}
