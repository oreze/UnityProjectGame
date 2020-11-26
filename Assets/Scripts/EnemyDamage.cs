using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage: MonoBehaviour
{
    public int health = 150;

    //public GameObject deathEffect;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
