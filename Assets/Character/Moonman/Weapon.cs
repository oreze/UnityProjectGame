using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public Animator animator;
    public GameObject projectile;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        animator.SetTrigger("Shoot");
        Instantiate(projectile, firePoint.position, firePoint.rotation);

    }
}
