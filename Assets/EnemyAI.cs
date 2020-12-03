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
    


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
       
        if (transform.hasChanged)
        {
            animator.SetBool("isMoving", true);
            transform.hasChanged = false;
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
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
    }
}
