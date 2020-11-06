using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private static float saveLoadTime;
    // Start is called before the first frame update
    void Start()
    {
        ReTarget();
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += loadMusicTime;
       // SceneManager.sceneUnloaded += saveMusicTime;
    }

    private void saveMusicTime(Scene current)
    {
        if (backgroundMusic != null)
            saveLoadTime = backgroundMusic.time;
        Debug.Log("saving music " + saveLoadTime);
    }
    private void loadMusicTime(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("loading music " + saveLoadTime);
        if (backgroundMusic != null)
            backgroundMusic.time = saveLoadTime;
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
                saveLoadTime = backgroundMusic.time;
               

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
