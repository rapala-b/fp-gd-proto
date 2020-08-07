using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehavior : MonoBehaviour
{
    public float rotationSpeed = 100;
    public float amplitude = 0.25f;
    public float frequency = 0.5f;
    public AudioClip keyPickupSFX;
    public AudioClip cageUnlockedSFX;
    public GameObject cagedOpenPrefab;

    Vector3 tempPosition = new Vector3();
    Vector3 offset = new Vector3();
    LevelManager LevelManager;

    // Start is called before the first frame update
    void Start()
    {
        LevelManager.totalKeys++;
        offset = transform.position;
        LevelManager = GameObject.FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        animateKey();
        //LevelManager.freeCat();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LevelManager.keysCollected++;
            LevelManager.freeCat();
            AudioSource.PlayClipAtPoint(keyPickupSFX, Camera.main.transform.position);
            gameObject.GetComponent<Renderer>().enabled = false;
            Destroy(gameObject, 0.5f);
        }
    }

    void animateKey()
    {
        // rotate object around y axis
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);
        // move object on y axis between two points
        tempPosition = offset;
        tempPosition.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPosition;
    }
}
