using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeLauncher : MonoBehaviour
{
    public GameObject bullet;
    Vector3 destination;
    [Tooltip("Bullet travel speed")]
    [Range(0.01f, 99)]
    public float bulletSpeed = 0.1f;
    [Tooltip("How long to wait for next shot")]
    [Range(0.1f, 99)]
    public float firingRate = 0.1f;
    [Range(0, 9)]
    [Tooltip("How long to wait before first shot")]
    public float initialWaitTimer;
    float timer = 0;
    int index;
    List<GameObject> objectPool;
    float distance;
    [Range(2, 99)]
    public int objectPoolSize = 2;
    bool pause = false;
    // Start is called before the first frame update
    void Start()
    {
        objectPool = new List<GameObject>(objectPoolSize);
        for (int i = 0; i < objectPoolSize; i++)
        {
            GameObject temp = Instantiate(bullet, this.transform);
            temp.SetActive(false);
            objectPool.Add(temp);
        }

        destination = transform.Find("Des").transform.position;
        destination = destination - transform.position;
        distance = destination.magnitude;
        timer = initialWaitTimer;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!pause)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = firingRate;
                objectPool[index].SetActive(true);
                objectPool[index].transform.position = transform.position;
                index = index == objectPoolSize - 1 ? 0 : index + 1;
            }
            foreach (var item in objectPool)
            {
                if (item.activeSelf)
                {
                    item.transform.Translate(destination * bulletSpeed * Time.deltaTime, Space.World);
                    float d = (item.transform.position - transform.position).magnitude;
                    if (d > distance)
                        item.SetActive(false);
                }
            }
        }
    }
    public void TurnOff()
    {
        pause = true;
        foreach (var item in objectPool)
        {
            item.SetActive(false);
        }
    }
}
