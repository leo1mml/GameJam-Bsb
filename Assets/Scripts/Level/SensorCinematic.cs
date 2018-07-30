﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class SensorCinematic : MonoBehaviour {

    public PlayableDirector playableDirector;
    public Canvas barraFome;
    public Sprite playerSprite;

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
            StartCoroutine(Creditos(collision.gameObject));
        }
        AudioManager.instance.Pause("Footstep");
    }

    IEnumerator Parar(GameObject gameObject){
        yield return new WaitForSeconds(3f);
        gameObject.GetComponent<Animator>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = playerSprite;
    }

    IEnumerator Creditos(GameObject gameObject){
        yield return new WaitForSeconds(5f);
        print("oi");
        SceneManager.LoadScene("Credits",LoadSceneMode.Additive);

    }

}
