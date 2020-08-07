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
    Damager damagerScript = null;
    // Start is called before the first frame update
    void Start()
    {
        if (myPlatform == null)
        {
            myPlatform = GetComponentInParent<MovingPlatform>();
        }
        if (damagerScript == null)
        {
            damagerScript = GetComponent<Damager>();
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
            if (target != Vector3.zero)
            {
                if (damagerScript != null)
                    damagerScript.enabled = true;
            }
            else
            {
                if (damagerScript != null)
                    damagerScript.enabled = false;
            }

            {

                transform.Translate(direction, Space.Self);
            }
        }

    }
}
