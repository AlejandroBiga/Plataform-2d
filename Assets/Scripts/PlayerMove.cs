using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //basics
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public string groundTag = "Ground";

    //physics and conditions
    private Rigidbody2D rb;
    private bool isGrounded = false;
    private bool isFacingRight = true;
    private bool isTouchingWall = false;
    private Animator animator;

    //test of particular system (?
    [SerializeField] private ParticleSystem particles;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float moveDirection = Input.GetAxis("Horizontal");

        //set animations
        animator.SetFloat("Horizontal", Mathf.Abs(moveDirection));
        
        animator.SetBool("OnGround", isGrounded);
        
        animator.SetBool("TouchingWall", isTouchingWall);

        // Move the player horizontally
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);

        // Flip the player's direction if needed
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
        // Check for jump input when grounded or touching a wall
        if ((isGrounded || isTouchingWall) && Input.GetKeyDown(KeyCode.Space))
        {
            
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            particles.Play();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.gameObject.CompareTag(groundTag))
        {
            isGrounded = true;
        }
        
        else if (other.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        
        if (other.gameObject.CompareTag(groundTag))
        {
            isGrounded = false;
        }
        
        else if (other.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = false;
        }
    }

    // Flip the player's sprite horizontally
    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
        particles.Play();
    }
}