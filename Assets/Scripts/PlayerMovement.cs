using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float maxSpeed;
    public float jumpForce;

    private bool isGrounded;
    private bool isJumping;

    private Rigidbody2D rgb2d;
    private SpriteRenderer sprite;
    private Animator animator;

    public BoxCollider2D groundCheck;

    public LayerMask groundLayer;

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
            isJumping = true;
        }
    }

    private void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");

        rgb2d.velocity = new Vector2(move * maxSpeed, rgb2d.velocity.y);
        animator.SetFloat("Velocity", Mathf.Abs(rgb2d.velocity.x));

        if (move > 0f && sprite.flipX || move < 0f && !sprite.flipX)
        {
            sprite.flipX = !sprite.flipX;
        }

        if (isJumping)
        {
            rgb2d.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }
    }

    void PlayerDeath() {
        animator.SetTrigger("isDead");
    }

}
