using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLines : MonoBehaviour {

    public LayerMask unitsLayerMask;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Draw();
        }
	}

    void Draw()
    {
        List<GameObject> allUnits = new List<GameObject>();
        allUnits.AddRange(GameObject.FindGameObjectsWithTag("Units"));

        GameObject currentColor = gameObject.GetComponent<SpawnSystem>().objectToSpawn;
        Color lineColor = currentColor.GetComponent<SpriteRenderer>().color;

        foreach (GameObject unitA in allUnits)
        {
            //Color lineColor = new Color (Random.value, Random.value, Random.value);
            
            foreach (GameObject unitB in allUnits)
            {
                //if (unitA.name != unitB.name)
                if (unitA.name != unitB.name && unitA.GetComponent<SpriteRenderer>().color == currentColor.GetComponent<SpriteRenderer>().color)
                {
                    Debug.DrawRay(unitA.transform.position, (unitB.transform.position - unitA.transform.position), lineColor, 1);
                    RaycastHit2D[] hit = Physics2D.LinecastAll(unitA.transform.position, unitB.transform.position);

                    if (hit[1])// && hit.collider.tag == "Units")
                    {
                        Debug.Log(hit[1].collider.gameObject.name);
                    }
                    if (false)
                    {
                        Debug.Log("Destroyed");
                        Destroy(unitB);
                    }
                }
            }
        }
        
    }
}
