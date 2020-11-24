using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CutScene : MonoBehaviour
{
    VideoPlayer player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        ulong frame = player.frameCount - 1;
        if (player.frame == (long)frame)
        if (player.frame > 0 && !player.isPlaying)
        {
            NextScene();
        }
        if (Input.GetKeyDown(KeyCode.Space))
            NextScene();
    }

    public void NextScene()
    {
        Timer.Instance.Reset();
        GetComponent<Gamekit2D.TransitionPoint>().TransitionInternal();
    }
}
