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
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1")) {
            Attack();
        }
    }
    void Attack() {
        RaycastHit hit;
        
        if(Physics.Raycast(transform.position, catEye.forward, out hit, attackRange)) {
            if(hit.transform.gameObject.GetComponent<EnemyStationary>() != null) {
                hit.transform.gameObject.GetComponent<EnemyStationary>().Stun();
            }
        }
    }
}
