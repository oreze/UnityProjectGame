using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    private float moveInput;
    public float jumpForce;
    public Animator animator;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    public Vector2 respawn;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    private bool m_FacingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(moveInput));
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if (moveInput > 0 && !m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (moveInput < 0 && m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }

        if (gameObject.transform.position.y < -50f)
            gameObject.transform.position = respawn;


    }
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        if(isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }
        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false; 
            }

        }
        
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
        if (isGrounded)
        {
            animator.SetBool("isGrounded", true);
        }
        else
        {
            animator.SetBool("isGrounded", false);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.U))
            TakeDamage(20);
        

    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        transform.Rotate(0f, 180f, 0f);
    }

    void Attack()
    {
        animator.SetTrigger("Shoot");
        
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
