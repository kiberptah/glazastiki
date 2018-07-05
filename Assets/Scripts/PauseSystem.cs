using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class PauseSystem : MonoBehaviour {

    public Transform canvasPause;

    public Transform canvasPauseMenu;

    public Transform canvasLoadMenu;

    public Transform canvasSaveMenu;

    public Component spawnSystem;

    //public GameObject ButtonToHide;

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {            
            PauseGame();
            //HideButton(); // пиздец говнокод конешно костыль чтобы прятать одну конкретную кнопку ааа мб реализовать прямо в функции этой кнопки через keyCode escape...
        }
	}

    public void PauseGame()
    {   
        //canvasPause.gameObject.SetActive(!canvasPause.gameObject.activeInHierarchy);
        gameObject.GetComponent<SpawnSystem>().enabled = !gameObject.GetComponent<SpawnSystem>().enabled; //отключение спауна объектов

        if (canvasPause.gameObject.activeSelf == true)
        {
            canvasPause.gameObject.SetActive(false);
            canvasPauseMenu.gameObject.SetActive(false);
            canvasLoadMenu.gameObject.SetActive(false);
            canvasSaveMenu.gameObject.SetActive(false);
        }
        else
        {
            canvasPause.gameObject.SetActive(true);
            canvasPauseMenu.gameObject.SetActive(true);
        }
    }

    public void ShowButton(GameObject button)
    {
        button.SetActive(!button.activeInHierarchy);
    }

    /*
    private void HideButton() //Прячем кнопку загрузки
    {
        ButtonToHide.SetActive(false);
    }
    */

}
