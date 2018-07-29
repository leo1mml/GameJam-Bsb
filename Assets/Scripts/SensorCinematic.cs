using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SensorCinematic : MonoBehaviour {

    public PlayableDirector playableDirector;
    public Canvas barraFome;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        playableDirector.Play();
        barraFome.enabled = false;
        if (collision.tag.Equals("Player")) {
            collision.gameObject.GetComponent<PlayerMovement>().isDead = true;
            StartCoroutine(Parar(collision.gameObject));
        }
        AudioManager.instance.Pause("Footstep");
    }

    IEnumerator Parar(GameObject gameObject){
        yield return new WaitForSeconds(3f);
        gameObject.GetComponent<Animator>().enabled = false;
    }


}
