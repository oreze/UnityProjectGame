using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class EnemyAI : MonoBehaviour, IEnemyAI
{
    public string Name;
    public Rigidbody2D RigidBody;
    public Animator PlayerAnimator;
    //public PlayerController Player;
    public Transform GroundCheck;
    public LayerMask WhatIsGround;
    protected Transform Target;

    public float CheckRadius;
    public float Speed;
    protected bool HasPath;
    protected float DistanceFromPlayer;

    [Range(0, 3f)] public float TrackingRange;
    [Range(0, 10f)] public float TimeBetweenAttacks;
    [Range(1, 2f)] public float AttackRange;
    protected bool BreakBetweenAttacks;
    protected bool IsAttacking;
    protected bool CanMove;

    protected int NumberOfAttacks;
    protected Dictionary<string, float> Clips;

    //-----------------------------------------------------------------------------------------//
    //------------------------------- ATTACK AND MOVEMENT LOGIC -------------------------------//
    //-----------------------------------------------------------------------------------------//

    protected virtual void AttackHandler()
    {
        IsAttacking = true;
        BreakBetweenAttacks = true;
        CanMove = false;

        int AttackID = UnityEngine.Random.Range(NumberOfAttacks != 0 ? 1 : 0, NumberOfAttacks);
        transform.GetComponent<EnemyDamage>().PreviousAttackID = AttackID;

        PlayerAnimator.SetTrigger("doAttack" + AttackID);
        Debug.Log(Name + " KLURWA");
        StartCoroutine(SetAttackToFalse(Clips[Name + "Attack" + AttackID], TimeBetweenAttacks));
    }

    protected IEnumerator SetAttackToFalse(float time, float extraTime)
    {
        yield return new WaitForSeconds(time);
        IsAttacking = false;
        CanMove = true;
        yield return new WaitForSeconds(extraTime);
        BreakBetweenAttacks = false;
    }

    protected virtual void MovementAnimationHandler()
    {
        if (transform.hasChanged)
        {
            PlayerAnimator.SetBool("isMoving", true);
            transform.hasChanged = false;
        }
        else
            PlayerAnimator.SetBool("isMoving", false);

        if (transform.position.x < Target.position.x)
            transform.localScale = new Vector2(-1, 1);
        else
            transform.localScale = new Vector2(1, 1);
    }

    //------------------------------------------------------------------------------------------//
    //--------------------------------------- COLLISIONS ---------------------------------------//
    //------------------------------------------------------------------------------------------//

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
            RigidBody.AddForce(Vector2.up * 120f);
        else if (collision.CompareTag("Player"))
            CanMove = false;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
            CanMove = false;
    }

    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
            CanMove = false;
    }

    protected virtual void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
            CanMove = true;
    }

    //-------------------------------------------------------------------------------------------//
    //------------------------------------------ UTILS ------------------------------------------//
    //-------------------------------------------------------------------------------------------//

    protected (string Key, float Value) GetClipLength(string name)
    {
        try
        {
            Debug.Log(name);
            AnimationClip Clip = PlayerAnimator.runtimeAnimatorController.animationClips.Single(el => el.name.Equals(name));
            return (Clip.name, Clip.length);
        }
        catch
        {
            return (null, 0);
        }
    }
}
