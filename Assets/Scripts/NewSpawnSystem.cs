using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NewSpawnSystem : MonoBehaviour
{
    public GameObject Grid;

    private int xSize;
    private int ySize;

    public float xCursor;
    public float yCursor;

    public GameObject spawnMenu;
    public GameObject row;
    public GameObject cell;

    public GameObject selectTiles;
    public GameObject selectUnits;
    public GameObject selectMisc;
    public GameObject selectEffects;

    public GameObject[] tiles = new GameObject[2];
    public GameObject[] units = new GameObject[2];
    public GameObject[] misc = new GameObject[2];
    public GameObject[] effects = new GameObject[2];

    GameObject objectToSpawn = null;
    string spawnMode = "Tiles";

    void Start()
    {
        xSize = Grid.GetComponent<GridGeneration>().xSize;
        ySize = Grid.GetComponent<GridGeneration>().ySize;

        OrganiseRows();
    }

    // Update is called once per frame
    void Update()
    {
        mouseTracking();

        if (Input.GetButtonDown("E"))
        {
            spawnMenu.active = !spawnMenu.active;
        }

        //SelectPage();
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

    void OrganiseRows()
    {
        /// Tiles     
        for (int i = 0; i < Mathf.Ceil(tiles.Length / 6f); ++i)
        {
            GameObject newRow = Instantiate(row, row.transform.position, Quaternion.identity, selectTiles.transform.GetChild(0).transform);

            for (int j = 0; j < 6; j++) // в каждой строке 6 слотов задано графически 1 дочерний по умолчанию
            {

                GameObject newCell = Instantiate(cell, cell.transform.position, Quaternion.identity, newRow.transform);
                newCell.GetComponent<ObjectCellData>().cellNumber = i + j;

                if (j < tiles.Length)
                {
                    newCell.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = tiles[j + 6 * i].GetComponent<SpriteRenderer>().sprite;
                    newCell.gameObject.transform.GetChild(0).GetComponent<Image>().color = tiles[j + 6 * i].GetComponent<SpriteRenderer>().color;
                }
            }
        }
        /// Units
        for (int i = 0; i < Mathf.Ceil(units.Length / 6f); ++i)
        {
            GameObject newRow = Instantiate(row, row.transform.position, Quaternion.identity, selectUnits.transform.GetChild(0).transform);

            for (int j = 0; j < 6; j++) // в каждой строке 6 слотов задано графически 1 дочерний по умолчанию
            {

                GameObject newCell = Instantiate(cell, cell.transform.position, Quaternion.identity, newRow.transform);
                newCell.GetComponent<ObjectCellData>().cellNumber = i + j;

                if (j < units.Length)
                {
                    newCell.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = units[j + 6 * i].GetComponent<SpriteRenderer>().sprite;
                    newCell.gameObject.transform.GetChild(0).GetComponent<Image>().color = units[j + 6 * i].GetComponent<SpriteRenderer>().color;
                }
            }
        }
    }


    public void SpawnObject(int number)
    {
        switch(spawnMode)
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
        if (!spawnMenu.activeSelf)
        {
            if (Input.GetButton("LMB") && spawnMode != null)
            {
                RaycastHit2D hit = Physics2D.Raycast(new Vector3(xCursor, yCursor, 1), Vector3.forward, 0, 1 << LayerMask.NameToLayer(spawnMode));
                if (!hit && objectToSpawn != null)
                {
                    if ((xCursor < xSize && xCursor >= 0) && (yCursor < ySize && yCursor >= 0))
                    {
                        GameObject newObject = Instantiate(objectToSpawn, new Vector3(xCursor, yCursor, 1), Quaternion.identity);
                        newObject.tag = spawnMode;
                    }
                }
            }
            if (Input.GetButton("RMB"))
            {
                RaycastHit2D hit = Physics2D.Raycast(new Vector3(xCursor, yCursor, 1), Vector3.forward, 0, 1 << LayerMask.NameToLayer(spawnMode), 1 << LayerMask.NameToLayer(spawnMode));
                if (hit)
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }

    public void SelectPage(string page, GameObject Thumbnail)
    {
        spawnMode = page;

        selectTiles.SetActive(false);
        selectUnits.SetActive(false);
        selectMisc.SetActive(false);
        selectEffects.SetActive(false);

        switch(spawnMode)
        {
            case "Tiles":
                selectTiles.SetActive(true);
                break;
            case "Units":
                selectUnits.SetActive(true);
                break;
            case "Misc":
                selectMisc.SetActive(true);
                break;
            case "FX":
                selectEffects.SetActive(true);
                break;
            default:
                selectTiles.SetActive(true);
                break;
        }   

    }
}
