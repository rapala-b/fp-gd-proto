using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightswitchBehavior : MonoBehaviour
{

    Light[] lights;

    // Start is called before the first frame update
    void Start()
    {
        lights = FindObjectsOfType(typeof(Light)) as Light[];
        Debug.Log("Number of Lights: " + (lights.Length - 1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
