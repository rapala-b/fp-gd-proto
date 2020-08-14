using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBehavior : MonoBehaviour
{
    public float attackRange = 0.5f;
    public Transform catEye;
    Animator anim;
    CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        anim.SetInteger("animState", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerBehavior.activeChar == 2) {

            if (Input.GetKeyDown("e"))
            {
                Attack();
            }

            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                anim.SetInteger("animState", 1);
            }
            else if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !anim.IsInTransition(0)) {
                anim.SetInteger("animState", 0);
            }
        }
    }
    void Attack() {
       
        Debug.Log("Cat attack");
        anim.StopPlayback();
        anim.SetInteger("animState", 2);

        foreach (RaycastHit hit in (Physics.SphereCastAll(transform.position + transform.forward, 1, transform.forward))) {
            if(hit.transform.gameObject.GetComponent<EnemyNavMesh>() != null) {
                Debug.Log("Enemy Hit by cat");
                hit.transform.gameObject.GetComponent<EnemyNavMesh>().Stun();
            }
        }
    }
}
