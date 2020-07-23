using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Gamekit2D;
public class PortalScript : MonoBehaviour
{
    private PlayerCharacter thePlayerCharacter = null;
    [SerializeField]
    private bool isPortalOpen = false;
    [SerializeField]
    private string transitionScene;
    [SerializeField]
    private Vector3 myDepositLocation;
    [SerializeField]
    private static Vector3 lastDepositLocation;
    [SerializeField]
    private static bool transitioning = false;

    private Animator m_animator = null;
    
    public bool IS_PORTAL_OPEN
    {
        get { return isPortalOpen; }
        set { isPortalOpen = value; PortalChanged(); }
    }

    public string TRANSITION_SCENE
    {
        get { return transitionScene; }
        set { transitionScene = value; }
    }

    private void Start()
    {
        thePlayerCharacter = GameObject.FindObjectOfType<PlayerCharacter>();
        m_animator = GetComponent<Animator>();
        if (m_animator != null)
        {
            m_animator.speed = 0;
        }
    }
    public void OpenPortal()
    {
      if(m_animator != null)
        {
            m_animator.speed = 1;
        }
    }

    public void ClosePortal()
    {
        if (m_animator != null)
        {
            m_animator.speed = 0;
        }
    }
    private void Awake()
    {
        if(transitioning == true)
        {
            thePlayerCharacter = GameObject.FindObjectOfType<PlayerCharacter>();
            if(thePlayerCharacter != null)
            {
                thePlayerCharacter.transform.position = lastDepositLocation;
            }
            transitioning = false;
        }
    }
    private void PortalChanged()
    {
        if (isPortalOpen == true)
        {
            OpenPortal();
        }
        else
        {
            ClosePortal();
        }
    }

    private void TransitionTo(string newScene)
    {
        Scene nextScene = SceneManager.GetSceneByName(newScene);
        if(nextScene != null)
        {
            lastDepositLocation = myDepositLocation;
            transitioning = true;
            SceneManager.LoadScene(newScene, LoadSceneMode.Single);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            if(thePlayerCharacter != null)
            {
                if(isPortalOpen == true)
                {
                    if(Vector3.Distance(thePlayerCharacter.transform.position, transform.position) < 3)
                    {
                        TransitionTo(transitionScene);
                    }
                }
            }
        }
    }


}
