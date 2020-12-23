using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int maxHealth = 150;
    private int health;
    public Vector2Int Attack1Damage;
    public Vector2Int Attack2Damage;
    public Vector2Int Attack3Damage;
    public int previousAttackId { get; set; }
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
        if (healthbar)
            healthbar.setHealth(health, maxHealth);
        if (health <= 0)
        {
	        //Destroy(); 
	        SoundToPlay.Play();
	        transform.Translate(0, -100, Time.deltaTime);
            Invoke("Die",0.8f);
        }
    }

    public (int AttackID, int Damage) MakeDamage()
    {
        EnemyAI AI = GetComponent<EnemyAI>();

        if (previousAttackId == 1)
            return (1, Random.Range(Attack1Damage.x, Attack1Damage.y));
        else if (previousAttackId == 2)
            return (2, Random.Range(Attack2Damage.x, Attack2Damage.y));
        else
            return (3, Random.Range(Attack3Damage.x, Attack3Damage.y));
    }

    
    void Die()
    {
	    Destroy(gameObject);
    }
    
}
