using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Buff : MonoBehaviour
{
    public PlayerController PlayerControllerScript;

    public float FloatingPositionDown;
    public float FloatingPositionUp;
    private bool goUp;

    [Range(9.8f, 13f)]
    public float FloatUpStrenght;
    public float RandomRotationStrenght;

    public static LinkedList<float> SpeedBuffQueue = new LinkedList<float>();
    // Start is called before the first frame update

    private void Start()
    {
        FloatingPositionDown = transform.position.y;
        FloatingPositionUp = transform.position.y + 0.2f;
        Debug.Log(gameObject.transform.position.y + " == with");
        Debug.Log(transform.position.y + " == without");
        goUp = true;
    }

    void FixedUpdate()
    {
        transform.Rotate(0, RandomRotationStrenght, 0);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Destroy(gameObject);
            int random = Random.Range(0, 100);

            if (Enumerable.Range(0, 100).Contains(random))
            {
                float speedBoost = PlayerControllerScript.Speed * 0.5f;
                SpeedBuffQueue.AddFirst(speedBoost);
                PlayerControllerScript.Speed += speedBoost;
                StartCoroutine(WaitAndRestoreSpeed(speedBoost, 1f));

            }
        }
    }

    private IEnumerator WaitAndRestoreSpeed(float speedBoost, float time)
    {
        yield return new WaitForSeconds(time);
        PlayerControllerScript.Speed -= SpeedBuffQueue.First.Value;
        SpeedBuffQueue.RemoveFirst();
    }
}
