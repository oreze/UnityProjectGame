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
        FloatingPositionDown = transform.position.y;
        FloatingPositionUp = transform.position.y + 0.2f;
        Debug.Log(gameObject.transform.position.y + " == with");
        Debug.Log(transform.position.y + " == without");
        goUp = true;
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

            if (Enumerable.Range(0, 25).Contains(random))
            {
                float speedBoost = PlayerControllerScript.Speed * 0.1f;
                SpeedBuffs.AddFirst(speedBoost);
                PlayerControllerScript.Speed += speedBoost;
                StartCoroutine(WaitAndRestore(speedBoost, 5f, Type.Speed));
            }
            else if (Enumerable.Range(25, 25).Contains(random))
            {
                Debug.Log("BUFF ATTACK SPEED += " + (PlayerControllerScript.AttackSpeed * 1.1f));
                float attackSpeedBoost = PlayerControllerScript.AttackSpeed * 0.1f;
                AttackSpeedBuffs.AddFirst(attackSpeedBoost);
                PlayerControllerScript.AttackSpeed += attackSpeedBoost;
                StartCoroutine(WaitAndRestore(attackSpeedBoost, 5f, Type.AttackSpeed));
            }
            else if (Enumerable.Range(50, 25).Contains(random))
            {
                Debug.Log("BUFF DAMAGE += " + (PlayerControllerScript.CurrentWeapon.damage * 1.1f));
                int damageBoost = (int)(PlayerControllerScript.CurrentWeapon.damage * 0.1f);
                DamageBuffs.AddFirst(damageBoost);
                PlayerControllerScript.CurrentWeapon.damage += damageBoost;
                StartCoroutine(WaitAndRestore(damageBoost, 5f, Type.Damage));
            }
            else if (Enumerable.Range(75, 25).Contains(random))
            {
                Debug.Log("HEALING += " + (PlayerControllerScript.CurrentHealth + 10));
                if (PlayerControllerScript.CurrentHealth + 10 >= 100)
                    PlayerControllerScript.CurrentHealth = 100;
                else
                    PlayerControllerScript.CurrentHealth += 10;

                PlayerControllerScript.HealthBar.SetHealth(PlayerControllerScript.CurrentHealth);
            }
        }
    }

    private IEnumerator WaitAndRestore(float speedBoost, float time, Type type)
    {
        yield return new WaitForSeconds(time);
        if (type == Type.Speed)
        {
            PlayerControllerScript.Speed -= SpeedBuffs.First.Value;
            SpeedBuffs.RemoveFirst();
        }
        else if (type == Type.AttackSpeed)
        {
            PlayerControllerScript.AttackSpeed -= AttackSpeedBuffs.First.Value;
            AttackSpeedBuffs.RemoveFirst();
        }
        else if (type == Type.Damage)
        {
            PlayerControllerScript.CurrentWeapon.damage -= DamageBuffs.First.Value;
            DamageBuffs.RemoveFirst();
        }
    }
}
