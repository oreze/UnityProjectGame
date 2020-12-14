using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;

   void Start()
    {
        rb.velocity = transform.right * speed;
        Destroy(rb, 0.5f);
    }
}
