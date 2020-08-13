using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// may be bugged if scale is not 1

public class EnemyNavMesh : MonoBehaviour
{
    public Vector3[] patrolPoints;
    public AudioClip alertSound;
    public AudioClip playerCaptured;

    // vision will be a cone of length viewRange and angle viewAngle

    public float viewRange;
    public float viewAngle;
    public float moveSpeed;
    public float attackRange;

    public float stunTime;
    public float searchTime;

    public NavMeshAgent agent;

    // 1=+z, 2=+x, 3=-z, 4=-x;
    float[] lookCycle = {0, 170, 0, 190, 0}; 

    PlayerBehavior pb;
    Animator anim;

    // OLD: 0=stationed, 1=chasing, 2=chase reorient, 3=searching, 4=returning, 5=stunned;
    // 
    // NEW: 0=stationed, 1=chasing, 2=attacking, 3=searching, 4=patrolling, 5=stunned;
    int state;

    int currentLook = 0;
    int currentPoint = 0;
    float counter = 0;

    // vertices of the view triangle relative to current position, counterclockwise
    // Vector2[] triangle = {Vector2.zero, Vector2.zero, Vector2.zero};
    Vector3 lastPos;
    //float angle;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = patrolPoints[0];
        //angle = (180 - viewAngle) / 2 * Mathf.Deg2Rad;
        currentLook = 0;
        counter = 0;

        //triangle[0] = new Vector2(0, 0);
        //triangle[1] = new Vector2(viewRange / Mathf.Tan(angle), viewRange);
        //triangle[2] = new Vector2(-1 * viewRange / Mathf.Tan(angle), viewRange);

        pb = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        
    }

    // Update is called once per frame
    void Update()
    {
        AIUpdate();
        Debug.Log(state);
    }

    void AIUpdate()
    {
        if (state == 0)
        {
            Stationed();
            anim.SetInteger("animState", -1);
        }
        else if (state == 1)
        {
            Chase();
            //anim.SetInteger("animState", 1);
        }
        else if (state == 2)
        {
            Attack();
            //anim.SetInteger("animState", 1);
        }
        else if (state == 3)
        {
            Search();
            anim.SetInteger("animState", 1);
        }
        else if (state == 4)
        {
            Patrol();
            anim.SetInteger("animState", 1);
        }
        else if (state == 5)
        {
            Stunned();
            anim.SetInteger("animState", 0);
        }
    }

    void Stationed()
    {
        agent.speed = 0;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0, lookCycle[currentLook], 0)), Time.deltaTime * 120);
        if (Mathf.Abs(transform.rotation.eulerAngles.y - lookCycle[currentLook]) < 10)
        {
            currentLook ++;
            if (currentLook >= lookCycle.Length)
            {
                currentLook = 0;
                if (patrolPoints.Length != 1)
                {
                    agent.speed = moveSpeed;
                    state = 4;
                    currentPoint = (currentPoint + 1) % patrolPoints.Length;
                }
            }

            // will eventually need a second counter for lerping between rotations

            
        }
        if (CanSeePlayer())
        {
            AudioSource.PlayClipAtPoint(alertSound, transform.position);
            agent.speed = moveSpeed;
            state = 1;
            counter = 0; 
        }
    }

    float sign (Vector2 p1, Vector2 p2, Vector2 p3)
    {
        return (p1.x - p3.x) * (p2.y - p3.y) - (p2.x - p3.x) * (p1.y - p3.y);
    }

    bool CanSeePlayer()
    {
        Vector3 targetPoint = pb.chars[PlayerBehavior.activeChar].transform.position;
        Vector3 posDifference = targetPoint - transform.position - 0.5f * Vector3.up;
        
        //Vector3 pointConvert = transform.InverseTransformPoint(targetPoint);

        if (posDifference.magnitude > viewRange)
        {
            return false;
        }
        
        if (Vector3.Angle(posDifference, transform.forward) <= viewAngle / 2)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + 0.5f * Vector3.up, (posDifference).normalized, out hit)) 
            {
                if (hit.collider.name == pb.chars[PlayerBehavior.activeChar].name)
                {
                    lastPos = new Vector3(targetPoint.x, transform.position.y, targetPoint.z);
                    agent.SetDestination(lastPos);
                    counter = 0;
                    return true;
                }
            }
        }
        return false;
    }

    // checks if a relative point is in the relative look triangle
    
    /*
    bool PointInTriangle (Vector2 point)
    {
        float d1, d2, d3;
        bool has_neg, has_pos;

        d1 = sign(point, triangle[0], triangle[1]);
        d2 = sign(point, triangle[1], triangle[2]);
        d3 = sign(point, triangle[2], triangle[0]);

        has_neg = (d1 < 0) || (d2 < 0) || (d3 < 0);
        has_pos = (d1 > 0) || (d2 > 0) || (d3 > 0);

        return !(has_neg && has_pos);
    }
    */

    void Chase()
    {
        if (CanSeePlayer()) 
        {
            Vector3 targetPoint = pb.chars[PlayerBehavior.activeChar].transform.position;
            Vector3 posDifference = targetPoint - transform.position;
            if (posDifference.magnitude < attackRange)
            {
                counter = 0;
                state = 2;
            }
        }
        else
        {
            counter = 0;
            state = 3;
        }
    }

    /* 
    void ChaseReorient()
    {   
        transform.LookAt(lastPos);
        transform.position = Vector3.MoveTowards(transform.position, lastPos, moveSpeed);
        if ((transform.position - lastPos).magnitude < 0.01f)
        {
            state = 3;
        }
        if (CanSeePlayer())
        {
            state = 1;
        }
    }
    */
    void Attack()
    {

    }

    void Search()
    {
        // come up with a nicer looking search later
        transform.LookAt(lastPos);
        counter += Time.deltaTime;
        if (counter > searchTime || (transform.position - lastPos).magnitude < 0.1f)
        {
            state = 0;
        }
        if (CanSeePlayer())
        {
            //AudioSource.PlayClipAtPoint(alertSound, transform.position);
            state = 1;
        }

    }

    void Patrol()
    {
        agent.SetDestination(patrolPoints[currentPoint]);
        if (CanSeePlayer())
        {
            AudioSource.PlayClipAtPoint(alertSound, transform.position);
            state = 1;
        }
        if ((transform.position - patrolPoints[currentPoint]).magnitude < 0.5f)
        {
            state = 0;
        }

    }

    void Stunned()
    {
        counter += Time.deltaTime;
        if (counter > stunTime)
        {
            state = 3;
        }
    }

    public void Stun()
    {
        if (state != 2 & state != 3)
        {
            counter = 0;
            state = 5;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(playerCaptured, transform.position);
            FindObjectOfType<LevelManager>().LevelLost();
            Debug.Log("Player captured, level over");
        }
    }
}
