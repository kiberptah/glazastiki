using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;



public class SpawnSystem : MonoBehaviour
{
    /*
    public GameObject Grid;

    private int xSize;
    private int ySize;

    ///general
    public GameObject objectToSpawn;
    ///walls
    public GameObject[] walls = new GameObject[5];
    ///units
    public GameObject[] units = new GameObject[2];
    //height map
    //public GameObject[] heights = new GameObject[10];
    public GameObject heights;
    private int minHeight;
    private int maxHeight;

    ///drag
    public GameObject draggedObject;


    public string chosenSpawnMode; //для рейкастинга зрения?

    public Text SpawnModeText;
    public Text spawnObjectText;


    public float xCursor;
    public float yCursor;

    // для выбора объекта из массива,  objectSelect();
    int i = 0;
    int j = 0;

    void Start()
    {
        xSize = Grid.GetComponent<GridGeneration>().xSize;
        ySize = Grid.GetComponent<GridGeneration>().ySize;


        minHeight = -5;
        maxHeight = 5;
    }

    // Update is called once per frame
    void Update()
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
        // Спавн штук
        if (chosenSpawnMode == "Walls" || chosenSpawnMode == "Units" || chosenSpawnMode == "Height")
        {
            ///Сначала удаляем уже стоящие на этом тайле стены, как при нажатие ЛКМ так и ПКМ
            if (Input.GetButton("RMB") 
                || (Input.GetButton("LMB") && (chosenSpawnMode == "Walls" || chosenSpawnMode == "Height"))                
                || (Input.GetButtonDown("LMB") && (chosenSpawnMode == "Units"))             
                )
            {
                RaycastHit2D hit = Physics2D.Raycast(new Vector3(xCursor, yCursor, 1), Vector3.forward, 0, 1 << LayerMask.NameToLayer(chosenSpawnMode));
                if (hit)
                {
                    if (hit.collider.gameObject.tag == chosenSpawnMode)
                    {
                        Destroy(hit.collider.gameObject);
                    }
                }
            }
            ///Потом ставим новую
            if ( (Input.GetButton("LMB") && (chosenSpawnMode == "Walls" || chosenSpawnMode == "Height")) 
                || (Input.GetButtonDown("LMB") && (chosenSpawnMode == "Units"))
                )
            {
                if ((xCursor < xSize && xCursor >= 0) && (yCursor < ySize && yCursor >= 0))
                {
                    if (chosenSpawnMode == "Height") // ебанатсво для карты высот
                    {
                        objectToSpawn.GetComponent<getNumberHeight>().number = j;
                        objectToSpawn.GetComponentInChildren<Canvas>().gameObject.GetComponentInChildren<Text>().text = objectToSpawn.GetComponent<getNumberHeight>().number.ToString();
                    }

                    GameObject newObject = Instantiate(objectToSpawn, new Vector3(xCursor, yCursor, 1), objectToSpawn.transform.rotation);
                    newObject.tag = chosenSpawnMode;                
                }
                
            }
        }


        // Режим перетаскивания
        if (chosenSpawnMode == "Drag")
        {
            if (Input.GetButtonDown("LMB"))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, 0);
                if (hit && (hit.transform.tag == "Units" || hit.transform.tag == "Corpses"))
                {
                    draggedObject = hit.collider.gameObject;
                }
            }
            if (Input.GetButton("LMB") && draggedObject != null)
            {
                if ((xCursor < xSize && xCursor >= 0) && (yCursor < ySize && yCursor >= 0))
                {
                    draggedObject.transform.position = new Vector3(xCursor, yCursor, 1); // Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    draggedObject.transform.position = new Vector3(draggedObject.transform.position.x, draggedObject.transform.position.y, 1);
                }
            }
            if (Input.GetButtonUp("LMB"))
            {
                draggedObject = null;
            }
        }

        // Режим киллера 
        if (chosenSpawnMode == "Kill")
        {
            if (Input.GetButtonDown("LMB"))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, 0, 1 << LayerMask.NameToLayer("Units")); //чел на форуме написал что нужен битшифт для лейермаска без него не работает почему то =\
                //убиваем
                if (hit && hit.transform.tag == "Units")
                {
                    hit.transform.tag = "Corpses";


                    //Хули так сложно, надо упростить наверное
                    hit.transform.gameObject.GetComponent<SpriteRenderer>().color = new Color(hit.transform.gameObject.GetComponent<SpriteRenderer>().color.r,
                                                                                                hit.transform.gameObject.GetComponent<SpriteRenderer>().color.g,
                                                                                                hit.transform.gameObject.GetComponent<SpriteRenderer>().color.b,
                                                                                                0.3f);
                    Color c = hit.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().color;
                    c = new Color(c.r, c.g, c.b, 0.3f);
                    hit.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().color = c;
                }
                else
                {
                    //Воскрешаем
                    if (hit && hit.transform.tag == "Corpses")
                    {
                        hit.transform.tag = "Units";
                        hit.transform.gameObject.GetComponent<SpriteRenderer>().color = new Color(hit.transform.gameObject.GetComponent<SpriteRenderer>().color.r,
                                                                                                    hit.transform.gameObject.GetComponent<SpriteRenderer>().color.g,
                                                                                                    hit.transform.gameObject.GetComponent<SpriteRenderer>().color.b,
                                                                                                    1f);
                        Color c = hit.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().color;
                        c = new Color(c.r, c.g, c.b, 1f);
                        hit.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().color = c;
                    }
                }
                
            }
        }

    }

    private void objectSelect()
    {
        

        //Выбор spawnmode
        if (Input.GetKeyDown(KeyCode.Q))
        {
            i = 0;
            j = 0;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {   
            if(i == 0) // говнокод для удобства использования при пеерключении режимов
            {
                j = 0;
            }
            i = 1;
            //j = 0;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            i = 2;
            //j = 0;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            i = 3;
            j = 0;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            i = 4;
        }

        // Выбор объекта через прокрутку, работает нормально
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            ++j;
            if ((i == 0 && (j > walls.GetLength(0) - 1 || walls[j] == null))
             || (i == 1 && (j > units.GetLength(0) - 1 || units[j] == null))
             || (i == 3 && (j > maxHeight))) // проверка на выход за пределы массива или на пустой элемент
            {
                --j;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            --j;
            if ((i == 0 && (j < 0 || walls[j] == null))
             || (i == 1 && (j < 0 || units[j] == null))
             || (i == 3 && (j < minHeight)))
            {
                ++j;
            }
        }
        // Возврат на первый объект колесиком
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            j = 0;
        }

        //execution
        if (i == 0)
        {
            objectToSpawn = walls[j];
            chosenSpawnMode = "Walls";

            SpawnModeText.text = "Mode: Enviroment";
            spawnObjectText.text = "Object: " + objectToSpawn.name;
        }
        if (i == 1)
        {
            objectToSpawn = units[j];
            chosenSpawnMode = "Units";

            SpawnModeText.text = "Mode: Units";
            spawnObjectText.text = "Object: " + objectToSpawn.name;
        }
        if (i == 2)
        {
            chosenSpawnMode = "Drag";

            SpawnModeText.text = "Mode: Drag";
            spawnObjectText.text = "";
        }
        if (i == 3)
        {
            objectToSpawn = heights;
            chosenSpawnMode = "Height";

            SpawnModeText.text = "Mode: Height Mapping";
            spawnObjectText.text = "Height: " + j;  
        }
        if (i == 4)
        {
            chosenSpawnMode = "Kill";

            SpawnModeText.text = "Mode: KILLING";
            spawnObjectText.text = "";
        }

    }

     */

}



  