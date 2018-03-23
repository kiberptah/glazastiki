using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class WallPlacer : MonoBehaviour {

    private int arenaSize = 26;

    ///general
    private GameObject objectToSpawn;
    ///walls
    public GameObject[] walls = new GameObject[5];
    ///units
    public GameObject[] units = new GameObject[2];
    ///drag
    public GameObject draggedObject;


    private string chosenSpawnMode;

    public Text SpawnModeText;
    public Text spawnObjectText;
    
    
    public float xCursor;
    public float yCursor;

    // для выбора объекта из массива,  objectSelect();
    int i = 0;
    int j = 0;


    void Start ()
    {
       
    }
	
	// Update is called once per frame
	void Update ()
    {
        mouseTracking();
        objectSelect();
        spawnStuff();            
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

    private void spawnStuff()
    {
        if (chosenSpawnMode == "Walls" || chosenSpawnMode == "Units")
        {
            ///Сначала удаляем уже стоящие на этом тайле стены, как при нажатие ЛКМ так и ПКМ
            if ( (Input.GetButton("LMB") && chosenSpawnMode == "Walls") || (Input.GetButtonDown("LMB") && chosenSpawnMode == "Units") || Input.GetButton("RMB"))
            {
                //RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, 0);
                RaycastHit2D hit = Physics2D.Raycast(new Vector3(xCursor, yCursor, 1), Vector3.forward, 0, 1 << LayerMask.NameToLayer(chosenSpawnMode));
                if (hit)
                {
                    //Debug.Log("hit");
                    if (hit.collider.gameObject.tag == chosenSpawnMode)
                    {
                        Destroy(hit.collider.gameObject);
                    }
                }
            }
            ///Потом ставим новую
            if ( (Input.GetButton("LMB") && chosenSpawnMode == "Walls") || (Input.GetButtonDown("LMB") && chosenSpawnMode == "Units") )
            {
                if( (xCursor <= arenaSize && xCursor > 0) && (yCursor <= arenaSize && yCursor > 0) )
                {
                    Instantiate(objectToSpawn, new Vector3(xCursor, yCursor, 1), Quaternion.identity);
                }                
            }
        }

        
        // Режим перетаскивания
        if(chosenSpawnMode == "Drag")
        {
           
            if (Input.GetButtonDown("LMB"))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, 0);
                if (hit)
                {
                    draggedObject = hit.collider.gameObject;
                }
            }            
            if (Input.GetButton("LMB") && draggedObject!= null)
            {            
                draggedObject.transform.position = new Vector3(xCursor, yCursor, 1); // Camera.main.ScreenToWorldPoint(Input.mousePosition);
                draggedObject.transform.position = new Vector3(draggedObject.transform.position.x, draggedObject.transform.position.y, 1);
            }    
            if (Input.GetButtonUp("LMB"))
            {
                draggedObject = null;
            }  
        }
        
    }
   
    private void objectSelect()
    {
        //execution
        if (i == 0)
        {
            objectToSpawn = walls[j];
            SpawnModeText.text = "Mode: Walls";
            chosenSpawnMode = "Walls";
        }
        if (i == 1)
        {
            objectToSpawn = units[j];
            SpawnModeText.text = "Mode: Units";
            chosenSpawnMode = "Units";
        }
        if (i == 2)
        {
            SpawnModeText.text = "Mode: Drag";
            chosenSpawnMode = "Drag";
        }
        spawnObjectText.text = "Object: " + objectToSpawn.name;

        //Выбор spawnmode
        if (Input.GetKeyDown(KeyCode.Q))
        {
            i = 0;
            j = 0;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            i = 1;
            j = 0;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            i = 2;
            j = 0;
        }

        // Выбор объекта через прокрутку, работает нормально
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            ++j;
            if (( i == 0 && (j > walls.GetLength(0) - 1 || walls[j] == null) ) 
                                                                            || (i == 1 && (j > units.GetLength(0) - 1 || units[j] == null) )) // проверка на выход за пределы массива или на пустой элемент
            {
                --j;
            }   
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            --j;
            if (( i == 0 && (j < 0 || walls[j] == null) )
                                                        || ( i == 1 && (j < 0 || units[j] == null) ))
            {
                ++j;
            }
        }
        // Возврат на первый объект колесиком
        if(Input.GetKeyDown(KeyCode.Mouse2))
        {
            j = 0;
        }
    }
    

}

