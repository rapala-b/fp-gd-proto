using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantBehavior : MonoBehaviour
{
    public Transform waterSprayPoint;

    public GameObject waterParticle;

    float sprayCountDown = 0.1f;
    float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetButton("Fire1")) {
            if(timer >= sprayCountDown) {
                GameObject water = Instantiate(waterParticle, waterSprayPoint.position, waterParticle.transform.rotation) as GameObject;
                Destroy(water, 3f);
                timer = 0f;
            }
        }
        timer += Time.deltaTime;
        
    }
}
