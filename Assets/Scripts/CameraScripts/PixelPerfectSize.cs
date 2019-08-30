using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfectSize : MonoBehaviour {

    public Camera theCamera;
    public float maxsize;

    public GameObject Grid;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {       
        if (Grid.GetComponent<GridGeneration>().ySize > Grid.GetComponent<GridGeneration>().xSize)
        {
            maxsize = Grid.GetComponent<GridGeneration>().ySize;
        }
        else
        {
            maxsize = Grid.GetComponent<GridGeneration>().xSize;
        }
        
        theCamera.orthographicSize =  Mathf.Ceil((maxsize + 3f) / 2f); // еще 3 клетки всегда занимают координаты
    }
}
