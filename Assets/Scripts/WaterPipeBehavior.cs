using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPipeBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
            water.SetActive(true);
        }
    }
}
