using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormheadDamage : EnemyDamage
{
    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
        RigidBody = GetComponent<Rigidbody2D>();
        if (Healthbar) Healthbar.setHealth(Health, MaxHealth);
        SoundToPlay = GetComponent<AudioSource>();
        Score = GameObject.FindGameObjectWithTag("Score").GetComponent<Score>();
    }

    public override void TakeDamage(int damage)
    {
        Debug.Log(gameObject.name);
        GetComponent<Animator>().SetTrigger("isDamaged");
        Debug.Log("Health - damage = " + Health + " - " + damage + " = " + (Health - damage));
        Health -= damage;
        Instantiate(BloodSplash, new Vector2(RigidBody.position.x, RigidBody.position.y - 0.1f), Quaternion.identity);
        if (Healthbar)
            Healthbar.setHealth(Health, MaxHealth);
        if (Health > 0)
        {
            Debug.Log(Health + " OVER 0");
            toPlay = Random.Range(0, rangeScan);
            SoundToPlay.PlayOneShot(myAudio[toPlay], 0.9F);
            SoundToPlay.Play();
            toPlay = (toPlay+1)%rangeScan;
        }
        else if (Health <= 0)
        {
            //Destroy(); 
            SoundToPlay.PlayOneShot(myAudio[IndexDeathSound], 0.9F);
            SoundToPlay.Play();
            Score.enemyScore += Points;

            transform.Translate(0, -100, Time.deltaTime);
            Invoke("Die", 0.8f);
        }
    }

    protected virtual void Die()
    {
        Debug.Log("DIE KURWA");
        Debug.Log(this.gameObject.name);
        Destroy(this.gameObject);
    }
}
