﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NewSpawnSystem : MonoBehaviour
{
    public GameObject Grid;
    GameObject GameSystems;

    private int xSize;
    private int ySize;

    public float xCursor;
    public float yCursor;

    public GameObject spawnMenu;

    public GameObject[] tiles = new GameObject[2];
    public GameObject[] units = new GameObject[2];
    public GameObject[] misc = new GameObject[2];
    public GameObject[] effects = new GameObject[2];

    GameObject objectToSpawn = null;
    string spawnMode = "Tiles";

    void Start()
    {
        GameSystems = GameObject.Find("GameSystems");

        xSize = Grid.GetComponent<GridGeneration>().xSize;
        ySize = Grid.GetComponent<GridGeneration>().ySize;

    }

    // Update is called once per frame
    void Update()
    {
        mouseTracking();

        if (Input.GetButtonDown("E"))
        {
            spawnMenu.active = !spawnMenu.active;           
            GameSystems.GetComponent<GameStatus>().isGamePaused = !GameSystems.GetComponent<GameStatus>().isGamePaused;
        }

        Spawner();

       
    }

    private void mouseTracking()
    {
        ///Переводим экранные координаты мыши в мировые
        Vector3 mouseposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        xCursor = mouseposition.x;
        yCursor = mouseposition.y;

        ///Синхронизируем float координаты мышки с целочисленными координатами на сетке
        xCursor = Mathf.Round(mouseposition.x);
        yCursor = Mathf.Round(mouseposition.y);
    }

    


    public void SpawnObject(int number, string mode)
    {
        spawnMode = mode;
        switch (spawnMode)
        {
            case "Tiles":
                objectToSpawn = tiles[number];
                break;
            case "Units":
                objectToSpawn = units[number];
                break;
            case "Misc":
                objectToSpawn = misc[number];
                break;
            case "FX":
                objectToSpawn = effects[number];
                break;
            default:
                objectToSpawn = tiles[number];
                break;

        }
        
    }
    

    void Spawner()
    {
        if (GameSystems.GetComponent<GameStatus>().isGamePaused == false)
        {
            if (Input.GetButton("LMB") && spawnMode != null)
            {
                RaycastHit2D hit = Physics2D.Raycast(new Vector3(xCursor, yCursor, 1), Vector3.forward, 0, 1 << LayerMask.NameToLayer(spawnMode));
                if (!hit && objectToSpawn != null)
                {
                    if ((xCursor < xSize && xCursor >= 0) && (yCursor < ySize && yCursor >= 0))
                    {
                        GameObject newObject = Instantiate(objectToSpawn, new Vector3(xCursor, yCursor, 1), Quaternion.identity);
                        Debug.Log(spawnMode);
                        newObject.tag = spawnMode;
                        
                    }
                }
            }
            if (Input.GetButton("RMB"))
            {
                RaycastHit2D hit = Physics2D.Raycast(new Vector3(xCursor, yCursor, 1), Vector3.forward, 0, 1 << LayerMask.NameToLayer(spawnMode));
                if (hit)
                {
                    Destroy(hit.collider.gameObject);
                }
            }
            if (Input.GetButton("SCROLLWHEEL"))
            {
                Debug.Log("object to spawn reset");
                objectToSpawn = null;
            }
        }
        
    }

    
}
