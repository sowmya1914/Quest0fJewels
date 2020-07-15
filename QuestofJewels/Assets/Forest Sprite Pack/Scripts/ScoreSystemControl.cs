using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ScoreSystemControl : MonoBehaviour
{
    private static int levelScore = 0;
    private static int totalRunScore = 0;
    private static Canvas scoreCanvas = null;
    private static Text scoreText = null;
    private static bool inStartMenu = true;

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
        if (scoreCanvas == null)
            scoreCanvas = new GameObject().AddComponent<Canvas>();
        if (scoreText == null)
            scoreText = new GameObject().AddComponent<Text>();


        GameObject dataManagerGameObject = new GameObject("ScoreSystemControl");
        DontDestroyOnLoad(dataManagerGameObject);
        instance = dataManagerGameObject.AddComponent<ScoreSystemControl>();
        if (scoreCanvas != null && scoreText != null)
        {
            //set name to easily identify in Unity
            scoreCanvas.name = "Score Canvas";
            scoreText.name = "Score Text";

            //Sets this as parent so they are never destroyed between scenes
            scoreCanvas.transform.SetParent(instance.transform);
            scoreCanvas.transform.localScale = Vector3.one;
            scoreText.transform.SetParent(scoreCanvas.transform);
            scoreText.transform.localScale = Vector3.one;

            //Sets up additional things needed for functionality
            scoreCanvas.renderMode = RenderMode.ScreenSpaceOverlay;

            CanvasScaler scoreCanvasScaler = scoreCanvas.gameObject.AddComponent<CanvasScaler>();
            scoreCanvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

            GraphicRaycaster raycaster = scoreCanvas.gameObject.AddComponent<GraphicRaycaster>();
            Font potentialFont = Resources.GetBuiltinResource<Font>("Arial.ttf");
            if (potentialFont != null)
            {
                scoreText.font = potentialFont;
            }

            // set the text position
            scoreText.resizeTextForBestFit = true;
            scoreText.rectTransform.anchorMin = new Vector2(0.0125f, 0.7f);
            scoreText.rectTransform.anchorMax = new Vector2(0.28f, 0.9f);
            scoreText.rectTransform.anchoredPosition = new Vector2(108f, -4.0f);
            scoreText.rectTransform.sizeDelta = new Vector2(236.0f, -9.0f);

            //Check if scene changed, dont want to show score on title screen
            SceneManager.activeSceneChanged += ActiveSceneWasChanged;
        }
        return instance;
    }

    public static void IncreaseScore(int value)
    {
        levelScore += value;
    }

    public static void DecreaseScore(int value)
    {
        levelScore -= value;
    }

    public static void ResetScore()
    {
        levelScore = 0;
    }

    public static void AddTotalScore()
    {
        totalRunScore += levelScore;
        levelScore = 0;
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
                inStartMenu = false;
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

    private void Update()
    {
        if (inStartMenu == false)
        {
            if (scoreText != null)
            {
                scoreText.text = "Score: " + (totalRunScore + levelScore);
            }
        }
    }

}
