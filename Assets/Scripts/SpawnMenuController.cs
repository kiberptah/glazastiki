using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpawnMenuController : MonoBehaviour
{
    public GameObject selectTiles;
    public GameObject selectUnits;
    public GameObject selectMisc;
    public GameObject selectEffects;

    public GameObject row;
    public GameObject cell;

    private GameObject[] tiles;
    private GameObject[] units;
    private GameObject[] misc;
    private GameObject[] effects;

    private GameObject GameSystems;
    
    public string selectedPage;
    void Start()
    {
        selectedPage = "Tiles";
        //
        GameSystems = GameObject.Find("GameSystems");
        tiles = GameSystems.GetComponent<NewSpawnSystem>().tiles;
        units = GameSystems.GetComponent<NewSpawnSystem>().units;
        misc = GameSystems.GetComponent<NewSpawnSystem>().misc;
        effects = GameSystems.GetComponent<NewSpawnSystem>().effects;
        //
        

        OrganiseRows();
    }

    void Update()
    {
        
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
        /// Misc
        for (int i = 0; i < Mathf.Ceil(misc.Length / 6f); ++i)
        {
            GameObject newRow = Instantiate(row, row.transform.position, Quaternion.identity, selectMisc.transform.GetChild(0).transform);

            for (int j = 0; j < 6; j++) // в каждой строке 6 слотов задано графически 1 дочерний по умолчанию
            {

                GameObject newCell = Instantiate(cell, cell.transform.position, Quaternion.identity, newRow.transform);
                newCell.GetComponent<ObjectCellData>().cellNumber = i + j;

                if (j < misc.Length)
                {
                    newCell.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = misc[j + 6 * i].GetComponent<SpriteRenderer>().sprite;
                    newCell.gameObject.transform.GetChild(0).GetComponent<Image>().color = misc[j + 6 * i].GetComponent<SpriteRenderer>().color;
                }
            }
        }

        /// FX
        for (int i = 0; i < Mathf.Ceil(effects.Length / 6f); ++i)
        {
            GameObject newRow = Instantiate(row, row.transform.position, Quaternion.identity, selectEffects.transform.GetChild(0).transform);

            for (int j = 0; j < 6; j++) // в каждой строке 6 слотов задано графически 1 дочерний по умолчанию
            {

                GameObject newCell = Instantiate(cell, cell.transform.position, Quaternion.identity, newRow.transform);
                newCell.GetComponent<ObjectCellData>().cellNumber = i + j;

                if (j < effects.Length)
                {
                    newCell.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = effects[j + 6 * i].GetComponent<SpriteRenderer>().sprite;
                    newCell.gameObject.transform.GetChild(0).GetComponent<Image>().color = effects[j + 6 * i].GetComponent<SpriteRenderer>().color;
                }
            }
        }
    }

    public void SelectPage(string page, GameObject Thumbnail)
    {
        selectedPage = page;

        selectTiles.SetActive(false);
        selectUnits.SetActive(false);
        selectMisc.SetActive(false);
        selectEffects.SetActive(false);

        switch (page)
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
