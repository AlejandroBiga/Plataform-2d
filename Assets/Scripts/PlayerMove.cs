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
    private bool isWallJumping = false;
    private Vector2 wallJumpDirection;
    private Animator animator;

    //test of particular system (?
    [SerializeField] private ParticleSystem particles;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        LoadPlayerPosition();
    }
    void LoadPlayerPosition()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            Vector3 position = new Vector3(data.position[0], data.position[1], data.position[2]);
            transform.position = position;
            Debug.Log("Player position set to: " + position);
        }
        else
        {
            Debug.LogError("No save data found.");
        }
    }
    void FixedUpdate()
    {
        float moveDirection = Input.GetAxis("Horizontal");

        //set animations
        animator.SetFloat("Horizontal", Mathf.Abs(moveDirection));

        animator.SetBool("OnGround", isGrounded);

        animator.SetBool("TouchingWall", isTouchingWall);

        // Move the player horizontally
        if (!isWallJumping)
        {
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
    }

    void Update()
    {
        // Check for jump input when grounded or touching a wall
        if ((isGrounded || isTouchingWall) && Input.GetKeyDown(KeyCode.Space))
        {
            if (isTouchingWall)
            {
                WallJump();
            }
            else
            {
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                particles.Play();
            }
        }
    }

    void WallJump()
    {
        
        Vector2 jumpDirection;

        if (isTouchingWall)
        {
            
            float angleInRadians = Mathf.PI / 4; 
            jumpDirection = new Vector2(Mathf.Cos(angleInRadians) * -wallJumpDirection.x, Mathf.Sin(angleInRadians)).normalized;

            
            if (jumpDirection.x > 0 && !isFacingRight)
            {
                Flip();
            }
            else if (jumpDirection.x < 0 && isFacingRight)
            {
                Flip();
            }
        }
        else
        {
            
            float angleInRadians = Mathf.PI / 4; 
            jumpDirection = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians)).normalized;
        }

        
        rb.velocity = jumpDirection * Mathf.Sqrt(2 * jumpForce * jumpForce); // Mantener la velocidad
        particles.Play();
        isWallJumping = true;
        StartCoroutine(DisableWallJump());
    }

    IEnumerator DisableWallJump()
    {
        yield return new WaitForSeconds(0.1f);  // Adjust the time based on your needs
        isWallJumping = false;
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
            wallJumpDirection = other.contacts[0].normal * -1;  // Store the wall direction
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

    public void DisableControles()
    {
        enabled = false;
    }

    public void EnableControles()
    {
        enabled = true;
    }
}