using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPipeBehavior : MonoBehaviour
{

    public GameObject waterSource;
    public AudioClip pipeBreakSFX;

    // Start is called before the first frame update
    void Start()
    {
        waterSource.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Check if collider is elephant
        if (collision.gameObject.CompareTag("Pet1"))
        {
            GameObject water = GameObject.FindGameObjectWithTag("WaterSource");
            AudioSource.PlayClipAtPoint(pipeBreakSFX, Camera.main.transform.position);
            water.SetActive(true);
        }
    }

    public void BreakPipe()
    {
        waterSource.SetActive(true);
    }
}
