using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyDamage
{
    (int AttackID, int Damage) MakeDamage();
    void TakeDamage(int damage);



    GameObject GetGameObject();
}
