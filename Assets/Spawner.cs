using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> Enemies;
    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> SpawnPoints = FindObjectsOfTypeAll(typeof(GameObject)).Cast<GameObject>().Where(g => g.tag == "SpawnPoint").ToList();

        foreach(GameObject point in SpawnPoints)
        {
            Instantiate(Enemies[0], point.transform.position + transform.position, Quaternion.identity);
        }
    }
}
