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
    public float levelLoadDuration = 2;

    int candiesCount;

    // Gamewide:
    public static int candiesFoundCount = 0;
    public static bool isGameOver = false;

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

    }

    void InitializeLevel()
    {
        isGameOver = false;
        keysCollected = 0;
        isCatFreed = false;
        candiesCount = CandyPickupBehavior.candiesCount;
        //SetCandyCounterText();
    }

    public void LevelLost()
    {
        isGameOver = true;
        gameText.text = "Game Over...";
        gameText.gameObject.SetActive(true);

        AudioSource.PlayClipAtPoint(levelLostSFX, Camera.main.transform.position);

        Invoke("LoadCurrentLevel", levelLoadDuration);
    }

    public void LevelBeat()
    {
        isGameOver = true;
        gameText.text = "You Won!";
        gameText.gameObject.SetActive(true);

        AudioSource.PlayClipAtPoint(levelBeatSFX, Camera.main.transform.position);

        Invoke("LoadNextLevel", levelLoadDuration);
    }

    void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

    void SetCandyCounterText()
    {
        candyCountText.text = "Candies: " + candiesFoundCount.ToString() + "/" 
            + candiesCount.ToString();
    }

    public void freeCat()
    {
        if (keysCollected == totalKeys)
        {
            isCatFreed = true;
        }
    }
}
