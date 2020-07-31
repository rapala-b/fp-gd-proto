using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCheckpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            print("Level beat, entered checkpoint area");
            FindObjectOfType<LevelManager>().LevelBeat();
        }
    }
}
