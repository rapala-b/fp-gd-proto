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
        if (other.CompareTag("Player") || other.CompareTag("Pet1") || other.CompareTag("Pet2") || other.CompareTag("Pet3")) {
            switch (SceneManager.GetActiveScene().buildIndex)
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
        }
    }

    private void LevelOne()
    {
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
        return KeyBehavior.numberOfKeysCollected == KeyBehavior.numberOfKeysInLevel;
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
        if (GameObject.FindGameObjectsWithTag("Fire") == null)
        {
            levelManager.LevelBeat();
        } else
        {
            Debug.Log("I need to put the fires out!");
        }
    }
}
