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
    public int[] status = {1, -1, -1, -1};
    public int activeChar = 0;
    public GameObject[] chars = {null, null};
    public CharPanel charPanel;

    PlayerCamera playerCamera;
    

    CharacterController controller;
    Animator anim;
    Vector3 moveVector;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        anim.SetInteger("animState", 0);
        activeChar = 0;
        playerCamera = Camera.main.GetComponent<PlayerCamera>();
        chars[0] = gameObject;
        chars[1] = GameObject.FindGameObjectWithTag("Pet1");
    }

     void Update()
    {
        // only toggles between 0 and 1 for now
        if (Input.GetKeyDown("f") & status[charPanel.target] != -1) 
        { 
            SwitchControl(charPanel.target); 
        }
        if (Input.GetKeyDown("r") & charPanel.target != 0) 
        { 
            Recall(charPanel.target); 
        }

        float moveHorizontal = Input.GetAxis("Horizontal") * Time.deltaTime;
        float moveVertical = Input.GetAxis("Vertical") * Time.deltaTime;

        Vector3 lateralMove = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized * speed;
        lateralMove = Quaternion.Euler(0, PlayerCamera.horizontal, 0) * lateralMove;
        
        if (CheckGrounded()) {
            moveVector = lateralMove;
           

            if (Input.GetButton("Jump"))
            {

                moveVector.y = Mathf.Sqrt(2 * jumpHeight * gravity);
                anim.SetInteger("animState", 2);
            }
            else { 
                moveVector.y = 0.0f;
                if (moveVector == Vector3.zero)
                {
                    anim.SetInteger("animState", 0);
                }
                else
                {
                    anim.SetInteger("animState", 1);
                }
            }
        }
        else 
        {
            // Player is in air
            moveVector.x = lateralMove.x * airspeed / speed;
            moveVector.z = lateralMove.z * airspeed / speed;
            anim.SetInteger("animState", 3);
        }
        moveVector.y -= gravity * Time.deltaTime;

        if (Mathf.Abs(moveVector.x) + Mathf.Abs(moveVector.z) > 0.1f){
            controller.transform.LookAt(new Vector3(controller.transform.position.x + moveVector.x, controller.transform.position.y, controller.transform.position.z + moveVector.z));
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
        //charPanel.UpdatePanels();
    }

    // currently can recall an individual stuffed animal
    void Recall(int target)
    {
        /*
        if (activeChar == 0)
        {
            for (int i = 1; i < status.Length; i++ )
            {
                if (status[i] == 1)
                {
                    chars[i].SetActive(false);
                    status[i] = 0;
                }
            }
        }
        */
        
        if (activeChar == 0)
        {
            chars[target].SetActive(false);
            status[target] = 0;
        }

        //charPanel.UpdatePanels();
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

        //charPanel.UpdatePanels();
    }

    bool CheckGrounded()
    {//Judge whether Junkochan is on the ground or not
        Ray ray = new Ray(this.transform.position + Vector3.up * 0.05f, Vector3.down * 0.1f);//Shoot ray at 0.05f upper from Junkochan's feet position to the ground with its length of 0.1f
        return Physics.Raycast(ray, 0.1f);//If the ray hit the ground, return true
    }
}
