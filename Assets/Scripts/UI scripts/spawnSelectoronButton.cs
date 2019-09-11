using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class spawnSelectoronButton : MonoBehaviour
{
    GameObject GameSystems;
    GameObject SpawnUI;

    public GameObject Cell;
    
    void Start()
    {
        GameSystems = GameObject.Find("GameSystems");
        SpawnUI = GameObject.Find("SpawnUI");

        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(SelectObjectToSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void SelectObjectToSpawn()
    {

        GameSystems.GetComponent<NewSpawnSystem>().SpawnObject(Cell.GetComponent<ObjectCellData>().cellNumber, SpawnUI.GetComponent<SpawnMenuController>().selectedPage);
        //Debug.Log(SpawnUI.GetComponent<SpawnMenuController>().selectedPage);
    }

}
