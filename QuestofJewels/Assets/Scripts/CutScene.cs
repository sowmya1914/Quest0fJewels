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
        if (player.frame > 0 && !player.isPlaying)
        {
            Timer.Instance.Reset();
            GetComponent<Gamekit2D.TransitionPoint>().TransitionInternal();
        }
    }
}
