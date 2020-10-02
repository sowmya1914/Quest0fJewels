using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeWall : MonoBehaviour
{
    [Range(0.1f, 99)]
    public float raiseTime;
    [Range(0.1f, 99)]
    public float dropTime;
    [Range(0.2f, 99)]
    public float raiseWaitTime;
    [Range(0.2f, 99)]
    public float dropWaitTime;
    [Range(0.0f, 99)]
    public float initialWait = 0f;
    float distance;
    float cheight = 0;
    float timer;
    bool raised = false;
    enum state { init, raise, rPause, drop, dPause};
    state cstate;
    // Start is called before the first frame update
    void Start()
    {
        distance = transform.Find("Top").transform.position.y - transform.position.y;
        distance = Mathf.Abs(distance);
        timer = initialWait;
        cstate = state.init;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (cstate)
        {
            case state.init:
                timer -= Time.deltaTime;
                if(timer<=0)
                {
                    //timer = frequency;
                    cstate = state.raise;
                }
                break;
            case state.raise:
                StartCoroutine(raise());
                break;
            case state.rPause:
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    cstate = state.drop;
                }
                break;
            case state.drop:
                StartCoroutine(drop());
                break;
            case state.dPause:
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    cstate = state.raise;
                }
                break;
            default:
                break;
        }
    }
    IEnumerator raise()
    {
        float speed = distance / raiseTime;
        while (cheight < distance)
        {
            float amount = Mathf.Min(speed * Time.deltaTime, distance - cheight);
            transform.Translate(0, amount, 0);
            cheight += amount;
            yield return null;
        }
        timer = raiseWaitTime;
        cstate = state.rPause;
    }
    IEnumerator drop()
    {
        float speed = distance / dropTime;
        while (cheight > 0)
        {
            float amount = Mathf.Min(speed * Time.deltaTime, cheight);
            transform.Translate(0, -amount, 0);
            cheight -= amount;
            yield return null;
        }
        timer = dropWaitTime;
        cstate = state.dPause;
    }
}
