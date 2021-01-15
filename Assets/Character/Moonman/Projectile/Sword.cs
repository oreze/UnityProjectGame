using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [Range(0f, 10f)] [SerializeField]public float speed;
    public Rigidbody2D rb;
    public Animator animator;
    public int damage;
    [Range(0.1f, 5f)] public float DestroyTime;

  

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void Start()
    {
	
        rb.velocity = transform.right * speed;
        Destroy(gameObject, DestroyTime);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        IEnemyDamage enemyDmg = null;

        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("COLLISION NAME " + collision.name + " " + collision.gameObject.name);

            /* if (collision.name.Equals("Spirit Boxer"))
                 enemyDmg = collision.GetComponent<IEnemyDamage>();

             if (enemyDmg != null)
             {
                 enemyDmg.TakeDamage(damage);
             }*/
            enemyDmg = collision.gameObject.GetComponent<IEnemyDamage>();
            enemyDmg.TakeDamage(damage);
            if (!collision.name.Equals("Player") && !collision.tag.Equals("Projectile"))
            {
                rb.velocity = transform.right;
                animator.SetBool("isDestroyed", true);
                Destroy(gameObject, 0.15f);
            }
        }
    }


}
