using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes: MonoBehaviour
{
     public PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

    }
    void OnTriggerEnter2D(Collider2D col)
    {
	if(col.CompareTag("Player"))
	{
		player.TakeDamage(150);
	}
    }	
    // Update is called once per frame
    void Update()
    {
        
    }
}
