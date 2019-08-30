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
        xPos = (Grid.GetComponent<GridGeneration>().xSize) * 0.5f - 0.5f;
        yPos = (Grid.GetComponent<GridGeneration>().ySize) * 0.5f - 0.5f; // смещение потому что центр левой нижней клетки расположен на х 0.5 у 0.5    

        gameObject.transform.position = new Vector3(xPos, yPos, -10); 
    }
}
