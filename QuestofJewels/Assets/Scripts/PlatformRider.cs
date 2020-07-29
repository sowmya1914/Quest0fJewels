using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamekit2D;
public class PlatformRider : MonoBehaviour
{
    public MovingPlatform myPlatform = null;
    public Vector2 TargetPosition;
    private Rigidbody2D myRigidbody;
    public float speed = 2;
    // Start is called before the first frame update
    void Start()
    {
        if (myPlatform == null)
        {
            myPlatform = GetComponentInParent<MovingPlatform>();
        }
        // myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (myPlatform != null)
        {
            Vector3 target = Vector3.zero;
            if (myPlatform.GetTimeRemaining() > 0)
            {
                target = TargetPosition;
            }
            Vector3 direction = target - transform.localPosition;
            direction *= speed * Time.deltaTime;
           // if (direction != Vector3.zero)
            {

                transform.Translate(direction, Space.Self);
            }
        }

    }
}
