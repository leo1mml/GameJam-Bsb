using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody2D rb;
    private float horizontalDirection;
    private float verticalDirection;
    public float velocity;


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalDirection = Input.GetAxis("Horizontal");
        verticalDirection = Input.GetAxis("Vertical");
        rb.AddForce(new Vector2(horizontalDirection * velocity, verticalDirection * velocity));


        if (Mathf.Approximately(horizontalDirection, 0.0f))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (Mathf.Approximately(verticalDirection, 0.0f))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }

    }
}
