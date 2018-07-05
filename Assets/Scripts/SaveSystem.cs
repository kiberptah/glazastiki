using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour {

    static List<GameObject> objectForSave = new List<GameObject>();

    public static string saveName;
    //public static string loadName;

    public static string savePath;
    void Start ()
    {
        savePath = Application.dataPath + "/saves/";
    }

    // Update is called once per frame
    void Update ()
    {
        //changeLoadName();

        if (Input.GetKeyDown(KeyCode.F5))
        {            
            Save();
        }

        if(Input.GetKeyDown(KeyCode.F9))
        {
            //Load();
        }
	}

    public static void Save()
    {
        Debug.Log("Save");
        //savePath = Application.dataPath + "/saves/";

        //и стенки и юниты, всё это объекты...
        objectForSave = new List<GameObject>();
        objectForSave.AddRange(GameObject.FindGameObjectsWithTag("Walls"));
        objectForSave.AddRange(GameObject.FindGameObjectsWithTag("Units"));



        // с е р и а л и з а ц и я объектов
        int num = 0;
        ObjectData[] data = new ObjectData[objectForSave.Capacity];
        foreach (GameObject element in objectForSave)
        {
            data[num] = new ObjectData(element);           

            ++num;
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(savePath + saveName + ".glaz", FileMode.Create);

        bf.Serialize(stream, data);
        stream.Close();
    }


    public static void Load(string loadName)
    {
        if (File.Exists(savePath + loadName))
        {
            //Очищаем сцену перед загрузкой сохранения
            List<GameObject> objectToDelete = new List<GameObject>();
            objectToDelete.AddRange(GameObject.FindGameObjectsWithTag("Walls"));
            objectToDelete.AddRange(GameObject.FindGameObjectsWithTag("Units"));

            foreach (GameObject o in objectToDelete)
            {
                Destroy(o);
            }

            //Загрузка
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(savePath + loadName, FileMode.Open);

            ObjectData[] data = bf.Deserialize(stream) as ObjectData[];
            stream.Close();
            foreach (ObjectData element in data)
            {
                //Debug.Log("Loading...");
                //SpawnOnLoad(element);
                //GameObject objectToSpawn;

                GameObject objectToSpawn = GameObject.Find(element.type);
                Debug.Log(element.type);
                GameObject newObject
                    = Instantiate(objectToSpawn,
                            new Vector3(element.coordinates[0], element.coordinates[1], 1), Quaternion.identity);
                newObject.tag = element.tag;
                //Debug.Log("LOADED");
            }
            
        }
    }

    /*
    private static void SpawnOnLoad(ObjectData loadedObject)
    {
        GameObject objectToSpawn;

        objectToSpawn = GameObject.Find(loadedObject.type);
        GameObject newObject 
            = Instantiate(objectToSpawn, 
                    new Vector3(loadedObject.coordinates[0], loadedObject.coordinates[1], 1), Quaternion.identity);
        newObject.tag = loadedObject.tag;

        Debug.Log("LOADED");
    }
    */


    /*
    int position = 0;
    public void LoadButton()
    {
        //if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Load();
            Debug.Log("CLICK");
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
        DirectoryInfo di = new DirectoryInfo(savePath);

        foreach (FileInfo file in di.GetFiles())
        {
            loadFiles.Add(file.Name);
        }

        if(position >= loadFiles.Count)
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
    */
}

[Serializable]
public class ObjectData
{
    public float[] coordinates;
    public string type;
    public string tag;

    public ObjectData(GameObject obj)
    {
        type = obj.name.Remove(obj.name.Length - 7); // нужно удалить окончание "(Clone)" это 7 символов.
        tag = obj.tag;

        coordinates = new float[3];
        coordinates[0] = obj.transform.position.x;
        coordinates[1] = obj.transform.position.y;

        coordinates[2] = obj.transform.rotation.z;
    }   
}
