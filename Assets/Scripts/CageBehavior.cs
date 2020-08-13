using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageBehavior : MonoBehaviour
{
    public GameObject cageUnlockedPrefab;
    public AudioClip cageUnlockedSFX;

    private void Start() {
    
    }
    private void Update() {

    }

    public void UnlockCage() {
        AudioSource.PlayClipAtPoint(cageUnlockedSFX, Camera.main.transform.position);
        Vector3 cagePosition = gameObject.transform.position;
        Quaternion cageRotation = gameObject.transform.rotation;
        Vector3 cageScale = gameObject.transform.localScale;
        Destroy(gameObject);
        GameObject unlocked = Instantiate(cageUnlockedPrefab, cagePosition, cageRotation);
        unlocked.transform.localScale = cageScale;
    }
}
