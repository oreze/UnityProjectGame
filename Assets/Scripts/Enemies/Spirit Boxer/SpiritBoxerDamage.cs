using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritBoxerDamage : EnemyDamage
{
    
    void Start()
    {
        Health = MaxHealth;
        RigidBody = GetComponent<Rigidbody2D>();
        if (Healthbar) Healthbar.setHealth(Health, MaxHealth);
	    SoundToPlay = GetComponent<AudioSource>();
        Score = GameObject.FindObjectOfType<Score>();

    }

/*    public (int AttackID, int Damage) MakeDamage()
    {
        return (PreviousAttackID, Random.Range(AttackDamage[PreviousAttackID].x, AttackDamage[PreviousAttackID].y));

        *//*SpiritBoxerAI AI = GetComponent<SpiritBoxerAI>();

        if (PreviousAttackID == 1)
            return (1, Random.Range(Attack1Damage.x, Attack1Damage.y));
        else if (PreviousAttackID == 2)
            return (2, Random.Range(Attack2Damage.x, Attack2Damage.y));
        else
            return (3, Random.Range(Attack3Damage.x, Attack3Damage.y));*//*
    }*/
}
