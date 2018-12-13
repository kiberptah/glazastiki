using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour {

    public GameObject Grid;

    private float xPos;
    private float yPos;

	void Start ()
    {
        CenterCamera();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void CenterCamera()
    {
        xPos = Grid.GetComponent<GridGeneration>().xSize / 2;
        yPos = Grid.GetComponent<GridGeneration>().ySize / 2;

        gameObject.transform.position = new Vector3(xPos, yPos, -10);
    }
}
