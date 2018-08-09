using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class ScreenShotMaker : MonoBehaviour {

    DateTime localDate;
    public string time;

    public GameObject UIelements;
   
    //public Camera screenShotCamera;

    //public float aspect;
    //public int height;
    //public int width;

    void Start ()
    {
        //screenShotCamera.enabled = false;

        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(CaptureScreen());           
        }

        //height = Screen.height;
        //width = Screen.width;

        //aspect = height / width;
        //screenShotCamera.rect = new Rect((1 - aspect) / 2, 0, aspect, 1);
    }

    public IEnumerator CaptureScreen()
    {
        localDate = DateTime.Now;
        time = localDate.ToString() + ".png";
        time = time.Replace('/', '.').Replace(':', '-');

        // Wait till the last possible moment before screen rendering to hide the UI
        yield return null;

        UIelements.SetActive(false);

        // Wait for screen rendering to complete
        yield return new WaitForEndOfFrame();
        
        // Take screenshot
        ScreenCapture.CaptureScreenshot(time, 1);

        // Wait for screen rendering to complete
        yield return new WaitForEndOfFrame();

        // Show UI after we're done
        UIelements.SetActive(true);        
    }



}


/*

if (Input.GetKeyDown(KeyCode.Space))
        {          
            localDate = DateTime.Now;
            time = localDate.ToString() + ".png";
            time = time.Replace('/', '.').Replace(':', '-');

            UIelements.SetActive(false);           

            ScreenCapture.CaptureScreenshot(time, 2);
            UIelements.SetActive(true);
        }

*/

