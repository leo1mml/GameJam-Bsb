using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReplayController : MonoBehaviour {

    private bool isPaused = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("p"))
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                Time.timeScale = 0f;
                AudioManager.instance.Pause("Theme");
                AudioManager.instance.Pause("Wind");
            } else
            {
                Time.timeScale = 1f;
                AudioManager.instance.Play("Theme");
                AudioManager.instance.Play("Wind");
            }

        }

        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene("Mapa1");
        }
    }
}
