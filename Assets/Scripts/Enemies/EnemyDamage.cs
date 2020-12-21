using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int maxHealth = 150;
    private int health;
    [Range(0, 200)] public int damage;
    public ParticleSystem bloodSplash;
    private Rigidbody2D rb;
    //public GameObject deathEffect;
    public HealthbarEnemy healthbar;
    private PolygonCollider2D attackCollider;
    public AudioSource SoundToPlay;
    
    void Start()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        if (healthbar) healthbar.setHealth(health, maxHealth);
	SoundToPlay = GetComponent<AudioSource>();
    }
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        Instantiate(bloodSplash, new Vector2(rb.position.x, rb.position.y - 0.1f), Quaternion.identity);
        if (healthbar) healthbar.setHealth(health, maxHealth);
        if (health <= 0)
        {
	    //Destroy(); 
	    SoundToPlay.Play();
	    transform.Translate(0, -100, Time.deltaTime);
            Invoke("Die",0.8f);
        }
    }

    
    void Die()
    {
	Destroy(gameObject);
    }
    
}
