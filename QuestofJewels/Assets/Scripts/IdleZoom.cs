using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Gamekit2D;
public class IdleZoom : MonoBehaviour
{
    public CinemachineVirtualCamera cinemaCamera;
    public float idleTime = 5.0f;
    public float defaultIdleTime = 5.0f;
    public PlayerCharacter player;
    public float defaultView = 3.73f;
    public float zoomedOutView = 7.73f;
    // Start is called before the first frame update
    void Start()
    {
        cinemaCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Awake()
    {
        if(cinemaCamera == null)
        {
            cinemaCamera = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        }
        if(player == null)
        {
            player = GameObject.FindObjectOfType<PlayerCharacter>();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(player != null)
        {
            if(Gamekit2D.PlayerInput.Instance.Horizontal.Value != 0)
            {
                cinemaCamera.m_Lens.OrthographicSize = Mathf.Lerp(cinemaCamera.m_Lens.OrthographicSize, defaultView, Time.fixedDeltaTime);
                idleTime = defaultIdleTime;
            }
            else if (Gamekit2D.PlayerInput.Instance.Horizontal.Value != 0)
            {
                cinemaCamera.m_Lens.OrthographicSize = Mathf.Lerp(cinemaCamera.m_Lens.OrthographicSize, defaultView, Time.fixedDeltaTime);
                idleTime = defaultIdleTime;
            }
            else if (Gamekit2D.PlayerInput.Instance.MeleeAttack.Down == true)
            {
                cinemaCamera.m_Lens.OrthographicSize = Mathf.Lerp(cinemaCamera.m_Lens.OrthographicSize, defaultView, Time.fixedDeltaTime);
                idleTime = defaultIdleTime;

            }
            else if (Gamekit2D.PlayerInput.Instance.RangedAttack.Down == true)
            {
                cinemaCamera.m_Lens.OrthographicSize = Mathf.Lerp(cinemaCamera.m_Lens.OrthographicSize, defaultView, Time.fixedDeltaTime);
                idleTime = defaultIdleTime;
            }
            else
            {
                if(idleTime > 0)
                {
                    idleTime -= 1 * Time.deltaTime;
                }
                else
                {
                cinemaCamera.m_Lens.OrthographicSize = Mathf.Lerp(cinemaCamera.m_Lens.OrthographicSize, zoomedOutView, Time.fixedDeltaTime);
                }

            }
        }
    }
}
