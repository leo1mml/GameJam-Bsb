﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float maxSpeed;
    public float jumpForce;

    [HideInInspector]
    public bool isJumping = false;
    private bool isGrounded;

    private Rigidbody2D rgb2d;
    private SpriteRenderer sprite;
    private Animator animator;

    public PhysicsMaterial2D geloMaterial;
    public PhysicsMaterial2D playerMaterial;
    public BoxCollider2D groundCheck;

    public LayerMask groundLayer;
    public GameObject dash;

    private void Awake()
    {
        rgb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.transform.position, groundCheck.size, groundCheck.transform.rotation.z, groundLayer);

        animator.SetBool("isGround", isGrounded);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Object.Instantiate(dash, new Vector3(rgb2d.position.x, rgb2d.position.y, 0f), Quaternion.identity);
            rgb2d.AddForce(new Vector2(0f, jumpForce));
            isJumping = true;
            maxSpeed = 5;
        }
    }

    private void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");

        if(rgb2d.velocity.Equals(Vector2.zero)){
            animator.SetBool("isWalking", false);
            animator.SetBool("isGround", true);
            animator.SetBool("isFalling", false);
        }

        if ((rgb2d.velocity.x > 0 || rgb2d.velocity.x < 0) && rgb2d.velocity.y.Equals(0)) {
            animator.SetBool("isWalking", true);
            animator.SetBool("isGround", true);
            animator.SetBool("isFalling", false);
        }

        if (rgb2d.velocity.y > 0) {
            animator.SetBool("isGround", false);
        }

        if (rgb2d.velocity.y < 0) {
            animator.SetBool("isFalling", true);
        }

        if (move.Equals(0))
            AudioManager.instance.Pause("Footstep");
        else if(rgb2d.velocity.y.Equals(0) && !move.Equals(0))
            AudioManager.instance.Play("Footstep");
            

        rgb2d.velocity = new Vector2(move * maxSpeed, rgb2d.velocity.y);
        animator.SetFloat("Velocity", Mathf.Abs(rgb2d.velocity.x));

        if (move > 0f && sprite.flipX || move < 0f && !sprite.flipX)
        {
            sprite.flipX = !sprite.flipX;
        }
    }

    void PlayerDeath() {
        animator.SetTrigger("isDead");
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag.Equals("Gelo")) {
            rgb2d.sharedMaterial = geloMaterial;
            maxSpeed = 15;
            GeloController geloController = collision.gameObject.GetComponent<GeloController>();

            if (isJumping) {
                geloController.BrokenIce();
                maxSpeed = 5;
            }
        }

        if (collision.collider.tag.Equals("Pedra")) {
            rgb2d.sharedMaterial = playerMaterial;
            maxSpeed = 10;
            isJumping = false;
        }
    }

}
