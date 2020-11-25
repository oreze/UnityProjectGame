using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [Range(0f, 10f)] [SerializeField]public float speed;
    public Rigidbody2D rb;
    public int damage = 30;

    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Stormhead stormhead = collision.GetComponent<Stormhead>();
        if(stormhead != null)
        {
            stormhead.TakeDamage(damage); 
        }
        if (!collision.name.Equals("Player"))
            Destroy(gameObject, 0.05f);
    }
}
