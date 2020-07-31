using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Transform target;
    public float attackRange = 0;
    public float chaseDistance = 3;
    public int attackDamage = 1;
    public float timeBetweenAttacks = 2f;

    public Transform[] patrolPoints;
    int patrolPointIndex = 0;

    Vector3 nextDestination;
    float distanceToPlayer;
    Animator anim;
    BehaviorState currentState;

    public enum BehaviorState
    {
        Idle,
        Patrol,
        Chase,
        Attack
    }

    public float visionRange = 50;

    // Start is called before the first frame update
    void Start()
    {
        if(target == null) {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        anim = GetComponent<Animator>();
        currentState = BehaviorState.Patrol;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, target.position);

        switch(currentState)
        {
            case BehaviorState.Patrol:
                Patrol();
                break;
            case BehaviorState.Chase:
                MoveToPlayer();
                break;
            case BehaviorState.Attack:
                Attack();
                break;
        }
    }

    void Patrol() {

        //Update Animation State
        anim.SetInteger("animState", 1);

        if (Vector3.Distance(transform.position, nextDestination) <= 1)
        {
            FindNextPoint();
        }
        else if (distanceToPlayer <= chaseDistance)
        {
            currentState = BehaviorState.Chase;
        }

        FaceTarget(nextDestination);
        transform.position = Vector3.MoveTowards(transform.position, nextDestination, moveSpeed * Time.deltaTime);
    }

    void MoveToPlayer() {

        anim.SetInteger("animState", 2);
        nextDestination = target.transform.position;

        if (distanceToPlayer <= attackRange)
        {
            currentState = BehaviorState.Attack;
        }
        else if (distanceToPlayer > chaseDistance)
        {
            currentState = BehaviorState.Patrol;
        }

        FaceTarget(nextDestination);
        transform.position = Vector3.MoveTowards(transform.position, nextDestination, moveSpeed * Time.deltaTime);
    }
    void Attack() {

    }

    void FindNextPoint()
    {
        nextDestination = patrolPoints[patrolPointIndex].transform.position;

        patrolPointIndex = (patrolPointIndex + 1) % patrolPoints.Length;
    }

    void FaceTarget(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        directionToTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }
}
