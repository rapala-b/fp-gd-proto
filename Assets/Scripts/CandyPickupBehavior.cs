using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyPickupBehavior : MonoBehaviour
{
    public AudioClip candyCollectSFX;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            gameObject.SetActive(false);
            Vector3 candyPosition = transform.position;
            AudioSource.PlayClipAtPoint(candyCollectSFX, candyPosition);
            Destroy(gameObject, 2);
            LevelManager.candiesFoundInLevel++;
        }
    }
}
