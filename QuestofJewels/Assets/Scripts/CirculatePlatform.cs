using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirculatePlatform : MonoBehaviour
{
    public GameObject platform;
    Vector2 startPos;
    Vector2 center;
    float radius;
    [Range(0.1f, 180)]
    public float degPerSec;
    float degree = 0;
    public bool clockwise;
    Gamekit2D.PlatformCatcher platformCatcher;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.Find("Start").transform.position;
        center = transform.Find("Center").transform.position;
        radius = Vector2.Distance(startPos, center);
        degree = Mathf.Atan2(startPos.y - center.y, startPos.x - center.x);

        center = transform.Find("Center").transform.localPosition;
        if (platformCatcher == null)
            platformCatcher = platform.GetComponent<Gamekit2D.PlatformCatcher>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 velocity = new Vector2();
        velocity.x = platform.transform.localPosition.x;
        velocity.y = platform.transform.localPosition.y;
        if (clockwise)
            degree += (degPerSec / 60f) * Mathf.PI / 180f;
        else
            degree -= (degPerSec / 60f) * Mathf.PI / 180f;
        float x = Mathf.Cos(degree);
        float y = Mathf.Sin(degree);
        platform.transform.localPosition = center + new Vector2(x * radius, y * radius);
        velocity.x = platform.transform.localPosition.x - velocity.x ;
        velocity.y = platform.transform.localPosition.y - velocity.y ;
        if (platformCatcher != null)
            platformCatcher.MoveCaughtObjects(velocity);
    }
}
