using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D RigidBody;
    public float Speed;
    private float MoveInput;
    public float JumpForce;
    public Animator Animator;

    public AudioSource mySource;
    public int rangeScan;
    public int rangeScan2;
    public int IndexDeathSound;
    public AudioClip[] myAudio;
    public int toPlay;
    
    private bool IsGrounded;
    public Transform FeetPosition;
    public float CheckRadius;
    public LayerMask WhatIsGround;
    public Vector2 Respawn;

    private float JumpTimeCounter;
    public float JumpTime;
    private bool IsJumping;

    public readonly int MaxHealth = 100;
    public int CurrentHealth;
    public HealthBar HealthBar;
    public ParticleSystem BloodSplash;

    public Transform FirePoint;
    public Sword CurrentWeapon;
    [Range(0.01f, 10f)] public float AttackSpeed;
    private float TimeBetweenAttacks;
    private bool CanShoot;

    private bool FacingRight = true;

    void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        CurrentHealth = MaxHealth;
        HealthBar.SetMaxHealth(MaxHealth);
        CanShoot = true;
        TimeBetweenAttacks = 1.0f / AttackSpeed;
    }

    void FixedUpdate()
    {
        MoveInput = Input.GetAxis("Horizontal");
        Animator.SetFloat("Speed", Mathf.Abs(MoveInput));
        RigidBody.velocity = new Vector2(MoveInput * Speed, RigidBody.velocity.y);

        if (MoveInput > 0 && !FacingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (MoveInput < 0 && FacingRight)
        {
            // ... flip the player.
            Flip();
        }

        if (gameObject.transform.position.y < -50f)
            gameObject.transform.position = Respawn;


    }
    void Update()
    {
        TimeBetweenAttacks = 1.0f / AttackSpeed;
        IsGrounded = Physics2D.OverlapCircle(FeetPosition.position, CheckRadius, WhatIsGround);
        if (IsGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            IsJumping = true;
            JumpTimeCounter = JumpTime;
            RigidBody.velocity = Vector2.up * JumpForce;
        }
        if (Input.GetKey(KeyCode.Space) && IsJumping == true)
        {
            if (JumpTimeCounter > 0)
            {
                RigidBody.velocity = Vector2.up * JumpForce;
                JumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                IsJumping = false;
            }

        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            IsJumping = false;
        }
        if (IsGrounded)
        {
            Animator.SetBool("IsGrounded", true);
        }
        else
        {
            Animator.SetBool("IsGrounded", false);
        }
        if (Input.GetKey(KeyCode.Return) && CanShoot)
        {
            Attack();
	        toPlay = Random.Range(rangeScan+1,rangeScan2);
            mySource.PlayOneShot(myAudio[toPlay], 0.9F);
            mySource.Play();
            CanShoot = false;
            StartCoroutine(ShootDelay(TimeBetweenAttacks));
        }


    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        FacingRight = !FacingRight;

        // Multiply the player's x local scale by -1.
        transform.Rotate(0f, 180f, 0f);
    }

    void Attack()
    {
        Animator.SetTrigger("Shoot");
        Instantiate(CurrentWeapon, FirePoint.position, FirePoint.rotation);

    }

    public void TakeDamage(int damage)
    {
	    CurrentHealth -= damage;
            HealthBar.SetHealth(CurrentHealth);
	    if(CurrentHealth > 0){
	       toPlay = Random.Range(0,rangeScan);
           mySource.PlayOneShot(myAudio[toPlay], 0.9F);
           mySource.Play();
	    }
	    else {
	       mySource.PlayOneShot(myAudio[IndexDeathSound], 0.9F);
           mySource.Play();
	   //Die();
	}

        Instantiate(BloodSplash, new Vector3(RigidBody.position.x, RigidBody.position.y - 0.1f, 0), Quaternion.identity);
    }

    public void CheckForSpecialAttacks(string name, Transform collision)
    {
        if (name.Equals("SpiritBoxer1"))
        {
            RigidBody.AddForce(new Vector2(500f * (transform.position.x < collision.transform.position.x ? -1 : 1), 150f));
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hitbox"))
        {
            IEnemyDamage script = collision.transform.parent.parent.GetComponent<IEnemyDamage>();     //TODO make interface for AI and damage scripts;
            (int AttackID, int Damage) Tuple = script.MakeDamage();
            TakeDamage(Tuple.Damage);
            CheckForSpecialAttacks(script.GetGameObject().name.Replace(" ", "") + Tuple.AttackID, collision.transform);
        }
    }

    public IEnumerator ShootDelay(float time)
    {
        yield return new WaitForSeconds(time);
        CanShoot = true;
    }

    public Vector3 CurrentPosition()
    {
        return transform.position;
    }
}

