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

    public void ResetSaveButton()
    {
        if (input.isFocused == false)
        {
            Debug.Log("AAAARR");
            input.text = "";
            input.gameObject.SetActive(false);
        }
    }
}
