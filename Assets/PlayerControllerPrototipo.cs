using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerPrototipo : MonoBehaviour {

    private Rigidbody2D rb;
    private bool isJumping = false;
    public int multiplyer = 10;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        if(!Input.GetButtonDown("Jump")){
            rb.AddForce(new Vector2(Input.GetAxis("Horizontal"), 0f) * multiplyer);
        }else{
            rb.AddForce(new Vector2(Input.GetAxis("Horizontal"), 20f) * multiplyer);
            isJumping = true;
        }
	}

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.tag.Equals("Gelo")){
            multiplyer = 15;
            if(collision.gameObject.GetComponent<SpriteRenderer>().color.Equals(new Color(0f,0f,1f))){
                Destroy(collision.gameObject);
                isJumping = true;
            }else{
                collision.gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 1f);
            }

            if(isJumping){
                Destroy(collision.gameObject);
            }
        }

        if(collision.collider.tag.Equals("Pedra")){
            multiplyer = 10;
            isJumping = false;
        }
    }
}
