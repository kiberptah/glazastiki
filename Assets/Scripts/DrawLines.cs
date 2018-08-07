using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
// Данный скрипт нужен для дебага линий и для хранения глобальной переменной ВКЛ/ВЫКЛ отображение линий взгляда
public class DrawLines : MonoBehaviour {

    public bool ShowVisionLines;
    public Text visionInfo;

    void Start ()
    {
        ShowVisionLines = false;
        visionInfo.text = "";
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ShowVisionLines = !ShowVisionLines;
            //Draw();
        }
        if (ShowVisionLines)
        {
            visionInfo.text = "Vision Lines Enabled";
        }
        else
        {
            visionInfo.text = "";
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
                foreach (GameObject unitB in allUnits)
                {
                    if (unitA.name != unitB.name && unitA.GetComponent<SpriteRenderer>().color == currentColor.GetComponent<SpriteRenderer>().color)
                    {
                        Debug.DrawRay(unitA.transform.position, (unitB.transform.position - unitA.transform.position), lineColor, 1);                         
                    }
                }
            }    
    }
}
