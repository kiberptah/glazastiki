using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class SaveButton : MonoBehaviour {

    public GameObject SaveMenu;

    public InputField saveName;

	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    public void Go(bool isOk)
    {
        Debug.Log("Go");
        string savePath = SaveSystem.savePath;

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || isOk == true)
        {
            //Создаём папку сохранений если её ещё нет
            if (File.Exists(savePath) == false)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(savePath));
            }
            //Если имея сохранения не введено, то оно стадартное  
            if (saveName.text == "")
            {
                int files_amount = 1;
                DirectoryInfo di = new DirectoryInfo(savePath);
                foreach (var fi in di.GetFiles())
                {
                    ++files_amount;
                }
                saveName.text = "Save" + files_amount;
            }

            SaveSystem.saveName = saveName.text;
            Debug.Log(savePath);
            SaveSystem.Save();
        }
    }

    public void ResetSaveButton()
    {
        if (saveName.isFocused == false)
        {
            saveName.text = "";
            //saveName.gameObject.SetActive(false);
        }
    }

    public void openSaveMenu()
    {
        SaveMenu.SetActive(true);
    }

    public void closeSaveMenu()
    {
        SaveMenu.SetActive(false);
    }
}
