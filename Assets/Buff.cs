using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    public float FloatingPosition;
    [Range(9.8f, 11f)]
    public float FloatUpStrenght;
    public float RandomRotationStrenght;
    // Start is called before the first frame update

    private void Start()
    {
        FloatingPosition = transform.position.y;
    }

    void FixedUpdate()
    {
        if (transform.position.y < FloatingPosition)
        {
            Debug.Log("force being applied upwards to move object up");
            transform.GetComponent<Rigidbody2D>().AddForce(Vector3.up * (FloatUpStrenght));
            transform.Rotate(0, RandomRotationStrenght, 0);
        }
        if (transform.position.y >= FloatingPosition)
        {
            Debug.Log("force applied is less than the gravitational force so that the object comes down. Here mass of object is 2.  ");
            transform.Rotate(0, RandomRotationStrenght, 0);
        }
    }
}
