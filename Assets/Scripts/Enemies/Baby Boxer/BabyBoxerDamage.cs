﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyBoxerDamage : EnemyDamage
{
    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
        RigidBody = GetComponent<Rigidbody2D>();
        if (Healthbar) Healthbar.setHealth(Health, MaxHealth);
        SoundToPlay = GetComponent<AudioSource>();
        Score = GameObject.FindObjectOfType<Score>();
    }
}
