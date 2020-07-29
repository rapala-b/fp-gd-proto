using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Vector3 relativePlacement = new Vector3(0, 0.3f, -1);
    public Transform focus;
    public float minVertical = -60;
    public float maxVertical = 20;
    public float senseHorizontal = 1;
    public float senseVertical = 0.5f;
    public static float horizontal = 0;
    public static float vertical = 0;
    

    Vector3 relativePos;



    // Start is called before the first frame update
    void Start()
    {
        focus = GameObject.FindGameObjectWithTag("Player").transform;
        horizontal = 0;
        vertical = 0;
        relativePos = relativePlacement; 
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {   
        float lookVertical = Input.GetAxis("Mouse Y");

        vertical -= lookVertical * senseVertical;
        if (vertical < minVertical) { vertical = minVertical; }
        if (vertical > maxVertical) { vertical = maxVertical; }
       
        
        float lookHorizontal = Input.GetAxis("Mouse X");
        horizontal = (horizontal + lookHorizontal * senseHorizontal) % 360;

        //Debug.Log(lookHorizontal + " " + horizontal + " " + lookVertical + " " + vertical);
        
        Quaternion rotation = Quaternion.Euler(0, horizontal, 0);

        relativePos = rotation * relativePlacement; 
        
        Vector3 playerPos = focus.transform.position;
        this.transform.position = playerPos + relativePos;

        transform.LookAt(focus);

        Vector3 currentRota = transform.rotation.eulerAngles;

        transform.rotation = Quaternion.Euler(vertical, currentRota.y, currentRota.z);

    }
}
