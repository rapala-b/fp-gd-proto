using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyPickupBehavior : MonoBehaviour
{
    public AudioClip candyCollectSFX;

    public static int candiesCount;

    // Start is called before the first frame update
    void Start()
    {
        candiesCount++;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            gameObject.SetActive(false);
            Vector3 candyPosition = transform.position;
            AudioSource.PlayClipAtPoint(candyCollectSFX, candyPosition);
            Destroy(gameObject, 2);
            LevelManager.candiesFoundCount++;
        }
    }
}
