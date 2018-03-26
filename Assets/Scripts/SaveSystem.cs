using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour {

    List<GameObject> objectForSave = new List<GameObject>();

    public InputField saveName;

    private string savePath;
    void Start ()
    {
        savePath = Application.dataPath + "/saves/";
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
        FileStream stream = new FileStream(savePath + saveName.text + ".glaz", FileMode.Create);

        bf.Serialize(stream, data);
        stream.Close();
    }


    private void Load()
    {
        if (File.Exists(savePath + saveName.text + ".glaz"))
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
            FileStream stream = new FileStream(savePath + saveName.text + ".glaz", FileMode.Open);

            ObjectData[] data = bf.Deserialize(stream) as ObjectData[];
            stream.Close();
            foreach (ObjectData element in data)
            {                               
                SpawnOnLoad(element);
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

    public void SaveButton()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
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

            Save();
        }
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
