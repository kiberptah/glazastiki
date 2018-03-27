using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class PauseSystem : MonoBehaviour {

    public Transform canvas;

    public Component spawnSystem;

    GameObject button;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //isPaused = !isPaused;
            PauseGame();
        }
	}

    public void PauseGame()
    {      
        canvas.gameObject.SetActive(!canvas.gameObject.activeInHierarchy);
        gameObject.GetComponent<SpawnSystem>().enabled = !gameObject.GetComponent<SpawnSystem>().enabled;
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ShowButton(GameObject button)
    {
        button.SetActive(!button.activeInHierarchy);
    }



}
