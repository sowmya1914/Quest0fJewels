using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicChanger : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioClip normalMusic;
    public AudioClip battleMusic;
    public Gamekit2D.PlayerCharacter thePlayer;
    public float enemyDetectRadius = 4.0f;
    public LayerMask layerMaskDetect = (1 << 9);
    private RaycastHit hitBox;
    Collider2D[] detections = new Collider2D[1024];
    private Rigidbody2D targetRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        ReTarget();
    }
    private void Awake()
    {
        ReTarget();
    }
    public void ReTarget()
    {
        if (backgroundMusic == null)
        {
            backgroundMusic = GetComponent<AudioSource>();
   
        }
        if (thePlayer == null)
        {
            thePlayer = GameObject.FindObjectOfType<Gamekit2D.PlayerCharacter>();
        }
        targetRigidbody = thePlayer.GetComponent<Rigidbody2D>();

    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (thePlayer != null)
        {
            if (backgroundMusic != null)
            {

                // if (Physics.SphereCast(thePlayer.GetComponent<Rigidbody2D>().position, enemyDetectRadius, Vector3.right, out hitBox, enemyDetectRadius * 2.0f, layerMaskDetect))
                if (Physics2D.OverlapCircleNonAlloc(targetRigidbody.position, enemyDetectRadius * 2, detections, layerMaskDetect) > 0)
                {

                    if (backgroundMusic.clip != battleMusic)
                    {
                        float time = backgroundMusic.time;
                        backgroundMusic.clip = battleMusic;
                        backgroundMusic.time = time;
                        backgroundMusic.Play();
                    }

                }
                else
                {
                    if (backgroundMusic.clip != normalMusic)
                    {
                        float time = backgroundMusic.time;
                        backgroundMusic.clip = normalMusic;
                        backgroundMusic.Play();
                        backgroundMusic.time = time;
                    }
                }
            }
        }
    }
}
