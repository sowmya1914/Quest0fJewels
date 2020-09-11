using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Gamekit2D;
public class PortalScript : MonoBehaviour, IDataPersister
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
    private SpriteRenderer myRenderer = null;
    private Animator m_animator = null;
    private static Color transparent = new Color(0, 0, 0, 0);

    private PortalActivator pa;

    //Gamekit 2D
    public DataSettings dataSettings;
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

    private void OnEnable()
    {
        thePlayerCharacter = GameObject.FindObjectOfType<PlayerCharacter>();
        m_animator = GetComponent<Animator>();
        myRenderer = GetComponent<SpriteRenderer>();
        if (m_animator != null)
        {
            m_animator.speed = 0;
        }
        if (myRenderer != null)
        {
            myRenderer.color = transparent;
        }
        PersistentDataManager.RegisterPersister(this);
    }

    private void OnDisable()
    {
        PersistentDataManager.UnregisterPersister(this);
    }
    public void OpenPortal()
    {
        if (m_animator != null)
        {
            m_animator.speed = 1;
        }

        if (myRenderer != null)
        {
            myRenderer.color = Color.white;
        }
    }

    public void ClosePortal()
    {
        if (m_animator != null)
        {
            m_animator.speed = 0;
        }
        if (myRenderer != null)
        {
            myRenderer.color = transparent;
        }
    }
    private void Awake()
    {
        if (transitioning == true)
        {
            thePlayerCharacter = GameObject.FindObjectOfType<PlayerCharacter>();
            if (thePlayerCharacter != null)
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
        PersistentDataManager.SetDirty(this);
    }

    private void TransitionTo(string newScene)
    {
        Scene nextScene = SceneManager.GetSceneByName(newScene);
        if (nextScene != null)
        {
            lastDepositLocation = myDepositLocation;
            transitioning = true;
            SceneManager.LoadScene(newScene, LoadSceneMode.Single);
        }
    }

    private void TransitionTo()
    {
        Gamekit2D.TransitionPoint tp = GetComponent<TransitionPoint>();
        tp.Transition();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (thePlayerCharacter != null)
            {
                if (isPortalOpen == true)
                {
                    if (Vector3.Distance(thePlayerCharacter.transform.position, transform.position) < 3)
                    {
                        TransitionTo();
                        //TransitionTo(transitionScene);
                    }
                }
            }
        }
    }

    public void setPA(PortalActivator _pa)
    {
        pa = _pa;
    }

    //////////////////////////////////////////////////////////
    /// ***Gamekit 2D IDataPersister Interface functions*** ///
    public DataSettings GetDataSettings()
    {
        return dataSettings;
    }

    public void SetDataSettings(string dataTag, DataSettings.PersistenceType persistenceType)
    {
        dataSettings.dataTag = dataTag;
        dataSettings.persistenceType = persistenceType;
    }

    public Data SaveData()
    {
        return new Data<bool>(IS_PORTAL_OPEN);
    }

    public void LoadData(Data data)
    {
        Data<bool> d = (Data<bool>)data;
        IS_PORTAL_OPEN = d.value;
        if (pa != null) pa.switching(d.value);
    }
    ///////////////////////////////////////////////////
    

}
