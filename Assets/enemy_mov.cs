using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_mov : MonoBehaviour {

	public int maxHealth;
	public int currentHealth;
	public float speed;
	private bool MoveRight = false;
	private bool hasPath;
	public Transform GroundCheck;
	public float checkRadius;
	public LayerMask whatIsGround;

	void Start()
    {
		currentHealth = maxHealth;
    }

	// Use this for initialization
	void Update () {

		hasPath = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, whatIsGround);
		if (!hasPath) MoveRight = !MoveRight;
		if (MoveRight) {
			transform.Translate(2* Time.deltaTime * speed, 0,0);
			transform.localScale= new Vector2 (1,1);
 		}
		else{
			transform.Translate(-2* Time.deltaTime * speed, 0,0);
			transform.localScale= new Vector2 (-1,1);
		}
	}

	void OnTriggerEnter2D(Collider2D trig)
	{
		if (trig.gameObject.CompareTag("turn")){

			if (MoveRight){
				MoveRight = false;
			}
			else{
				MoveRight = true;
			}	
		}
		else if (trig.gameObject.CompareTag("Projectile"))
        {
			TakeDamage(20);
        }
	}

	void TakeDamage(int damage)
    {
		currentHealth -= damage;
		if (currentHealth <= 0)
			Destroy(gameObject);
    }

}