using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Buff : MonoBehaviour
{
    private PlayerController PlayerControllerScript;

    public float FloatingPositionDown;
    public float FloatingPositionUp;
    private bool goUp;

    public AudioSource mySource;
    public AudioClip[] myAudio;
    //public int toPlay;

    [Range(9.8f, 13f)]
    public float FloatUpStrenght;
    public float RandomRotationStrenght;

    protected Score score;
    private enum Type
    {
        Speed,
        AttackSpeed,
        Damage,
        Healing,
    }

    public static LinkedList<float> SpeedBuffs = new LinkedList<float>();
    public static LinkedList<float> AttackSpeedBuffs = new LinkedList<float>();
    public static LinkedList<int> DamageBuffs = new LinkedList<int>();
    // Start is called before the first frame update

    private void Start()
    {
        PlayerControllerScript =  GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        mySource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        FloatingPositionDown = transform.position.y;
        FloatingPositionUp = transform.position.y + 0.2f;
        Debug.Log(gameObject.transform.position.y + " == with");
        Debug.Log(transform.position.y + " == without");
        goUp = true;
        score  = GameObject.FindObjectOfType<Score>();
    }

    public void FixedUpdate()
    {
        transform.Rotate(0, RandomRotationStrenght, 0);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Destroy(gameObject);
            mySource.PlayOneShot(myAudio[0], 0.6F);
            mySource.Play();
            int random = Random.Range(0, 100);
            Destroy(gameObject);
            /*
             * - speed
             * - attack speed
             * - dmg
             * - healing
             */

            if (Enumerable.Range(0, 10).Contains(random))
            {
                score.enemyScore += 30;
                float speedBoost = PlayerControllerScript.Speed * 0.1f;
                SpeedBuffs.AddFirst(speedBoost);
                PlayerControllerScript.Speed += speedBoost;
                StartCoroutine(WaitAndRestore(speedBoost, 5f, 1));
                
            }
            else if (Enumerable.Range(10, 25).Contains(random))
            {
                score.enemyScore += 30;
                Debug.Log("BUFF ATTACK SPEED += " + (PlayerControllerScript.AttackSpeed * 1.1f));
                float attackSpeedBoost = PlayerControllerScript.AttackSpeed * 0.1f;
                AttackSpeedBuffs.AddFirst(attackSpeedBoost);
                PlayerControllerScript.AttackSpeed += attackSpeedBoost;
                StartCoroutine(WaitAndRestore(attackSpeedBoost, 5f, 2));
                
            }
            else if (Enumerable.Range(35, 25).Contains(random))
            {
                score.enemyScore += 30;
                Debug.Log("BUFF DAMAGE += " + (PlayerControllerScript.damage * 1.2f));
                int damageBoost = (int)(PlayerControllerScript.damage * 0.2f);
                DamageBuffs.AddFirst(damageBoost);
                PlayerControllerScript.damage += damageBoost;

                StartCoroutine(WaitAndRestore(damageBoost, 5f, 3));
                
            }
            else if (Enumerable.Range(60, 40).Contains(random))
            {
                score.enemyScore += 30;
                Debug.Log("HEALING += " + (PlayerControllerScript.CurrentHealth + 30));
                if (PlayerControllerScript.CurrentHealth + 30 >= 100)
                    PlayerControllerScript.CurrentHealth = 100;
                else
                    PlayerControllerScript.CurrentHealth += 30;

                PlayerControllerScript.HealthBar.SetHealth(PlayerControllerScript.CurrentHealth);
                
            }
        }
    }

    private IEnumerator WaitAndRestore(float speedBoost, float time, int type)
    {
        yield return new WaitForSeconds(5);
        if (type == 1)
        {
            PlayerControllerScript.Speed -= SpeedBuffs.First.Value;
            SpeedBuffs.RemoveFirst();
        }
        else if (type == 2)
        {
            PlayerControllerScript.AttackSpeed -= AttackSpeedBuffs.First.Value;
            AttackSpeedBuffs.RemoveFirst();
        }
        else if (type == 3)
        {
            PlayerControllerScript.damage -= DamageBuffs.First.Value;
            DamageBuffs.RemoveFirst();
        }
    }
}
