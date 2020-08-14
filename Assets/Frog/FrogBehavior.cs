using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogBehavior : MonoBehaviour
{

    public Transform tonguePosition;
    Animator anim;
    CharacterController controller;

    float timer = 0f;
    float attackSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerBehavior.activeChar == 3)
        {
            if (controller.velocity.magnitude > 0.01)
            {
                anim.SetInteger("state", 1);
            }
            else
            {
                anim.SetInteger("state", 0);
            }

            if (Input.GetKeyDown("e"))
            {
                if (timer >= attackSpeed)
                {
                    Debug.Log("tongue");
                    Tongue();
                    anim.SetInteger("state", 2);
                    timer = 0f;
                }
            }
            timer += Time.deltaTime;
        }
    }

    void Tongue() {
        RaycastHit hit;

        if(Physics.Raycast(tonguePosition.position, tonguePosition.forward, out hit, 2f)) {
            if(hit.rigidbody != null) {
                hit.rigidbody.AddForce((transform.position - hit.transform.position)  * 200);
            }
        }
        
    }

    private void OnDrawGizmos() {
        Vector3 frontRayPoint = tonguePosition.position + tonguePosition.forward * 2f;
        Debug.DrawLine(tonguePosition.position, frontRayPoint, Color.cyan);
    }
    
}
