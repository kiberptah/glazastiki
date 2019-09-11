using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class numeration : MonoBehaviour {


    private Text unitNumber;
    public int number;

    void Start ()
    {
        unitNumber = gameObject.transform.GetChild(0).GetComponentInChildren<Text>();

        if (number > 0 && number <= 9)
            unitNumber.text = number.ToString();
    }
	
	void Update ()
    {

    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            number = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            number = 2;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            number = 3;
        if (Input.GetKeyDown(KeyCode.Alpha4))
            number = 4;
        if (Input.GetKeyDown(KeyCode.Alpha5))
            number = 5;
        if (Input.GetKeyDown(KeyCode.Alpha6))
            number = 6;
        if (Input.GetKeyDown(KeyCode.Alpha7))
            number = 7;
        if (Input.GetKeyDown(KeyCode.Alpha8))
            number = 8;
        if (Input.GetKeyDown(KeyCode.Alpha9))
            number = 9;

        if (number > 0 && number <= 9)
            unitNumber.text = number.ToString();

        if (Input.GetKeyDown(KeyCode.Alpha0))
            number = 0;
        if (number == 0)
            unitNumber.text = "";



    }
}

