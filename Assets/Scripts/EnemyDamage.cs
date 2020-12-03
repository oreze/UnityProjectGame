using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage: MonoBehaviour
{
    public int health = 150;
    public ParticleSystem bloodSplash;
    private Rigidbody2D rb;
    //public GameObject deathEffect;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Instantiate(bloodSplash, new Vector2(rb.position.x, rb.position.y - 0.1f), Quaternion.identity);
        if (health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
