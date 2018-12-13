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
        //Debug.Log("Save");
        //savePath = Application.dataPath + "/saves/";

        //и стенки и юниты, всё это объекты...
        objectForSave = new List<GameObject>();
        objectForSave.AddRange(GameObject.FindGameObjectsWithTag("Walls"));
        objectForSave.AddRange(GameObject.FindGameObjectsWithTag("Units"));
        objectForSave.AddRange(GameObject.FindGameObjectsWithTag("Corpses"));
        objectForSave.AddRange(GameObject.FindGameObjectsWithTag("Height"));


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
            objectToDelete.AddRange(GameObject.FindGameObjectsWithTag("Corpses"));
            objectToDelete.AddRange(GameObject.FindGameObjectsWithTag("Height"));

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

                GameObject objectToSpawn = GameObject.Find(element.type);
                //Debug.Log(element.type);
                GameObject newObject
                    = Instantiate(objectToSpawn,
                            new Vector3(element.coordinates[0], element.coordinates[1], 1), Quaternion.identity);
                newObject.tag = element.tag;
                if (newObject.tag == "Units" || newObject.tag == "Corpses")
                {
                    newObject.GetComponent<numeration>().number = element.number;
                    newObject.GetComponent<changeType>().unitType = element.unitType;                 
                }
                if (newObject.tag == "Height")
                {
                    newObject.GetComponent<getNumberHeight>().number = element.number;
                    Debug.Log(element.number);
                    //Debug.Log(newObject.GetComponent<numeration>().number);
                }
                if (newObject.tag == "Corpses")
                {
                    newObject.transform.gameObject.GetComponent<SpriteRenderer>().color = new Color(newObject.transform.gameObject.GetComponent<SpriteRenderer>().color.r,
                                                                                                newObject.transform.gameObject.GetComponent<SpriteRenderer>().color.g,
                                                                                                newObject.transform.gameObject.GetComponent<SpriteRenderer>().color.b,
                                                                                                0.3f);
                    Color c = newObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().color;
                    c = new Color(c.r, c.g, c.b, 0.3f);
                    newObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().color = c;
                }
                    //Debug.Log("LOADED");
                }
            
        }
    }
}

[Serializable]
public class ObjectData
{
    public float[] coordinates;
    public string type;
    public string tag;

    public int number; // для юнитов и высот
    public int unitType; // для юнитов

    public ObjectData(GameObject obj)
    {
        type = obj.name.Remove(obj.name.Length - 7); // нужно удалить окончание "(Clone)" это 7 символов.
        tag = obj.tag;

        coordinates = new float[3];
        coordinates[0] = obj.transform.position.x;
        coordinates[1] = obj.transform.position.y;

        coordinates[2] = obj.transform.rotation.z;

        if (obj.tag == "Units" || obj.tag == "Corpses")
        {
            number = obj.GetComponent<numeration>().number;
            unitType = obj.GetComponent<changeType>().unitType;
        }

        if (obj.tag == "Height")
        {
            number = obj.GetComponent<getNumberHeight>().number;
        }
    }   
}
