using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeloController : MonoBehaviour {

    public Sprite geloSprite;

    private SpriteRenderer spriteRenderer;
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public int qtdColision = 0;

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.tag.Equals("Player")){
            if(qtdColision == 0){
                RemoveSnow();
                qtdColision++;
            }else if (qtdColision == 1){
                BrokenIce();
                collision.gameObject.GetComponent<PlayerMovement>().isJumping = true;
            }
        }
    }

    public void RemoveSnow(){
        animator.SetBool("isRemovingSnow", true);
    }

    public void BrokenIce() {
        GetComponent<BoxCollider2D>().enabled = false;
        animator.SetBool("isBroken", true);
        AudioManager.instance.Play("IceCracking");
    }

    public void DestroyTile(){
        Destroy(gameObject);
    }
}
