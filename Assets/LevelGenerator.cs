using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform LevelPrefab1;
    private void Awake()
    {
      Instantiate(LevelPrefab1, new Vector3(3.4f, 0.48f), Quaternion.identity); 
    }
}
