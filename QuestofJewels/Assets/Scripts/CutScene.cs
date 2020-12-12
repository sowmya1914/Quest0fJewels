using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class CutScene : MonoBehaviour
{
    VideoPlayer player;
    VideoText VideoText;
    public Text framecount;
    public Text framet;

    public Text caption;
    [TextArea]
    public string[] FrameText;
    // Start is called before the first frame update
    void Start()
    {
        Gamekit2D.BackgroundMusicPlayer.Instance.Stop();
        player = GetComponent<VideoPlayer>();
        VideoText = GetComponent<VideoText>();
    }

    // Update is called once per frame
    void Update()
    {
        ulong frame = player.frameCount - 1;
        playText();
        framecount.text = "FrameCount : " + player.frameCount;
        framet.text = "Frame : " + player.frame;
        if (player.frame == (long)frame)
            //if (player.frame > 0 && !player.isPlaying)
            if (!player.isPlaying)
            {
                NextScene();
            }
        if (Input.GetKeyDown(KeyCode.Space))
            NextScene();
    }

    public void NextScene()
    {
        player.Stop();
        Timer.Instance.Reset();
        GetComponent<Gamekit2D.TransitionPoint>().TransitionInternal();
        Gamekit2D.BackgroundMusicPlayer.Instance.Play();
    }

    public void playText()
    {
        if (player.frame == 120) caption.text = FrameText[0];
        if (player.frame == 457) caption.text = FrameText[1];
        if (player.frame == 800) caption.text = FrameText[2];
        if (player.frame == 1137) caption.text = FrameText[3];
        if (player.frame == 1474) caption.text = "";
        if (player.frame == 1796) caption.text = FrameText[5];
        if (player.frame == 2133) caption.text = FrameText[6];
        if (player.frame == 2500) caption.text = "";

    }
}
