using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class LoadButton : MonoBehaviour
{
    public GameObject LoadMenu;

    public ScrollRect scrollview;

    public GameObject loadFilePrefab;
    public GameObject content;

    public void Load(Text loadName)
    {
        SaveSystem.Load(loadName.text);
        Debug.Log(loadName.text);

    }

    public void GetSaveList()
    {
        //List<string> loadFiles = new List<string>();
        DirectoryInfo di = new DirectoryInfo(SaveSystem.savePath);

        foreach (FileInfo file in di.GetFiles())
        {
            //loadFiles.Add(file.Name);

            GameObject s = Instantiate(loadFilePrefab, content.transform.position, Quaternion.identity, content.transform);

            Transform t = s.transform.Find("TitleText");
            t.GetComponent<Text>().text = file.Name;
        }
    }

    public void openLoadMenu()
    {
        LoadMenu.SetActive(true);

        GetSaveList();

        scrollview.verticalNormalizedPosition = 1;
    }

    public void closeLoadMenu()
    {
        LoadMenu.SetActive(false);
    }
}




        

        