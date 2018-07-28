using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float maxSpeed;
    public float jumpForce;

    private bool isGrounded;
    [HideInInspector]
    public bool isJumping = false;

    private Rigidbody2D rgb2d;
    private SpriteRenderer sprite;
    private Animator animator;

    public PhysicsMaterial2D geloMaterial;
    public PhysicsMaterial2D playerMaterial;
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
            rgb2d.AddForce(new Vector2(0f, jumpForce));
            isJumping = true;
            maxSpeed = 5;
        }
    }

    private void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");

        if (move.Equals(0))
            AudioManager.instance.Pause("Footstep");
        else
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

            //if (geloController.qtdColision == 1) {
            //    isJumping = true;
            //}

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
