using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBeatIconBehavior : MonoBehaviour
{
    public float rotationSpeed = 100f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);
    }
}
