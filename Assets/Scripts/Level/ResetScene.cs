using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerExit2D(Collider2D collision){
        if (collision.tag.Equals("Player")){
            SceneManager.LoadScene("Mapa1");
        }
    }

}
