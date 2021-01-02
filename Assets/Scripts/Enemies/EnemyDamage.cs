using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour, IEnemyDamage
{
    public int MaxHealth;
    protected int Health;
    public Vector2Int[] AttackDamage;
    [System.NonSerialized] public int PreviousAttackID;
    public ParticleSystem BloodSplash;
    public Rigidbody2D RigidBody;
    //public GameObject deathEffect;
    public HealthbarEnemy Healthbar;
    protected PolygonCollider2D AttackCollider;
    
    public AudioSource SoundToPlay;
    public int rangeScan;  
    public int IndexDeathSound;
    public AudioClip[] myAudio;
    private int toPlay;


    public virtual (int AttackID, int Damage) MakeDamage()
    {
        Debug.Log(AttackDamage.Length);
        Debug.Log("attack id: " + PreviousAttackID + " damage " + AttackDamage[PreviousAttackID - 1].x + " " + AttackDamage[PreviousAttackID - 1].y);
        return (PreviousAttackID, Random.Range(AttackDamage[PreviousAttackID-1].x, AttackDamage[PreviousAttackID-1].y));
    }

    public virtual void TakeDamage(int damage)
    {
        Health -= damage;
        Instantiate(BloodSplash, new Vector2(RigidBody.position.x, RigidBody.position.y - 0.1f), Quaternion.identity);
        if (Healthbar)
            Healthbar.setHealth(Health, MaxHealth);
        if (Health > 0)
	{
	   toPlay = Random.Range(0,rangeScan);
           SoundToPlay.PlayOneShot(myAudio[toPlay], 0.9F);
           SoundToPlay.Play();
	   //toPlay = (toPlay+1)%rangeScan;
	}
        else {
            //Destroy(); 
	   SoundToPlay.PlayOneShot(myAudio[IndexDeathSound], 0.9F);
           SoundToPlay.Play();
           
           transform.Translate(0, -100, Time.deltaTime);
           Invoke("Die", 0.8f);
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }


}
