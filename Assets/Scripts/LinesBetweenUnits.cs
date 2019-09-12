using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;

public class LinesBetweenUnits : MonoBehaviour
{
    Camera MainCamera;
    GameObject GameSystems;
    public Text distance;
    void Start()
    {
        MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        GameSystems = GameObject.Find("GameSystems");
    }

    // Update is called once per frame
    void Update()
    {
        Draw();
    }

    void Draw()
    {
        List<GameObject> allUnits = new List<GameObject>();
        allUnits.AddRange(GameObject.FindGameObjectsWithTag("Units"));

        LineRenderer connector;
        connector = gameObject.GetComponent<LineRenderer>();
        connector.positionCount = 0;
        connector.startColor = GameSystems.GetComponent<ColorSystem>().unitVisionLinesColor;
        connector.endColor = GameSystems.GetComponent<ColorSystem>().unitVisionLinesColor;

        //connector.positionCount = (allUnits.Count - 1) * 2;


        foreach (GameObject unit in allUnits)
        {
            RaycastHit2D[] hits;
            hits = Physics2D.RaycastAll(transform.position, unit.transform.position - transform.position, 1000.0f);
            bool unithit = false;

            for (int i = 0; i < hits.Length; i++)
            {
                unithit = false;

                RaycastHit2D hit = hits[i];
                if (hit.transform.name == "Wall(Clone)")
                {
                    unithit = false;
                    break;
                }
                if (hit.transform.tag == "Units" 
                    && hit.transform.gameObject != transform.gameObject
                    && hit.transform.GetComponent<UnitData>().team != gameObject.GetComponent<UnitData>().team
                    && hit.transform.gameObject == unit)
                {
                    unithit = true;
                    break;
                }
            }

            ClearOldDistances(gameObject, unit);
            if (unithit == true)
            {
                connector.positionCount++;
                connector.SetPosition((connector.positionCount - 1), gameObject.transform.position);
                connector.positionCount++;
                connector.SetPosition((connector.positionCount - 1), unit.transform.position);

                CalculateDistance(gameObject, unit);
            }
        }  
    }

    void CalculateDistance(GameObject U1, GameObject U2)
    {
        Vector3 U1Coord = U1.transform.position;
        Vector3 U2Coord = U2.transform.position;
        //Вычисляем координаты центра между юнитами.
        Vector3 centerBetweenUnits;
        centerBetweenUnits.x = (U1Coord.x + U2Coord.x) * 0.5f;
        centerBetweenUnits.y = (U1Coord.y + U2Coord.y) * 0.5f;
        centerBetweenUnits.z = 0;
        Debug.Log(centerBetweenUnits);
        ///нужно виртуально сдвинуть один из юнитов ближе к другому 
        ///т.к. нам нужно не расстояние между центрами считть а количество клеток между ними
        if (U1Coord.x > U2Coord.x)
        {
            U1Coord.x--;
        }
        if (U1Coord.x < U2Coord.x)
        {
            U2Coord.x--;
        }
        if(U1Coord.y > U2Coord.y)
        {
            U1Coord.y--;
        }
        if (U1Coord.y < U2Coord.y)
        {
            U2Coord.y--;
        }

        //вычисляем длину вектора
        double dist = Math.Sqrt(Math.Pow((U1Coord.x - U2Coord.x), 2) + Math.Pow((U1Coord.y - U2Coord.y), 2));

        //округляем до 10х
        dist = dist * 10;
        dist = Math.Round(dist);
        dist = dist * 0.1;

        
        //Спавним новую дистацнию только если нет старой
                Text newDistance;
                newDistance = Instantiate(distance, centerBetweenUnits, Quaternion.identity, transform.GetChild(0));
                newDistance.text = dist.ToString();
                newDistance.tag = "Distance";
                newDistance.GetComponent<DistanceManager>().Unit1 = U1;
                newDistance.GetComponent<DistanceManager>().Unit2 = U2;


    }

    void ClearOldDistances(GameObject Unt1, GameObject Unt2)
    {
        List<GameObject> allDistances = new List<GameObject>();
        allDistances.AddRange(GameObject.FindGameObjectsWithTag("Distance"));

        foreach (GameObject d in allDistances)
        {
            if (d.GetComponent<DistanceManager>().Unit1 == Unt1 && d.GetComponent<DistanceManager>().Unit2 == Unt2)
            {
                Destroy(d);
            }
        }

    }




}
