using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public static Timer Instance
    {
        get
        {
            if (instance != null)
                return instance;

            instance = FindObjectOfType<Timer>();

            if (instance != null)
                return instance;

            Create();

            return instance;
        }
    }
    protected static Timer instance;
    public static Timer Create()
    {
        GameObject TimerGameObject = new GameObject("Timer");
        instance = TimerGameObject.AddComponent<Timer>();
        return instance;
    }
    static float CurTime;
    static int DeathCount;

    bool pause;

    Text TimerText;
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!pause)
            CurTime += Time.deltaTime;
        if (!TimerText)
            setText();
        if (TimerText)
            TimerText.text = GetTimeString();
    }

    public void setPause(bool b)
    {
        pause = b;
    }

    public void Reset()
    {
        CurTime = 0;
        DeathCount = 0;
    }

    public void DieOnce()
    {
        DeathCount++;
    }

    public string GetTimeString()
    {
        int min = 0;
        float temp = CurTime;
        while (temp >= 60)
        {
            temp -= 60;
            min ++;
        }
        if(min >0)
            return "Time : " + min.ToString() + " : "+ temp.ToString("00.00") + "\nDeath : " + DeathCount.ToString();
        return "Time : " + CurTime.ToString("0.00") + "\nDeath : " + DeathCount.ToString();
    }

    public void setText()
    {
        TimerText = GameObject.Find("TimerText").GetComponent<Text>();
        
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        if(scene.buildIndex > 2)
            setText();
    }
}
