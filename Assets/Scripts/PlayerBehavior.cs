using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float speed = 10;
    public float airspeed = 10;
    public float jumpHeight = 5;
    public float gravity = 9.8f;
    public int health = 3;
    // animals now get recalled upon damage
    //public int[] health = {3, 3};
    public int[] status = {1, 0};
    public int activeChar = 0;
    public GameObject[] chars = {null, null};
    public bool canSwitch = false;

    PlayerCamera playerCamera;

    CharacterController controller;
    Vector3 moveVector;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        activeChar = 0;
        playerCamera = Camera.main.GetComponent<PlayerCamera>();
        chars[0] = gameObject;
        chars[1] = GameObject.FindGameObjectWithTag("Pet1");
        chars[1].SetActive(false);


    }

     void Update()
    {
        // only toggles between 0 and 1 for now
        if (Input.GetKeyDown("f") & canSwitch) { SwitchControl((activeChar + 1) % 2); }
        if (Input.GetKeyDown("r") & canSwitch) { Recall(); }

        float moveHorizontal = Input.GetAxis("Horizontal") * Time.deltaTime;
        float moveVertical = Input.GetAxis("Vertical") * Time.deltaTime;

        Vector3 lateralMove = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized * speed;
        lateralMove = Quaternion.Euler(0, PlayerCamera.horizontal, 0) * lateralMove;
        
        if (controller.isGrounded) {
            moveVector = lateralMove;
            
            if (Input.GetButton("Jump"))
            {
                moveVector.y = Mathf.Sqrt(2 * jumpHeight * gravity);
            }
            else { moveVector.y = 0.0f; }
        }
        else 
        {
            // Player is in air
            moveVector.x = lateralMove.x * airspeed / speed;
            moveVector.z = lateralMove.z * airspeed / speed;
        }
        moveVector.y -= gravity * Time.deltaTime;

        if (Mathf.Abs(moveVector.x) + Mathf.Abs(moveVector.z) > 0.1f){
            controller.transform.LookAt(new Vector3(transform.position.x + moveVector.x, transform.position.y, transform.position.z + moveVector.z));
        }
            
        controller.Move(moveVector * Time.deltaTime);
    }

    void SwitchControl(int target) 
    {
        if (activeChar != target)
        {
            if (status[target] == 0) 
            { 
                status[target] = 1; 
                chars[target].transform.position = chars[activeChar].transform.position + 0.1f * chars[activeChar].transform.forward;
                chars[target].SetActive(true);
            }
            playerCamera.focus = chars[target].transform;
            activeChar = target;
            controller = chars[target].GetComponent<CharacterController>();
        }
    }

    void Recall()
    {
        if (activeChar == 0)
        {
            for (int i = 1; i < status.Length; i++ )
            {
                chars[i].SetActive(false);
                status[i] = 0;
            }
        }
    }

    void Damage(int target)
    {
        if (target == 0)
        {
            health --;
            if (health <= 0) { 
                FindObjectOfType<LevelManager>().LevelLost();
            }
        }
        else 
        {
            chars[activeChar].SetActive(false);
            activeChar = 0;
        }
    }
}
