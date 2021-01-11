using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDeleter : MonoBehaviour
{
    private const float PLAYER_DISTANCE_DELETE_LEVEL_PART = 10f;

    protected Transform Target;
    private Vector3 EndPosition;
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        EndPosition = gameObject.transform.Find("EndPosition").position;
    }

    void Update()
    {
        if (!Target)
        {
            Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }

        if(EndPosition.x < Target.position.x)
        {
            if (Vector3.Distance(Target.position, EndPosition) > PLAYER_DISTANCE_DELETE_LEVEL_PART)
            {
                Destroy(gameObject);
            }
        }
    }
}
