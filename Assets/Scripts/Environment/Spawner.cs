using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject EnemyType;
    private GameObject Enemy;
    private GameObject Parent;
    [Range(0f, 100f)] public float chance;
    void Start()
    {
        Destroy(gameObject);
        if (Random.value <= (chance / 100)){

        Parent = gameObject.transform.parent.gameObject; 
        Enemy = Instantiate(EnemyType, gameObject.transform.position /*+ transform.position*/, Quaternion.identity);
        Enemy.transform.parent = Parent.transform;
        //Enemy.transform.localScale += new Vector3(2, 1, 1);
        }
        /*
        List<GameObject> SpawnPoints = FindObjectsOfTypeAll(typeof(GameObject)).Cast<GameObject>().Where(g => g.tag == "SpawnPoint").ToList();

        foreach(GameObject point in SpawnPoints)
        {
            Instantiate(Enemies[Random.Range(0, Enemies.Count)], point.transform.position + transform.position, Quaternion.identity);
        }
        */
    }

}
