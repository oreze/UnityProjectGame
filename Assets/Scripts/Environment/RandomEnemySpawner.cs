using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemySpawner : MonoBehaviour
{
    public List<GameObject> enemies;
    public List<int> chances;
    private GameObject Enemy;
    private GameObject Parent;
    private int totalChance;
    void Start()
    {
        Parent = gameObject.transform.parent.gameObject;
        totalChance = enemies.Count * 100;
        int chance = Random.Range(0, totalChance);
        //Debug.Log(name + " " + (chances[chance / 100] + (100 * (chance / 100))) + " " + chance);
        if (chances[chance / 100]+ (100*(chance/100)) <= chance) {
           // Debug.Log("Spawn " + enemies[chance / 100].name);
            Enemy = Instantiate(enemies[chance / 100], gameObject.transform.position, Quaternion.identity);
            Enemy.transform.parent = Parent.transform;
        }
    }
}
