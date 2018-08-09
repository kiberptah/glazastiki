using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using System;


public class getNumberHeight : MonoBehaviour {

    public int number;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //number = Int32.Parse(gameObject.transform.GetChild(0).GetComponentInChildren<Text>().text);
        gameObject.transform.GetChild(0).GetComponentInChildren<Text>().text = number.ToString();
    }
}
