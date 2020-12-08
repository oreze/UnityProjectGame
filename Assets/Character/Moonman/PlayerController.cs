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
    public ParticleSystem bloodSplash;

    public Transform firePoint;
    public GameObject currentWeapon;
    [Range(0.01f, 10f)]public float attackSpeeed;
    private float timeBetweenAttacks;
    private bool canShoot;

    private bool m_FacingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        canShoot = true;
        timeBetweenAttacks = 1.0f / attackSpeeed;
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
        if (Input.GetKeyDown(KeyCode.Return) && canShoot)
        {
            Attack();
            canShoot = false;
            StartCoroutine(ShootDelay(timeBetweenAttacks));
            Debug.Log(timeBetweenAttacks);
            
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
        Instantiate(currentWeapon, firePoint.position, firePoint.rotation);

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        Instantiate(bloodSplash, new Vector3(rb.position.x, rb.position.y-0.1f, 0), Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.gameObject.name.Contains("Baby Boxer"))
            TakeDamage(20);
    }

    public IEnumerator ShootDelay(float time)
    {
        yield return new WaitForSeconds(time);
        canShoot = true;
    }

}
