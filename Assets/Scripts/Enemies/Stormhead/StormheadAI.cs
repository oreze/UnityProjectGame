using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormheadAI : EnemyAI
{
    // Start is called before the first frame update
    void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        IsAttacking = false;
        BreakBetweenAttacks = false;
        CanMove = true;
        NumberOfAttacks = 1;

        Clips = new Dictionary<string, float>();
        (string Key, float Value) Tuple;
        for (int i = 0; i < NumberOfAttacks; i++)
        {
            Tuple = GetClipLength(Name + "Attack" + (i + 1));
            Clips.Add(Tuple.Key, Tuple.Value);
        }
    }

    void Update()
    {
        //------------------------------------------------------------------------------------------//
        //-----------------------------   ATTACK AND MOVEMENT LOGIC   ------------------------------//
        //------------------------------------------------------------------------------------------//
        if (!Target)
        {
            Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }

        HasPath = Physics2D.OverlapCircle(GroundCheck.position, CheckRadius, WhatIsGround);
        DistanceFromPlayer = Vector3.Distance(transform.position, Target.transform.position);

        if (HasPath && DistanceFromPlayer < TrackingRange)
        {
            AnimatorTransitionInfo info = PlayerAnimator.GetAnimatorTransitionInfo(0);
            if (!BreakBetweenAttacks)
            {
                if (DistanceFromPlayer <= AttackRange)
                    AttackHandler();
                if (!IsAttacking && CanMove)
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(Target.position.x, Target.position.y + 0.5f), Speed * Time.deltaTime);
            }
            else if (CanMove && DistanceFromPlayer > 1.07f)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(Target.position.x, Target.position.y + 0.5f), Speed * Time.deltaTime);
            }
        }

        MovementAnimationHandler();
    }
}
