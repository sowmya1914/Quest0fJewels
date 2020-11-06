using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class CutsceneController : MonoBehaviour
{
    public Sprite[] Frame;
    public AudioSource BGMusic;
    public Image CurrentImage;
    public Text Dialogue;
    public string[] FrameText;

    public void Start()
    {
        StartCoroutine(playCutscene());
    }
    IEnumerator playCutscene()
    {
        CurrentImage.sprite = Frame[0];
        Dialogue.text = FrameText[0];
        yield return new WaitForSeconds(4);
        CurrentImage.sprite = Frame[1];
        Dialogue.text = FrameText[1];
        yield return new WaitForSeconds(4);
        CurrentImage.sprite = Frame[2];
        Dialogue.text = FrameText[2];
        yield return new WaitForSeconds(4);
        CurrentImage.sprite = Frame[3];
        Dialogue.text = FrameText[3];
        yield return new WaitForSeconds(4);
        CurrentImage.sprite = Frame[4];
        Dialogue.text = FrameText[4];
        yield return new WaitForSeconds(4);
        CurrentImage.sprite = Frame[5];
        Dialogue.text = FrameText[5];
        yield return new WaitForSeconds(4);
        CurrentImage.sprite = Frame[6];
        Dialogue.text = FrameText[6];
        yield return new WaitForSeconds(4);
    }

}
