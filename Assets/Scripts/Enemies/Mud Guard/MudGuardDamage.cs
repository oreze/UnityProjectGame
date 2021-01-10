using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudGuardDamage : EnemyDamage
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
        GetComponent<Animator>().SetTrigger("isDamaged");
        Debug.Log("Health - damage = " + Health + " - " + damage + " = " + (Health - damage));
        Health -= damage;
        Instantiate(BloodSplash, new Vector2(RigidBody.position.x, RigidBody.position.y - 0.1f), Quaternion.identity);
        if (Healthbar)
            Healthbar.setHealth(Health, MaxHealth);
        if (Health > 0)
        {
            toPlay = Random.Range(0, rangeScan);
            SoundToPlay.PlayOneShot(myAudio[toPlay], 0.9F);
            SoundToPlay.Play();
            //toPlay = (toPlay+1)%rangeScan;
        }
        else
        {
            //Destroy(); 
            SoundToPlay.PlayOneShot(myAudio[IndexDeathSound], 0.9F);
            SoundToPlay.Play();

            transform.Translate(0, -100, Time.deltaTime);
            Invoke("Die", 0.8f);
        }
    }
}
