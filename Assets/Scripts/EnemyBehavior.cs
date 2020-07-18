using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Transform target;
    public float attackRange = 0;
    public int attackDamage = 1;
    public float timeBetweenAttacks = 2f;

    public Transform[] patrolPoint;

    public float visionRange = 50;
    int pointNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        if(target == null) {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        bool playerInTerritory = false;
        if(distance < visionRange) {
            playerInTerritory = true;
        }

        if(playerInTerritory) {
            MoveToPlayer();
        }
        else {
            Patrol();
        }
    }

    void Patrol() {
        Transform targetPoint = patrolPoint[pointNum];

        float step = moveSpeed * Time.deltaTime;
        float distance = Vector3.Distance(transform.position, targetPoint.position);
        if(distance > 0) {
            transform.LookAt(targetPoint);
            transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, step);
        }
        else {
            pointNum++;
            pointNum %= patrolPoint.Length;
        }
    }

    void MoveToPlayer() {
        float step = moveSpeed * Time.deltaTime;
        float distance = Vector3.Distance(transform.position, target.position);
        if(distance > attackRange) {
            transform.LookAt(target);
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
        else {
            Attack();
        }
    }
    void Attack() {

    }
}
