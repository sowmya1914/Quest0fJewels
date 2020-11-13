using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossRoomScript : MonoBehaviour
{
    List<GameObject> enemyList;
    public List<GameObject> enemyType;
    Gamekit2D.Damageable boss;
    bool start;
    public UnityEvent onStart, onEnd;
    // Start is called before the first frame update
    void Start()
    {
        enemyList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(start)
        {
            if (enemyList.Count == 0)
                allEnemyIsDead();
        }
        
    }

    public void startFight()
    {
        boss = GameObject.Find("Boss").GetComponent<Gamekit2D.Damageable>();
        if (!boss)
            Debug.Log("No Boss is Found");
        start = true;
        onStart.Invoke();
    }
    public void endFight()
    {
        onEnd.Invoke();
    }

    public void bossGotHit()
    {
        boss.m_ImmuteToDamage = true;
    }

    void allEnemyIsDead()
    {
        boss.m_ImmuteToDamage = false;
    }
}
