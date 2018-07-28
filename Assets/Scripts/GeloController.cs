using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeloController : MonoBehaviour {

    public Sprite geloSprite;

    private Animation animationQuebrando;
    private SpriteRenderer spriteRenderer;
    private int qtdColision = 0;

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animationQuebrando = GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.tag.Equals("Player")){
            if(qtdColision == 0){
                spriteRenderer.sprite = geloSprite;
                qtdColision++;
            }else if (qtdColision == 1){
                animationQuebrando.Play();
            }
        }
    }
}
