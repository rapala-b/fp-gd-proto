using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantBehavior : MonoBehaviour
{
    public Transform waterSprayPoint;

    public GameObject waterParticle;

    float sprayCountDown = 0.5f;
    float timer = 0f;

    int shotNum = 3;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetButton("Fire1")) {
            if(timer >= sprayCountDown && shotNum > 0) {
                GameObject water = Instantiate(waterParticle, waterSprayPoint.position, transform.rotation) as GameObject;
                Destroy(water, 3f);
                timer = 0f;
                shotNum--;
            }
        }
        timer += Time.deltaTime;
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.gameObject.CompareTag("Box")) {
            Destroy(hit.gameObject);
        }
    }
}
