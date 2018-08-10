using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeType : MonoBehaviour {

    public int unitType; // пехота/баттлсют

    public Sprite[] unitSprites = new Sprite[4];

	// Use this for initialization
	void Start ()
    {
        if (unitType < 0 || unitType >= unitSprites.Length)
        {
            unitType = 0;
        }


	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = unitSprites[0];
            unitType = 0;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = unitSprites[1];
            unitType = 1;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = unitSprites[2];
            unitType = 2;
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = unitSprites[3];
            unitType = 3;
        }
    }
}
