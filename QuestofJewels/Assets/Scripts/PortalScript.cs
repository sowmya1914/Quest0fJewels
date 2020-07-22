using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PortalScript : MonoBehaviour
{
    private CharacterController thePlayerCharacter = null;
    [SerializeField]
    private bool isPortalOpen = false;
    [SerializeField]
    private string transitionScene;

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
        thePlayerCharacter = GameObject.FindObjectOfType<CharacterController>();
    }
    public void OpenPortal()
    {

    }

    public void ClosePortal()
    {

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
            SceneManager.LoadScene(newScene, LoadSceneMode.Single);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            if(thePlayerCharacter != null)
            {

            }
        }
    }
}
