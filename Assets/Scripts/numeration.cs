using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class numeration : MonoBehaviour {


    public SpriteRenderer number;
    public Sprite[] numberSprites = new Sprite[9];


    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            number.sprite = numberSprites[0];
            Debug.Log("1");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
            number.sprite = numberSprites[1];
        if (Input.GetKeyDown(KeyCode.Alpha3))
            number.sprite = numberSprites[2];
        if (Input.GetKeyDown(KeyCode.Alpha4))
            number.sprite = numberSprites[3];
        if (Input.GetKeyDown(KeyCode.Alpha5))
            number.sprite = numberSprites[4];
        if (Input.GetKeyDown(KeyCode.Alpha6))
            number.sprite = numberSprites[5];
        if (Input.GetKeyDown(KeyCode.Alpha7))
            number.sprite = numberSprites[6];
        if (Input.GetKeyDown(KeyCode.Alpha8))
            number.sprite = numberSprites[7];
        if (Input.GetKeyDown(KeyCode.Alpha9))
            number.sprite = numberSprites[8];
    }
}

