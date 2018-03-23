using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveRook : MonoBehaviour
{

    bool isSelected;

    public float xCursor;
    public float yCursor;
    // Use this for initialization
    void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        testControls();
        mouseTracking();
        movingPiece();


    }

    private void OnMouseDown()
    {
        isSelected = !isSelected;

    }

    private void OnMouseDrag()
    {
        //Vector3 mouseposition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
        //                                                                     Input.mousePosition.y, 10));

        //transform.position = objPosition;
    }

    private void testControls()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.position += new Vector3(0, 1, 0);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.position += new Vector3(0, -1, 0);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position += new Vector3(-1, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position += new Vector3(1, 0, 0);
        }
    }

    private void mouseTracking()
    {
        /*
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        */

        Vector3 mouseposition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                                                                            Input.mousePosition.y, 10));
        xCursor = mouseposition.x;
        yCursor = mouseposition.y;

        //xCursor = Mathf.Round(mouseposition.x);
        //yCursor = Mathf.Round(mouseposition.y);
    }


    private void movingPiece()
    {
        if (xCursor <= 8 && xCursor > 0 && yCursor <= 8 && yCursor > 0 && isSelected == true)
        {
            if ((xCursor == transform.position.x || yCursor == transform.position.y) && Input.GetButtonDown("Fire1"))
            {
                transform.position = new Vector3(xCursor, yCursor, 0);
            }
        }
    }
}
