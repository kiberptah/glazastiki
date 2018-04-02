using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class LoadButton : MonoBehaviour {

    static int position = 0;


    public Text loadName;

    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    public void Go()
    {
        string savePath = SaveSystem.savePath;

        //if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SaveSystem.Load();
        }
    }
    public void PositionPrev()
    {
        --position;
        Debug.Log(position);
    }
    public void PositionNext()
    {
        ++position;
        Debug.Log(position);
    }
    public void changeLoadName()
    {
        List<string> loadFiles = new List<string>();
        DirectoryInfo di = new DirectoryInfo(SaveSystem.savePath);

        foreach (FileInfo file in di.GetFiles())
        {
            loadFiles.Add(file.Name);
        }

        if (position >= loadFiles.Count)
        {
            position = 0;
            Debug.Log("Changed to " + position);
        }
        if (position < 0)
        {
            position = loadFiles.Count - 1;
            Debug.Log("Changed to " + position);
        }

        loadName.text = loadFiles[position];
    }
}
