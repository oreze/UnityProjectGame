using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [Range(0f, 10f)] [SerializeField]public float speed;
    public Rigidbody2D rb;
    public Animator animator;
    public int damage = 30;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        rb.velocity = transform.right * speed;
        Destroy(gameObject, 2f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Stormhead stormhead = collision.GetComponent<Stormhead>();
        if(stormhead != null)
        {
            stormhead.TakeDamage(damage); 
        }
        if (!collision.name.Equals("Player")) {
            rb.velocity = transform.right;
            //Destroy(rb);
            animator.SetBool("isDestroyed", true);
            Destroy(gameObject, 0.2f);
        }
    }
}
