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
            if (SceneManager.GetActiveScene().name == "First Floor")
            {
                if (!LevelManager.isCatFreed)
                {
                    Debug.Log("We have to free our Sir Wiskers if we want to go there...");
                }
                else
                {
                    levelManager.LevelBeat();
                }
            }
            else
            {
                levelManager.LevelBeat();
            }
        }
    }
}
