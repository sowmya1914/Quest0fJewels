using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Gamekit2D;
using UnityEngine.SceneManagement;

public class GameSettings : MonoBehaviour
{
    public AudioMixer audioMixer;
    Slider sMaster;
    Slider sSfx;
    Slider sBGM;
    Slider sVO;
    Slider sAmbient;

    Button KB_Melee;
    Button KB_Shoot;
    Button KB_Interact;

    Resolution[] resolutions;
    Dropdown resolutionDropdown;
    Dropdown qualityDropdown;
    Toggle FS;

    GameObject currentkey;
    Dictionary<string, KeyCode> Keybinds;

    void Start()
    {
        ///Init
        resolutionDropdown = transform.Find("Options/Graphics Panel/Dropdown").GetComponent<Dropdown>();
        qualityDropdown = transform.Find("Options/Graphics Panel/QualityDropdown").GetComponent<Dropdown>();

        sMaster = transform.Find("Options/Sound Panel/Master").gameObject.GetComponent<Slider>();
        sSfx = transform.Find("Options/Sound Panel/SFx").gameObject.GetComponent<Slider>();
        sBGM = transform.Find("Options/Sound Panel/BGM").gameObject.GetComponent<Slider>();
        sVO = transform.Find("Options/Sound Panel/VO").gameObject.GetComponent<Slider>();
        sAmbient = transform.Find("Options/Sound Panel/Ambient").gameObject.GetComponent<Slider>();

        KB_Melee = transform.Find("Options/KeyBinding Panel/Melee").gameObject.GetComponent<Button>();
        KB_Shoot = transform.Find("Options/KeyBinding Panel/Shoot").gameObject.GetComponent<Button>();
        KB_Interact = transform.Find("Options/KeyBinding Panel/Interact").gameObject.GetComponent<Button>();

        FS = transform.Find("Options/Graphics Panel/FullScreen").GetComponent<Toggle>();
        Keybinds = new Dictionary<string, KeyCode>();

        ///Audio
        SetAudio();

        ///Resolution
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        FS.isOn = Screen.fullScreen;

        ///Quality
        string[] names = QualitySettings.names;
        qualityDropdown.ClearOptions();
        options.Clear();
        for (int i = 0; i < names.Length; i++)
        {
            string option = names[i];
            options.Add(option);
        }
        qualityDropdown.AddOptions(options);
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        qualityDropdown.RefreshShownValue();



        ///KeyBind
        //KeybindsSetup();
    }

    private void OnGUI()
    {
        //if (currentkey != null)
        //{
        //    Event e = Event.current;
        //    if (e.isKey)
        //    {
        //        ChangeTheKey(currentkey.name, e.keyCode);
        //    }
        //}
    }
    
