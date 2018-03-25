using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour {

    //private GameObject[] objectForSave;
    List<GameObject> objectForSave = new List<GameObject>();
    public string test;

    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {
		if (Input.GetKeyDown(KeyCode.F5))
        {            
            Save();
        }

        if(Input.GetKeyDown(KeyCode.F9))
        {
            Load();
        }
	}

    private void Save()
    {
        int num = 0;

        //и стенки и юниты, всё это объекты...
        objectForSave = new List<GameObject>();
        objectForSave.AddRange(GameObject.FindGameObjectsWithTag("Walls"));
        objectForSave.AddRange(GameObject.FindGameObjectsWithTag("Units"));

        //Чистим папку сохранения... Как только их будет несколько это надо перенести в менеджер сохранений
        foreach (string file in Directory.GetFiles(Application.persistentDataPath + "/saves/"))
        {
            File.Delete(file);
        }
        // с е р и а л и з а ц и я объектов
        foreach (GameObject element in objectForSave)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/saves/object" + num + ".glaz", FileMode.Create);

            ObjectData data = new ObjectData(element);

            bf.Serialize(stream, data);
            stream.Close();

            ++num;
        }     
        /*
        //Сериализация информации об объектах (количество)
        BinaryFormatter bf2 = new BinaryFormatter();
        FileStream stream2 = new FileStream(Application.persistentDataPath + "/saves/objects_data.save", FileMode.Create);

        ObjectData oadata = new ObjectData(objectForSave.Capacity);

        bf2.Serialize(stream2, oadata);
        stream2.Close();
        */
    }


    private void Load()
    {
        //if (File.Exists(Application.persistentDataPath + "/saves/"))
        {
            List<GameObject> objectToDelete = new List<GameObject>();
            objectToDelete.AddRange(GameObject.FindGameObjectsWithTag("Walls"));
            objectToDelete.AddRange(GameObject.FindGameObjectsWithTag("Units"));

            foreach (GameObject o in objectToDelete)
            {
                Destroy(o);
            }

            int objects_amount = 0;
            foreach (string file in Directory.GetFiles(Application.persistentDataPath + "/saves/"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream stream = new FileStream(Application.persistentDataPath + "/saves/object" + objects_amount + ".glaz", FileMode.Open);

                ObjectData data = bf.Deserialize(stream) as ObjectData;
                stream.Close();

                SpawnOnLoad(data);

                ++objects_amount;
            }
        }
    }

    private void SpawnOnLoad(ObjectData loadedObject)
    {
        GameObject objectToSpawn;

        objectToSpawn = GameObject.Find(loadedObject.type);
        GameObject newObject 
            = Instantiate(objectToSpawn, 
                    new Vector3(loadedObject.coordinates[0], loadedObject.coordinates[1], 1), Quaternion.identity);
        newObject.tag = loadedObject.tag;
    }

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

        coordinates = new float[2];
        coordinates[0] = obj.transform.position.x;
        coordinates[1] = obj.transform.position.y;
    }   
}
