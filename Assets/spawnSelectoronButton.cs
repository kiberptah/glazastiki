using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class spawnSelectoronButton : MonoBehaviour
{
    GameObject GameSystems;
    public GameObject Cell;
    
    void Start()
    {
        GameSystems = GameObject.Find("GameSystems");

        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(SelectObjectToSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void SelectObjectToSpawn()
    {
        GameSystems.GetComponent<NewSpawnSystem>().SpawnObject(Cell.GetComponent<ObjectCellData>().cellNumber);
    }

}
