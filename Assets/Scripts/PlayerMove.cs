using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public string groundTag = "Ground";


    private Rigidbody2D rb;
    private bool isGrounded = false;
    // i need to know wheres my character
    private bool isFacingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float moveDirection = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);

        // change the side direction of my character. That works with 0 and 1 for left and right 

        if (moveDirection > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (moveDirection < 0 && isFacingRight)
        {
            Flip();
        }

       


    }

    void Update()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(groundTag))
        {
            isGrounded = true;
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(groundTag))
        {
            isGrounded = false;
        }
    }
    // the Flip function just change the side of my sprite
    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);

    }
}
