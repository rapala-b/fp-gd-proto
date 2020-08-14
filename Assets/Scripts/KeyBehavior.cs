using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehavior : MonoBehaviour
{
    public float rotationSpeed = 100;
    public float amplitude = 0.25f;
    public float frequency = 0.5f;
    public AudioClip keyPickupSFX;
    public GameObject controlTipPrefab;

    Vector3 tempPosition = new Vector3();
    Vector3 offset = new Vector3();

    public static int numberOfKeysInLevel;
    public static int numberOfKeysCollected = 0;

    public int numberOfKeysToFreeCat = 4;

    bool catFree = false;
    bool frogFree = false;

    // Start is called before the first frame update
    void Start()
    {
        numberOfKeysInLevel++;
        offset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        animateKey();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Pet1") || other.gameObject.CompareTag("Pet2") || other.gameObject.CompareTag("Pet2"))
        {
            numberOfKeysCollected++;
            gameObject.GetComponent<Renderer>().enabled = false;
            Destroy(gameObject, 0.5f);
            AudioSource.PlayClipAtPoint(keyPickupSFX, Camera.main.transform.position);

            if (numberOfKeysCollected == numberOfKeysInLevel && !frogFree)
            {
                UnlockFrogCage();
                GameObject frogTipObj = GameObject.Instantiate(controlTipPrefab, Vector3.forward * 1000, Quaternion.identity);
                ControlTip frogTip = frogTipObj.GetComponent<ControlTip>();
                frogTip.message = "E as Frog: launch tongue";
                frogTip.destroyKey = "e";
            } else if (numberOfKeysCollected == numberOfKeysToFreeCat && !catFree)
            {
                UnlockCatCage();
                GameObject catTipObj = GameObject.Instantiate(controlTipPrefab, Vector3.forward * 1000, Quaternion.identity);
                ControlTip catTip = catTipObj.GetComponent<ControlTip>();
                catTip.message = "E as Cat: disable soldier from behind";
                catTip.destroyKey = "e";
            }
        }
    }

    private void UnlockCatCage()
    {
        Debug.Log("Cat cage unlocked");
        LevelManager.isCatFreed = true;
        PlayerBehavior.status[2] = 1; //Make cat active
        GameObject catCage = GameObject.FindGameObjectWithTag("CatCage");
        catCage.GetComponent<CageBehavior>().UnlockCage();
        GameObject.Destroy(catCage);
    }

    private void UnlockFrogCage()
    {
        Debug.Log("Frog cage unlocked");
        LevelManager.isFrogFreed = true;
        PlayerBehavior.status[3] = 1; //Make frog active
        GameObject frogCage = GameObject.FindGameObjectWithTag("FrogCage");
        frogCage.GetComponent<CageBehavior>().UnlockCage();
        GameObject.Destroy(frogCage);
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