    #region Audio
    public void SetMasterVolume(float volume)
    {
        PlayerPrefs.SetFloat("Master_Vol", volume);
        sMaster.value = volume;
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20f);
    }
    public void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("BGM_Vol", volume);
        sBGM.value = volume;
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20f);
    }
    public void SetSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat("SFx_Vol", volume);
        sSfx.value = volume;
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20f);
    }
    public void SetAmbientVolume(float volume)
    {
        PlayerPrefs.SetFloat("Amb_Vol", volume);
        sAmbient.value = volume;
        audioMixer.SetFloat("AmbientVolume", Mathf.Log10(volume) * 20f);
    }
    public void SetVOVolume(float volume)
    {
        PlayerPrefs.SetFloat("VO_Vol", volume);
        sVO.value = volume;
        audioMixer.SetFloat("VOVolume", Mathf.Log10(volume) * 20f);
    }

    void SetAudio()
    {
        if (PlayerPrefs.HasKey("Master_Vol"))
        {
            SetMasterVolume(PlayerPrefs.GetFloat("Master_Vol"));
        }

        if (PlayerPrefs.HasKey("BGM_Vol"))
        {
            SetMusicVolume(PlayerPrefs.GetFloat("BGM_Vol"));
        }

        if (PlayerPrefs.HasKey("SFx_Vol"))
        {
            SetSFXVolume(PlayerPrefs.GetFloat("SFx_Vol"));
        }

        if (PlayerPrefs.HasKey("Amb_Vol"))
        {
            SetAmbientVolume(PlayerPrefs.GetFloat("Amb_Vol"));
        }

        if (PlayerPrefs.HasKey("VO_Vol"))
        {
            SetVOVolume(PlayerPrefs.GetFloat("VO_Vol"));
        }
    }

    public void SetAudioDefault()
    {
        float value = 0.8f;
        SetMasterVolume(value);
        SetMusicVolume(value);
        SetSFXVolume(value);
        SetAmbientVolume(value);
        SetVOVolume(value);
    }
    #endregion
    #region Graphics
    public void SetResolution(int resolutionIndex)
    {
        Resolution _resolution = resolutions[resolutionIndex];
        Screen.SetResolution(_resolution.width, _resolution.height, Screen.fullScreen);
    }

    public void SetFullScreen(bool b)
    {
        Screen.fullScreen = b;
    }

    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index, true);
    }
    #endregion
    #region KeyBinding
    public void ChangeKey(GameObject go)
    {
        currentkey = go;
    }

    void ChangeTheKey(string name, KeyCode k)
    {
        if (Keybinds.ContainsValue(k))
        {
            currentkey = null;
            return;
        }
        SetKey(name, k);
        currentkey.transform.Find("Text").GetComponent<Text>().text = k.ToString().ToUpper();
        currentkey = null;
    }

    void SetKey(string name, KeyCode k)
    {
        switch (name)
        {
            case "Melee":
                Gamekit2D.PlayerInput.Instance.MeleeAttack.key = k;
                PlayerPrefs.SetInt("KB_M", (int)k);
                break;
            case "Shoot":
                Debug.Log("yolo");
                Gamekit2D.PlayerInput.Instance.RangedAttack.key = k;
                PlayerPrefs.SetInt("KB_S", (int)k);
                break;
            case "Interact":
                Gamekit2D.PlayerInput.Instance.Interact.key = k;
                PlayerPrefs.SetInt("KB_I", (int)k);
                break;
            default:
                break;
        }
        Keybinds[name] = k;
    }

    public void SetKeyDefault()
    {
        Gamekit2D.PlayerInput.Instance.MeleeAttack.key = KeyCode.K;
        KB_Melee.transform.Find("Text").GetComponent<Text>().text = "K";
        PlayerPrefs.SetInt("KB_M", (int)KeyCode.K);

        Gamekit2D.PlayerInput.Instance.RangedAttack.key = KeyCode.K;
        KB_Shoot.transform.Find("Text").GetComponent<Text>().text = "K";
        PlayerPrefs.SetInt("KB_S", (int)KeyCode.K);

        Gamekit2D.PlayerInput.Instance.Interact.key = KeyCode.E;
        KB_Interact.transform.Find("Text").GetComponent<Text>().text = "E";
        PlayerPrefs.SetInt("KB_I", (int)KeyCode.E);
    }

    void KeybindsSetup()
    {
        Keybinds.Add("Melee", PlayerPrefs.HasKey("KB_M") ? (KeyCode)PlayerPrefs.GetInt("KB_M") : KeyCode.K);
        KB_Melee.transform.Find("Text").GetComponent<Text>().text = Keybinds["Melee"].ToString().ToUpper();
        Gamekit2D.PlayerInput.Instance.MeleeAttack.key = Keybinds["Melee"];

        Keybinds.Add("Shoot", PlayerPrefs.HasKey("KB_S") ? (KeyCode)PlayerPrefs.GetInt("KB_S") : KeyCode.K);
        KB_Shoot.transform.Find("Text").GetComponent<Text>().text = Keybinds["Shoot"].ToString().ToUpper();
        Gamekit2D.PlayerInput.Instance.RangedAttack.key = Keybinds["Shoot"];

        Keybinds.Add("Interact", PlayerPrefs.HasKey("KB_I") ? (KeyCode)PlayerPrefs.GetInt("KB_I") : KeyCode.E);
        KB_Interact.transform.Find("Text").GetComponent<Text>().text = Keybinds["Interact"].ToString().ToUpper();
        Gamekit2D.PlayerInput.Instance.Interact.key = Keybinds["Interact"];
    }
    #endregion


    public void StartNewGame()
    {
        SceneManager.LoadScene("DragonsCutscene");
    }

    public void StartContinueGame()
    {

    }
    public void BackToMain()
    {
        Debug.Log("Going to Main");
        //TODO
        SceneManager.LoadScene("MenuScene");

    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void GoToScene(string sceneName)
    {
        Debug.Log("Going to " + sceneName);
        SceneManager.LoadScene(sceneName);
    }
    public void ExitPause()
    {
        PlayerCharacter.PlayerInstance.Unpause();
        Cursor.visible = true;
        FindObjectOfType<InventoryController>().Clear();
        PersistentDataManager.ClearSave();
    }
}
