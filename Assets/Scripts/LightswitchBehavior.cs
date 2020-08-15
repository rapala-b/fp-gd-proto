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
        foreach (Light light in lights)
        {
            if (light.tag != "Respawn")
            light.intensity = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOnLights()
    {
        foreach (Light light in lights)
        {
            light.intensity = 1;
        }
    }
}
