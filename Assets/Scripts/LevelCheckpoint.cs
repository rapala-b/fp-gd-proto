using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCheckpoint : MonoBehaviour
{
    LevelManager levelManager;
    private void Start() {
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            switch (LevelManager.currentLevel)
            {
                case 0:
                    // main menu, do nothing
                    break;
                case 1:
                    LevelOne();
                    break;
                case 2:
                    LevelTwo();
                    break;
                case 3:
                    LevelThree();
                    break;
            }

            // if (LevelManager.currentLevel == 1)
            // {
            //     if (LevelManager.keysCollected != LevelManager.totalKeys)
            //     {
            //         Debug.Log("I need my Froggie and Kittie before I go down there...);
            //     }
            //     else
            //     {
            //         levelManager.LevelBeat();
            //     }
            // }
            // else
            // {
            //     levelManager.LevelBeat();
            // }
        }
    }

    private void LevelOne()
    {
        Debug.Log("Level 1 beat");
        levelManager.LevelBeat();
    }

    private void LevelTwo()
    {
        if (KeysAllCollected())
        {
            if (AllAnimalsFreed())
                levelManager.LevelBeat();
            else
                LevelTwoFreeAnimalsMessage();
        }
        else
            LevelTwoGetKeysMessage();
    }

    private bool KeysAllCollected()
    {
        return LevelManager.keysCollected == LevelManager.totalKeys;
    }

    private bool AllAnimalsFreed()
    {
        return LevelManager.isCatFreed && LevelManager.isFrogFreed;
    }

    private void LevelTwoFreeAnimalsMessage()
    {
        // if neither are freed
        if (!LevelManager.isCatFreed && !LevelManager.isFrogFreed)
            LevelTwoGetKeysMessage();
        else if (LevelManager.isCatFreed && !LevelManager.isFrogFreed)
            Debug.Log("Kittie is freed but we still need to help Froggie!");
        else if (!LevelManager.isCatFreed && LevelManager.isFrogFreed)
            Debug.Log("Froggie is freed but we still need to help Kittie!");
    }

    private void LevelTwoGetKeysMessage()
    {
        Debug.Log("I need to help Froggie and Kittie if I want to go down there!");
    }

    private void LevelThree()
    {
        Debug.Log("Level Three Beat");
        levelManager.LevelBeat();
    }
}
