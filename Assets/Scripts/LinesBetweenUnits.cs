using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinesBetweenUnits : MonoBehaviour
{

    void Start()
    {
        
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
        //connector.positionCount = (allUnits.Count - 1) * 2;


        foreach (GameObject unit in allUnits)
        {
            RaycastHit2D[] hits;
            hits = Physics2D.RaycastAll(transform.position, unit.transform.position - transform.position, 1000.0f);
            bool unithit = false;
            //Debug.Log(hits.Length);
            for (int i = 0; i < hits.Length; i++)
            {

                RaycastHit2D hit = hits[i];
                if (hit.transform.name == "Wall(Clone)")
                //if (hit.transform.tag == "Tiles")
                {
                    unithit = false;
                    break;
                }
                if (hit.transform.tag == "Units" && hit.transform.gameObject != transform.gameObject)// && hit.transform.gameObject != transform.gameObject)
                {
                    unithit = true;
                    break;
                }
            }


            if (unithit == true)
            {
                connector.positionCount++;
                connector.SetPosition((connector.positionCount - 1), gameObject.transform.position);
                connector.positionCount++;
                connector.SetPosition((connector.positionCount - 1), unit.transform.position);
            }
        }  
    }



   
}
