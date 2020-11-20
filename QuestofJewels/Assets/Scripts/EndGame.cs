using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public Text Timetext;
    // Start is called before the first frame update

    public void gameEnd()
    {
        Timer.Instance.setPause(false);
        Timetext.text = Timer.Instance.GetTimeString();
    }

    public void BacktoMain()
    {
        SceneManager.LoadScene("StartScene");
    }
}
