using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageBehavior : MonoBehaviour
{
    public GameObject cageUnlockedPrefab;
    public AudioClip cageUnlockedSFX;
    public AudioClip cageLockedSFX;
    public float cageLockedDuration = 5f;

    float lastStart = 0f;

    private void Start() {
        lastStart = 0f;
    }
    private void Update() {
        lastStart += Time.deltaTime;
    }

    public void unlockCage() {
            if (LevelManager.isCatFreed)
            {
                AudioSource.PlayClipAtPoint(cageUnlockedSFX, Camera.main.transform.position);
                Vector3 cagePosition = gameObject.transform.position;
                Quaternion cageRotation = gameObject.transform.rotation;
                Vector3 cageScale = gameObject.transform.localScale;
                Destroy(gameObject);
                GameObject unlocked = Instantiate(cageUnlockedPrefab, cagePosition, cageRotation);

                unlocked.transform.localScale = cageScale;
            }
            else
            {
                if (lastStart > cageLockedDuration)
                {
                    AudioSource.PlayClipAtPoint(cageLockedSFX, Camera.main.transform.position);
                    lastStart = 0f;
                }
            }
    }
}
