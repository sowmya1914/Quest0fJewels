using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VideoText : MonoBehaviour
{
    public Text videoText;
    public string[] FrameText;
    

    IEnumerator PlayText()
    {
        yield return new WaitForSeconds(2);
        videoText.text = FrameText[0];
        yield return new WaitForSeconds(5.5f);
        videoText.text = FrameText[1];
        yield return new WaitForSeconds(5.5f);
        videoText.text = FrameText[2];
        yield return new WaitForSeconds(5.5f);
        videoText.text = FrameText[3];
        yield return new WaitForSeconds(5.5f);
        videoText.text = FrameText[4];
        yield return new WaitForSeconds(5.5f);
        videoText.text = FrameText[5];
        yield return new WaitForSeconds(5.5f);
        videoText.text = FrameText[6];
        yield return new WaitForSeconds(5.5f);
        videoText.text = "";
        

    }
    void Start()
    {

    }

    public void Playtext()
    {
        StartCoroutine(PlayText());
    }

   
}
