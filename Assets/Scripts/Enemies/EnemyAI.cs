using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyAI : MonoBehaviour
{
    public string Name;
    public Rigidbody2D RigidBody;
    public Animator PlayerAnimator;
    public PlayerController Player;
    public Transform GroundCheck;
    public LayerMask WhatIsGround;
    private Transform Target;

    public float CheckRadius;
    public float Speed;
    private bool HasPath;
    private float DistanceFromPlayer;

    [Range(0, 3f)] public float TrackingRange;
    [Range(0, 10f)] public float TimeBetweenAttacks;
    [Range(1, 2f)] public float AttackRange;
    private bool BreakBetweenAttacks;
    private bool IsAttacking;
    private bool CanMove;

    private int NumberOfAttacks;
    private Dictionary<string, float> Clips;

    void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        IsAttacking = false;
        BreakBetweenAttacks = false;
        CanMove = true;
        NumberOfAttacks = 3;
        
        Clips = new Dictionary<string, float>();
        (string Key, float Value) Tuple;
        for(int i = 0; i < NumberOfAttacks; i++)                        
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

        HasPath = Physics2D.OverlapCircle(GroundCheck.position, CheckRadius, WhatIsGround);
        DistanceFromPlayer = Vector3.Distance(transform.position, Player.transform.position);

        if (HasPath && DistanceFromPlayer < TrackingRange)
        {
            AnimatorTransitionInfo info = PlayerAnimator.GetAnimatorTransitionInfo(0);
            if (!BreakBetweenAttacks)
            {
                if (DistanceFromPlayer <= AttackRange)
                    AttackHandler();
                if (!IsAttacking && CanMove)
                    transform.position = Vector2.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
            }
            else if (CanMove)
                transform.position = Vector2.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
        }

        MovementAnimationHandler();
    }


    private void AttackHandler()
    {
        IsAttacking = true;
        BreakBetweenAttacks = true;
        CanMove = false;

        int AttackID = Random.Range(1, 4);
        transform.GetComponent<EnemyDamage>().previousAttackId = AttackID;

        switch (AttackID)
        {
            case 1:
                PlayerAnimator.SetTrigger("doAttack1");
                break;
            case 2:
                PlayerAnimator.SetTrigger("doAttack2");
                break;
            case 3:
                PlayerAnimator.SetTrigger("doAttack3");
                break;
        }

        StartCoroutine(SetAttackToFalse(Clips["SpiritBoxerAttack" + AttackID], TimeBetweenAttacks));
    }

    private IEnumerator SetAttackToFalse(float time, float extraTime)
    {
        yield return new WaitForSeconds(time);
        IsAttacking = false;
        CanMove = true;
        yield return new WaitForSeconds(extraTime);
        BreakBetweenAttacks = false;
    }

    private void MovementAnimationHandler()
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
            RigidBody.AddForce(Vector2.up * 100f);
        else if (collision.CompareTag("Player"))
            CanMove = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
            CanMove = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
            CanMove = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
            CanMove = true;
    }

    //-------------------------------------------------------------------------------------------//
    //------------------------------------------ UTILS ------------------------------------------//
    //-------------------------------------------------------------------------------------------//
    private (string Key, float Value) GetClipLength(string name)
    {
        AnimationClip Clip = PlayerAnimator.runtimeAnimatorController.animationClips.Single(el => el.name.Equals(name));
        return (Clip.name, Clip.length);
    }
}
