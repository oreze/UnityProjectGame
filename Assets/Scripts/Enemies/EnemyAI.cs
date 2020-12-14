using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed;
    private Transform target;
    public PlayerController player;
    public Animator animator;
    public Rigidbody2D rb;
    private bool hasPath;
    public Transform GroundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        hasPath = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, whatIsGround);
        if (hasPath && Vector3.Distance(transform.position, player.transform.position) < 1.5f)
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        
        if (hasPath)
        {
            if (transform.hasChanged)
            {
                animator.SetBool("isMoving", true);
                transform.hasChanged = false;
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
        }
        else
            animator.SetBool("isMoving", false);

        if(transform.position.x < target.position.x)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            transform.localScale = new Vector2(1, 1);
        }

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            player.TakeDamage(20);
        }
        else if (col.CompareTag("Obstacle")) {
            rb.AddForce(Vector2.up * 100f);
        }
    }
}
