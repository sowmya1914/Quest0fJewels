using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ScoreSystemControl : MonoBehaviour
{
    private int levelScore = 0;
    private int totalRunScore = 0;
    [SerializeField]
    private Canvas scoreCanvas = null;
    [SerializeField]
    private Text scoreText = null;
    [SerializeField]
    private Image scorePanel = null;
    [SerializeField]
    private bool inStartMenu = true;

    public int LEVEL_SCORE
    {
        get { return levelScore; }
        set { levelScore = value; }
    }

    public int TOTAL_SCORE
    {
        get { return totalRunScore; }
        set { totalRunScore = value; }
    }

    public static ScoreSystemControl Instance
    {
        get
        {
            if (instance != null)
                return instance;
            instance = FindObjectOfType<ScoreSystemControl>();
            if (instance != null)
                return instance;

            Create();
            return instance;
        }
    }

    protected static ScoreSystemControl instance;
    protected static bool quitting;

    public static ScoreSystemControl Create()
    {
        bool foundInScene = true;
   
        instance = GameObject.FindObjectOfType<ScoreSystemControl>();
        if (instance == null)
        {
            GameObject dataManagerGameObject = new GameObject("ScoreSystemControl");
            DontDestroyOnLoad(dataManagerGameObject);
            instance = dataManagerGameObject.AddComponent<ScoreSystemControl>();
            foundInScene = false;
        }
        if (instance.scoreCanvas == null)
        {
            instance.scoreCanvas = instance.GetComponentInChildren<Canvas>();
        }
        if (instance.scorePanel == null)
        {
            instance.scorePanel = instance.GetComponentInChildren<Image>();
        }

        if (instance.scoreText == null)
        {
            instance.scoreText = instance.GetComponentInChildren<Text>();
        }

        if (instance.scoreCanvas == null)
            instance.scoreCanvas = new GameObject().AddComponent<Canvas>();
        if (instance.scorePanel == null)
            instance.scorePanel = new GameObject().AddComponent<Image>();
        if (instance.scoreText == null)
            instance.scoreText = new GameObject().AddComponent<Text>();

        if (SceneManager.GetActiveScene().name == "Start")
        {
            instance.inStartMenu = true;
        }
        else
        {
            instance.inStartMenu = false;
        }
     
        if (instance.scoreCanvas != null && instance.scoreText != null && foundInScene == false)
        {
            //set name to easily identify in Unity
            instance.scoreCanvas.name = "Score Canvas";
            instance.scorePanel.name = "Score Panel";
            instance.scoreText.name = "Score Text";

            //Sets this as parent so they are never destroyed between scenes
            instance.scoreCanvas.transform.SetParent(instance.transform);
            instance.scoreCanvas.transform.localScale = Vector3.one;
            instance.scorePanel.transform.SetParent(Instance.scoreCanvas.transform);
            instance.scorePanel.transform.localScale = Vector3.one;
            instance.scoreText.transform.SetParent(Instance.scoreCanvas.transform);
            instance.scoreText.transform.localScale = Vector3.one;

            //Sets up additional things needed for functionality
            instance.scoreCanvas.renderMode = RenderMode.ScreenSpaceOverlay;

            CanvasScaler scoreCanvasScaler = instance.scoreCanvas.gameObject.AddComponent<CanvasScaler>();
            scoreCanvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

            GraphicRaycaster raycaster = instance.scoreCanvas.gameObject.AddComponent<GraphicRaycaster>();
            Font potentialFont = Resources.GetBuiltinResource<Font>("Arial.ttf");
            if (potentialFont != null)
            {
                instance.scoreText.font = potentialFont;
            }

            //set colors
            instance.scorePanel.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd");
            instance.scorePanel.color = new Color(0,0,0, instance.inStartMenu? 0:1);
            instance.scorePanel.type = Image.Type.Sliced;
            instance.scoreText.color = Color.white;
            // set the position

            instance.scoreText.resizeTextForBestFit = true;
            instance.scoreText.rectTransform.anchorMin = new Vector2(0.0125f, 0.7f);
            instance.scoreText.rectTransform.anchorMax = new Vector2(0.28f, 0.9f);
            instance.scoreText.rectTransform.anchoredPosition = new Vector2(108f, -4.0f);
            instance.scoreText.rectTransform.sizeDelta = new Vector2(236.0f, -9.0f);

            instance.scorePanel.rectTransform.anchorMin = new Vector2(0.0f, 0.0f);
            instance.scorePanel.rectTransform.anchorMax = new Vector2(1.0f, 1.0f);
            instance.scorePanel.rectTransform.anchoredPosition = new Vector2(-287.5f, 148.5f);
            instance.scorePanel.rectTransform.sizeDelta = new Vector2(-575.0f, -403.0f);


            //Check if scene changed, dont want to show score on title screen
            SceneManager.activeSceneChanged += ActiveSceneWasChanged;
        }
        return instance;
    }

    public static void IncreaseScore(int value)
    {
        Instance.levelScore += value;
    }

    public static void DecreaseScore(int value)
    {
        Instance.levelScore -= value;
    }

    public static void ResetScore()
    {
        Instance.levelScore = 0;
    }

    public static void AddTotalScore()
    {
        Instance.totalRunScore += Instance.levelScore;
        Instance.levelScore = 0;
    }

    public void Setup()
    {

    }
    private static void ActiveSceneWasChanged(Scene current, Scene next)
    {
        string currentName = current.name;

        if (currentName == null)
        {
            // Scene1 has been removed
            currentName = "Replaced";
        }

        if (next.name != null)
        {
            if (next.name != "Start")
            {
                Instance.inStartMenu = false;
                Instance.scorePanel.color = Color.black;
            }
            else
            {
                Instance.inStartMenu = true;
                Instance.scorePanel.color = new Color(0,0,0,0);
            }
        }
    }

    public void TryAddToHighScore()
    {
        if (PlayerPrefs.HasKey("leaderboard"))
        {
            int previousRun = PlayerPrefs.GetInt("leaderboard");
            if (totalRunScore > previousRun)
            {
                PlayerPrefs.SetInt("leaderboard", totalRunScore);
            }
        }
        else
        {
            PlayerPrefs.SetInt("leaderboard", totalRunScore);
        }
    }
    private void Start()
    {
        Create();
    }
    private void Update()
    {
        if (Instance.inStartMenu == false)
        {
            if ( Instance.scoreText != null)
            {
                Instance.scoreText.text = "Score: " + (totalRunScore + levelScore);
            }
        }
    }

}
