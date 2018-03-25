using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfectSize : MonoBehaviour {

    public Camera theCamera;
    public float height;
    public float width;

    private int ppu = 32;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Screen.height <= 700)
        {
            ppu = 24;
        }
        if (Screen.height > 700)
        {
            ppu = 32;
        }

        height = Screen.height;
        width = Screen.width;

        theCamera.orthographicSize = height / ppu * 0.5f;
    }
}
