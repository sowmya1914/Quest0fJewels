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
    public float raiseDuration;
    [Range(0.2f, 99)]
    public float frequency;
    float height;
    float cheight = 0;
    float timer;
    bool raised = false;
    // Start is called before the first frame update
    void Start()
    {
        float temp = transform.Find("Top").transform.position.y - transform.position.y;
        if (temp <= 0)
            Debug.LogError("SpikeWall Need more height!");
        else
            height = temp;
        timer = frequency;
    }

    // Update is called once per frame
    void Update()
    {
        if (!raised)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                StartCoroutine(raise());
                timer = raiseDuration;
                raised = true;
            }
        }
        else
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                StartCoroutine(drop());
                timer = frequency;
                raised = false;
            }
        }
    }
    IEnumerator raise()
    {
        float speed = height / raiseTime;
        while (cheight < height)
        {
            float amount = Mathf.Min(speed * Time.deltaTime, height - cheight);
            transform.Translate(0, amount, 0);
            cheight += amount;
            yield return null;
        }
    }
    IEnumerator drop()
    {
        float speed = height / dropTime;
        while (cheight > 0)
        {
            float amount = Mathf.Min(speed * Time.deltaTime, cheight);
            transform.Translate(0, -amount, 0);
            cheight -= amount;
            yield return null;
        }
    }
}
