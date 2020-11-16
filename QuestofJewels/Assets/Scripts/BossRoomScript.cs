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
    public UnityEvent onEnd, onDie;
    // Start is called before the first frame update
    void Start()
    {
        enemyList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            if (enemyList.Count == 0)
                allEnemyIsDead();
        }

    }

    public void startFight()
    {
        if (!start)
        {
            boss = GameObject.Find("Boss").GetComponent<Gamekit2D.Damageable>();
            if (!boss)
                Debug.Log("No Boss is Found");
            start = true;
            foreach (var item in enemyType)
            {
                GameObject temp = Instantiate(item);
                temp.SetActive(true);
                temp.GetComponent<Gamekit2D.Damageable>().OnDie.AddListener(removeEnemy);
                enemyList.Add(temp);
            }
        }
    }
    public void endFight()
    {
        onEnd.Invoke();
    }

    public void bossGotHit()
    {
        boss.m_ImmuteToDamage = true;
        if (boss.CurrentHealth > 0)
        {
            foreach (var item in enemyType)
            {
                GameObject temp = Instantiate(item);
                temp.SetActive(true);
                temp.GetComponent<Gamekit2D.Damageable>().OnDie.AddListener(removeEnemy);
                enemyList.Add(temp);
            }
        }
    }

    void allEnemyIsDead()
    {
        boss.m_ImmuteToDamage = false;
    }

    void removeEnemy(Gamekit2D.Damager er, Gamekit2D.Damageable able)
    {
        GameObject obj = able.gameObject;
        if (enemyList.Contains(obj))
            enemyList.Remove(obj);
    }

    public void charDie()
    {
        onDie.Invoke();
    }
}
