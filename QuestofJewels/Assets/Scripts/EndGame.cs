using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EndGame : MonoBehaviour
{
    public TextMeshProUGUI You_win, Timetext, DeathText, timeBest, DeathBest;
    public GameObject mainmenu;
    // Start is called before the first frame update

    public void gameEnd()
    {
        Timer.Instance.setPause(true);
        IEnumerator enumerator = playText();
        StartCoroutine(enumerator);
    }

    public void BacktoMain()
    {
        SceneManager.LoadScene("StartScene");
    }

    IEnumerator playText()
    {
        yield return new WaitForSeconds(1f);
        You_win.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        Timetext.text = Timer.Instance.GetTimeOnly();
        Timetext.gameObject.SetActive(true);
        if (Timer.Instance.BestTime())
            timeBest.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        DeathText.text = Timer.Instance.GetDeathOnly();
        DeathText.gameObject.SetActive(true);
        if (Timer.Instance.BestDeath())
            DeathBest.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        mainmenu.SetActive(true);
    }
}
