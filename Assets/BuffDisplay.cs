using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDisplay : MonoBehaviour
{
    public GameObject Damage, AttackSpeed, MovementSpeed;
    public int DamageCounter, AttackCounter, MovementCounter;
    void Start()
    {
        DamageCounter = 0;
        AttackCounter = 0;
        MovementCounter = 0;
    }

  
    void Update()
    {
        if(DamageCounter > 0)
        {
            Damage.SetActive(true);
        }
        else
        {
            Damage.SetActive(false);
        }

        if (AttackCounter > 0)
        {
            AttackSpeed.SetActive(true);
        }
        else
        {
            AttackSpeed.SetActive(false);
        }

        if (MovementCounter > 0)
        {
            MovementSpeed.SetActive(true);
        }
        else
        {
            MovementSpeed.SetActive(false);
        }

    }
}
