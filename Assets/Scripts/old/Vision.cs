using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour {
    /*
    public GameObject gameSystems;

    void Start ()
    {
        if (transform.position.y != -100) // на y = -100 лежат префабы
        {
            gameObject.GetComponent<LineRenderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (transform.position.y != -100) // на y = -100 лежат префабы
        {           
            DrawLines();                 
        }
    }

    void DrawLines()
    {
        List<GameObject> allUnits = new List<GameObject>();

        LineRenderer connector;      
        connector = GetComponent<LineRenderer>();

        if (gameSystems.GetComponent<SpawnSystem>().chosenSpawnMode == "Units")
        {
            connector.enabled = (gameObject.GetComponent<SpriteRenderer>().color == gameSystems.GetComponent<SpawnSystem>().objectToSpawn.GetComponent<SpriteRenderer>().color)
                && gameSystems.GetComponent<DrawLines>().ShowVisionLines; //если цвета ок и режим включен то показываем
        }

        connector.positionCount = 0;

        connector.startColor = connector.endColor = gameObject.GetComponent<SpriteRenderer>().color;


        allUnits.AddRange(GameObject.FindGameObjectsWithTag("Units"));

        //connector.positionCount = allUnits.Count + 1;
        
        Vector3[] unitCoords = new Vector3[allUnits.Count];


        connector.positionCount++;
        connector.SetPosition(0, transform.position);
        int i = 1;
        foreach (GameObject unit in allUnits)
        {
            if (unit.transform.GetComponent<SpriteRenderer>().color != gameObject.GetComponent<SpriteRenderer>().color)
            {
                connector.positionCount++;
                connector.SetPosition(i, unit.transform.position);
                //Debug.Log("posiition " + i + " = " + connector.GetPosition(i));
                ++i;

                connector.positionCount++;
                connector.SetPosition(i, transform.position);
                //Debug.Log("RETURN: position " + i + " = " + connector.GetPosition(i));
                ++i;
            }
        }    
        
        /*
        int i = 0;
        foreach (GameObject unit in allUnits)
        {
            if (unit.transform.GetComponent<SpriteRenderer>().color != currentColor)
            {
                unitCoords[i] = unit.transform.position;
                //Debug.Log(unitCoords[i]);

                ++i;
           }        
        }

        connector.positionCount++;
        connector.SetPosition(0, transform.position);
        for (int a = 1; a <= allUnits.Count; a += 2)
        {
            connector.positionCount++;
            connector.SetPosition(a, unitCoords[a-1]);
            Debug.Log("posiition " + a + " = " + connector.GetPosition(a));

            connector.positionCount++;
            connector.SetPosition((a + 1), transform.position);
            Debug.Log("RETURN: position " + (a + 1) + " = " + connector.GetPosition(a+1));
        }
        */
    
}
