using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float maxSpeed;
    public float jumpForce;

    [HideInInspector]
    public bool isJumping = false;
    private bool isGrounded;
    [HideInInspector]
    public bool isDead = false;

    private Rigidbody2D rgb2d;
    private SpriteRenderer sprite;
    private Animator animator;

    private int countParado = 0;
    private int countAndando = 0;
    private float qtdFome = 100;
    private float lenthFome;

    public BoxCollider2D groundCheck;
    public RectTransform barraFome;

    public LayerMask groundLayer;
    public GameObject dash;

    private void Awake()
    {
        rgb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        lenthFome = barraFome.sizeDelta.x;
    }

    // Use this for initialization
    void Start()
    {
        lenthFome = barraFome.sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead){
            rgb2d.velocity = Vector2.zero;
            return;
        }
        isGrounded = Physics2D.OverlapBox(groundCheck.transform.position, groundCheck.size, groundCheck.transform.rotation.z, groundLayer);

        animator.SetBool("isGround", isGrounded);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Object.Instantiate(dash, new Vector3(rgb2d.position.x, rgb2d.position.y, 0f), Quaternion.identity);
            rgb2d.AddForce(new Vector2(0f, jumpForce));
            isJumping = true;
            maxSpeed = 5;
        }

        if(countParado >= 60){
            qtdFome -= 2;
            barraFome.sizeDelta = new Vector2((qtdFome / 100) * lenthFome, barraFome.sizeDelta.y);
            countParado = -1;
        }

        if(countAndando >= 60){
            qtdFome -= 4;
            barraFome.sizeDelta = new Vector2((qtdFome / 100) * lenthFome, barraFome.sizeDelta.y);
            countAndando = -1;
        }

        if(qtdFome <= 0){
            isDead = true;
            PlayerDeath();
        }
    }

    private void FixedUpdate()
    {
        if (isDead) {
            rgb2d.velocity = Vector2.zero;
            return;
        }
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

        if (move.Equals(0) || isJumping) {
            AudioManager.instance.Pause("Footstep");
            countParado++;
        } else if (rgb2d.velocity.y.Equals(0) && !move.Equals(0)) {
            AudioManager.instance.Play("Footstep");
            countAndando++;
        }
            

        rgb2d.velocity = new Vector2(move * maxSpeed, rgb2d.velocity.y);

        if (move > 0f && sprite.flipX || move < 0f && !sprite.flipX)
        {
            sprite.flipX = !sprite.flipX;
        }
    }

    void PlayerDeath() {
        AudioManager.instance.Pause("Footstep");
        animator.SetTrigger("isDead");
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag.Equals("Gelo")) {
            maxSpeed = 10;
            GeloController geloController = collision.gameObject.GetComponent<GeloController>();

            if (isJumping) {
                //geloController.BrokenIce();
               // maxSpeed = 5;
                isJumping = false;
            }
        }

        if (collision.collider.tag.Equals("Pedra")) {
            maxSpeed = 5;
            isJumping = false;
        }

        if (collision.collider.tag.Equals("Coletavel")){
            if (qtdFome <= 50)
                qtdFome += 50;
            else
                qtdFome = 100;
            barraFome.sizeDelta = new Vector2((qtdFome / 100) * lenthFome, barraFome.sizeDelta.y);
            Destroy(collision.gameObject);
        }

    }

}
