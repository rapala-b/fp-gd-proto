﻿using System.Collections;
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
    public Text keyCountText;
    public float levelLoadDuration = 2;

    //int candiesCount;

    // Gamewide:
    public static bool isGameOver = false;
    public static int currentLevel = 1;
    int totalCandiesFound = 0;

    // Current Level Variables
    public static int candiesFoundInLevel = 0;
    int candiesInCurrentLevel;


    // Level2 (possibly rewrite Level1 to reuse this)
    public static bool isCatFreed = false;
    public static bool isFrogFreed = false;

    // Start is called before the first frame update
    void Start()
    {
        InitializeLevel();
    }

    // Update is called once per frame
    void Update()
    {
        SetCandyCounterText();
        
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SetCageButtonText();
        } else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            SetKeyCounterText();
        }
    }

    void InitializeLevel()
    {
        isGameOver = false;
        //keysCollected = 0;
        //isCatFreed = false;
        //isFrogFreed = false;
        candiesFoundInLevel = 0;
        candiesInCurrentLevel = GameObject.FindGameObjectsWithTag("Candy").Length;
        //currentLevel = SceneManager.GetActiveScene().buildIndex;
        SetCandyCounterText();
    }

    public void LevelLost()
    {
        isGameOver = true;
        gameText.text = "Game Over...";
        gameText.gameObject.SetActive(true);

        //Reset Game Values
        candiesFoundInLevel = 0;
        candiesInCurrentLevel = GameObject.FindGameObjectsWithTag("Candy").Length;

        //Reset Level 1 Values
        if (currentLevel == 1)
        {
            CageButton.cageButtonsPressed = 0;
            PlayerBehavior.status[1] = -1;
        }


        AudioSource.PlayClipAtPoint(levelLostSFX, Camera.main.transform.position);

        Invoke("LoadCurrentLevel", levelLoadDuration);
    }

    public void LevelBeat()
    {
        isGameOver = true;
        gameText.text = "LEVEL BEAT";
        gameText.gameObject.SetActive(true);

        // Update Game Variables
        totalCandiesFound += candiesFoundInLevel;

        AudioSource.PlayClipAtPoint(levelBeatSFX, Camera.main.transform.position);

        Invoke("LoadNextLevel", levelLoadDuration);
    }

    void SetLevelVariables()
    {
        candiesFoundInLevel = 0;
        candiesInCurrentLevel = GameObject.FindGameObjectsWithTag("Candy").Length;

        if (currentLevel == 2)
        {
            PlayerBehavior.status[1] = 1; //Set elephant to be active
        }

        if (currentLevel == 3)
        {
            PlayerBehavior.status[1] = 1; //Set elephant to be active
            PlayerBehavior.status[2] = 1; //Set cat to be active
            PlayerBehavior.status[3] = 1; //Set frog to be active
        }
    }

    void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevel);
        currentLevel += 1;
        SetLevelVariables();
    }

    void SetCandyCounterText()
    {
        candyCountText.text = "Candies: " + candiesFoundInLevel.ToString() + "/" 
            + candiesInCurrentLevel.ToString();
    }

    void SetCageButtonText()
    {
        cageButtonText.text = "Cage Buttons Activated: " + CageButton.cageButtonsPressed.ToString() + "/"
            + CageButton.cageButtonCount.ToString();
    }

    void SetKeyCounterText()
    {
        keyCountText.text = "Keys Collected: " + KeyBehavior.numberOfKeysCollected.ToString() + "/"
            + KeyBehavior.numberOfKeysInLevel.ToString();
    }

    //public void freeCat()
    //{
      //  if (keysCollected == totalKeys)
        //{
          //  isCatFreed = true;
        //}
    //}
}
