using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class PauseSystem : MonoBehaviour {

    public Transform canvas;

    public Component spawnSystem;

    bool isPaused = false;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            PauseGame(isPaused);
        }
	}

    public void PauseGame(bool isPaused)
    {      
        canvas.gameObject.SetActive(isPaused);
        gameObject.GetComponent<SpawnSystem>().enabled = !isPaused;
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
