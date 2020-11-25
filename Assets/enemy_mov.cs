using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_mov : MonoBehaviour {

	public float speed;
	private bool MoveRight = false;
	private bool hasPath;
	public Transform GroundCheck;
	public float checkRadius;
	public LayerMask whatIsGround;

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
	}

}