using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    private int platformTimer;

    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
        platformTimer = 0;
    }

    void Update()
    {
        

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            effector.rotationalOffset = 180f;
            platformTimer = 2;
        }
        /*
          if (Input.GetKey(KeyCode.Space))
          {
              effector.rotationalOffset = 0;
              platformTimer = 0;
          }
          */

        if (platformTimer != 0)
        {
            platformTimer -= 1;
        }
        else effector.rotationalOffset = 0;
      
    }
}
