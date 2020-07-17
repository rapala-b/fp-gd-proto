using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform focus;
    public float minDistance = 10;
    public float rangeDistance = 5;
    public float minVertical = 10;
    public float verticalRange = 60;
    public float senseHorizontal = 1;
    public float senseVertical = 0.5f;
    public static float horizontal = 0;
    public static float vertical = 0;
    public static float distance = 0;

    Vector3 relativePos;



    // Start is called before the first frame update
    void Start()
    {
        focus = GameObject.FindGameObjectWithTag("Player").transform;
        horizontal = 0;
        vertical = 0;
        relativePos = (0.2f * rangeDistance + minDistance) * Vector3.back; 
    }

    void Update()
    {   
        float lookVertical = Input.GetAxis("Mouse Y");

        vertical += lookVertical * senseVertical;
        if (vertical < minVertical) { vertical = minVertical; }
        if (vertical > minVertical + verticalRange) { vertical = minVertical + verticalRange; }

        distance = (vertical - minVertical) / verticalRange * rangeDistance + minDistance;       
        
        float lookHorizontal = Input.GetAxis("Mouse X");
        horizontal = (horizontal + lookHorizontal * senseHorizontal) % 360;

        //Debug.Log(lookHorizontal + " " + horizontal + " " + lookVertical + " " + vertical);
        
        Quaternion rotation = Quaternion.Euler(vertical, horizontal, 0);

        relativePos = rotation * Vector3.back * distance;  
        
        Vector3 playerPos = focus.transform.position;
        this.transform.position = playerPos + relativePos;

        transform.LookAt(focus);

        Cursor.lockState = CursorLockMode.Locked;

    }
}
