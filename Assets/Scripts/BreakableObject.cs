using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pet1") {
            if (Input.GetKeyDown("e"))
            {
                GameObject.Destroy(gameObject);
            }
        }
    }
}
