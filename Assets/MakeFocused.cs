using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class MakeFocused : MonoBehaviour {

    public InputField input;

    public Component button;

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        input.Select();
    }

    private void OnMouseDown()
    {
        if (input.isFocused == false)
        {
            input.gameObject.SetActive(false);
        }
    }
}
