using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public AudioClip levelBeatSFX;
    public AudioClip levelLostSFX;
    public string nextLevel;
    public Text gameText;
    public Text candyCountText;
    public Text cageButtonText;
    public float levelLoadDuration = 2;

    //int candiesCount;

    // Gamewide:
    public static bool isGameOver = false;
    public static int currentLevel = 1;
    int totalCandiesFound = 0;

    // Current Level Variables
    public static int candiesFoundInLevel = 0;

    // Level2 (possibly rewrite Level1 to reuse this)
    public static int keysCollected = 0;
    public static int totalKeys;
    public static bool isCatFreed = false;

    // Start is called before the first frame update
    void Start()
    {
        InitializeLevel();
    }

    // Update is called once per frame
    void Update()
    {
        SetCandyCounterText();
        if (currentLevel == 1)
        {
            SetCageButtonText();
        }
    }

    void InitializeLevel()
    {
        isGameOver = false;
        keysCollected = 0;
        isCatFreed = false;
        //candiesCount = CandyPickupBehavior.candiesCount;
        SetCandyCounterText();
    }

    public void LevelLost()
    {
        isGameOver = true;
        gameText.text = "Game Over...";
        gameText.gameObject.SetActive(true);

        //Reset Game Values
        ResetLevelVariables();

        //Reset Level 1 Values
        if (currentLevel == 1)
        {
            CageButton.cageButtonsPressed = 0;
        }


        AudioSource.PlayClipAtPoint(levelLostSFX, Camera.main.transform.position);

        Invoke("LoadCurrentLevel", levelLoadDuration);
    }

    public void LevelBeat()
    {
        isGameOver = true;
        gameText.text = "LEVEL BEAT";
        gameText.gameObject.SetActive(true);

        // Update Variables
        totalCandiesFound += candiesFoundInLevel;
        ResetLevelVariables();

        AudioSource.PlayClipAtPoint(levelBeatSFX, Camera.main.transform.position);

        Invoke("LoadNextLevel", levelLoadDuration);
    }

    void ResetLevelVariables()
    {
        candiesFoundInLevel = 0;
    }

    void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevel);
        currentLevel += 1;
    }

    void SetCandyCounterText()
    {
        candyCountText.text = "Candies: " + candiesFoundInLevel.ToString() + "/" 
            + CandyPickupBehavior.candiesCount.ToString();
    }

    void SetCageButtonText()
    {
        cageButtonText.text = "Cage Buttons Activated: " + CageButton.cageButtonsPressed.ToString() + "/"
            + CageButton.cageButtonCount.ToString();
    }

    public void freeCat()
    {
        if (keysCollected == totalKeys)
        {
            isCatFreed = true;
        }
    }
}
