using Gamekit2D;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    [SerializeField]
    private TextAsset saveFile = null;

    private bool useBackup = false;

    public void CreateAsset()
    {
        if (saveFile == null)
        {
            saveFile = Resources.Load("SaveFile") as TextAsset;
        }

        if (saveFile == null)
        {
            useBackup = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        // CreateAsset();
        saveFile = Resources.Load("SaveFile") as TextAsset;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SaveCharacterData(null);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadCharacterData(null);
        }
    }

    public void SaveCharacterData(PlayerCharacter character)
    {
        if (character == null)
        {
            character = GameObject.FindObjectOfType<PlayerCharacter>();
        }
        if (character == null)
        {
            return;
        }

        Vector3 characterPosition = character.transform.position;
        int characterHealth = character.damageable.CurrentHealth;
        int totalScore = ScoreSystemControl.Instance.TOTAL_SCORE;
        int currentScore = ScoreSystemControl.Instance.LEVEL_SCORE;
        string currentSceneName = SceneManager.GetActiveScene().name;
        PlatformCatcher[] movingPlatorms = GameObject.FindObjectsOfType<PlatformCatcher>();
        Pushable[] pushableBlocks = GameObject.FindObjectsOfType<Pushable>();

        string writtenString = " scene," + currentSceneName;
        writtenString += "\n" + " position," + characterPosition.x + "," + characterPosition.y + "," + characterPosition.z;
        writtenString += "\n" + " health," + characterHealth;
        writtenString += "\n" + " totalScore," + totalScore;
        writtenString += "\n" + " levelScore," + currentScore;

        string platformString = "\n platforms, |";
        for (int i = 0; i < movingPlatorms.Length; i++)
        {
            PlatformCatcher platform = movingPlatorms[i];
            Vector3 platformPosition = platform.platformRigidbody.position;
            platformString += platform.GetInstanceID() + " !" + platformPosition.x + "!" + platformPosition.y + "!" + platformPosition.z;
            if (i + 1 < movingPlatorms.Length)
            {
                platformString += "|";
            }
        }

        writtenString += platformString;

        string pushableString = "\n pushables, |";
        for (int i = 0; i < pushableBlocks.Length; i++)
        {
            Pushable someBlock = pushableBlocks[i];
            if (someBlock.GetComponent<Rigidbody2D>())
            {
                Vector3 pushablePosition = someBlock.GetComponent<Rigidbody2D>().position;
                pushableString += someBlock.GetInstanceID() + " !" + pushablePosition.x + "!" + pushablePosition.y + "!" + pushablePosition.z;
                if (i + 1 < pushableBlocks.Length)
                {
                    pushableString += "|";
                }
            }
        }

        writtenString += pushableString;

        if (saveFile != null)
        {
            File.WriteAllText(AssetDatabase.GetAssetPath(saveFile), writtenString);
            EditorUtility.SetDirty(saveFile);

        }
        else
        {
            CreateAsset();
            if (useBackup == true)
            {
                PlayerPrefs.SetString("SavePlayerFile", writtenString);
            }
            else
            {
                File.WriteAllText(AssetDatabase.GetAssetPath(saveFile), writtenString);
            }
        }
    }

    public void LoadCharacterData(PlayerCharacter character)
    {
        if (character == null)
        {
            character = GameObject.FindObjectOfType<PlayerCharacter>();
        }
        if (character == null)
        {
            return;
        }

        if (saveFile != null)
        {

            string textString = File.ReadAllText(AssetDatabase.GetAssetPath(saveFile));
            LoadDataFromText(character, textString);
        }
        else if (PlayerPrefs.HasKey("SavePlayerFile"))
        {
            string textString = PlayerPrefs.GetString("SavePlayerFile");
           LoadDataFromText(character, textString);
        }
    }

    private void LoadDataFromText(PlayerCharacter character, string textString)
    {
        string[] lines = textString.Split('\n');

        Vector3 newPlayerPosition = character.transform.position;
        int playerHealth = character.damageable.CurrentHealth;
        int totalScore = ScoreSystemControl.Instance.TOTAL_SCORE;
        int currentScore = ScoreSystemControl.Instance.LEVEL_SCORE;
        PlatformCatcher[] platforms = GameObject.FindObjectsOfType<PlatformCatcher>();
        Pushable[] pushableBlocks = GameObject.FindObjectsOfType<Pushable>();
        for (int i = 0; i < lines.Length; i++)
        {
            lines[i] = lines[i].Trim();
            string[] theLine = lines[i].Split(',');

            switch (theLine[0])
            {
                case "position":
                    {
                        if (theLine.Length >= 4)
                        {
                            float floatX = newPlayerPosition.x;
                            float floatY = newPlayerPosition.y;
                            float floatZ = newPlayerPosition.z;
                            Single.TryParse(theLine[1], out floatX);
                            Single.TryParse(theLine[2], out floatY);
                            Single.TryParse(theLine[3], out floatZ);
                            newPlayerPosition = new Vector3(floatX, floatY, floatZ);
                        }
                    }
                    break;
                case "platforms":
                    {
                        if (theLine.Length >= 2)
                        {
                            string[] plats = theLine[1].Split('|');

                            if (plats.Length >= 2)
                            {
                                for (int j = 1; j < plats.Length; j++)
                                {
                                    string somePlat = plats[j];
                                    string[] platformData = somePlat.Split('!');
                                    if (platformData.Length >= 4)
                                    {
                                        int someId = 0;
                                        Int32.TryParse(platformData[0], out someId);

                                        for (int k = 0; k < platforms.Length; k++)
                                        {

                                            if (platforms[k].GetInstanceID() == someId)
                                            {
                                                float floatX = 0.0f;
                                                float floatY = 0.0f;
                                                float floatZ = 0.0f;

                                                Single.TryParse(platformData[1], out floatX);
                                                Single.TryParse(platformData[2], out floatY);
                                                Single.TryParse(platformData[3], out floatZ);
                                                platforms[k].platformRigidbody.position = new Vector3(floatX, floatY, floatZ);
                                                break;
                                            }
                                        }


                                    }
                                }
                            }




                        }
                    }
                    break;
                case "pushables":
                    {
                        if (theLine.Length >= 2)
                        {
                            string[] blocks = theLine[1].Split('|');

                            if (blocks.Length >= 2)
                            {
                                for (int j = 1; j < blocks.Length; j++)
                                {
                                    string someBlock = blocks[j];
                                    string[] blockData = someBlock.Split('!');

                                    if (blockData.Length >= 4)
                                    {
                                        int someId = 0;
                                        Int32.TryParse(blockData[0], out someId);

                                        for (int k = 0; k < pushableBlocks.Length; k++)
                                        {

                                            if (pushableBlocks[k].GetInstanceID() == someId)
                                            {
                                                if (pushableBlocks[k].GetComponent<Rigidbody2D>())
                                                {
                                                    float floatX = 0.0f;
                                                    float floatY = 0.0f;
                                                    float floatZ = 0.0f;

                                                    Single.TryParse(blockData[1], out floatX);
                                                    Single.TryParse(blockData[2], out floatY);
                                                    Single.TryParse(blockData[3], out floatZ);
                                                    pushableBlocks[k].GetComponent<Rigidbody2D>().position = new Vector3(floatX, floatY, floatZ);
                                                }
                                                break;
                                            }
                                        }


                                    }
                                }
                            }




                        }
                    }
                    break;
                case "health":
                    {
                        if (theLine.Length >= 2)
                        {
                            Int32.TryParse(theLine[1], out playerHealth);
                        }
                    }
                    break;

                case "totalScore":
                    {
                        if (theLine.Length >= 2)
                        {
                            Int32.TryParse(theLine[1], out totalScore);
                        }
                    }
                    break;

                case "levelScore":
                    {
                        if (theLine.Length >= 2)
                        {
                            Int32.TryParse(theLine[1], out currentScore);
                        }
                    }
                    break;

            }
        }

        character.transform.position = newPlayerPosition;
        character.damageable.SetHealth(playerHealth);
        ScoreSystemControl.Instance.TOTAL_SCORE = totalScore;
        ScoreSystemControl.Instance.LEVEL_SCORE = currentScore;
    }
}
