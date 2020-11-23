using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [Range(0f, 10f)] [SerializeField]public float speed;
    public Rigidbody2D rb;

    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.name.Equals("Player"))
            Destroy(gameObject, 0.05f);
    }
}
