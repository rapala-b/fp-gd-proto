using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// may be bugged if scale is not 1

public class EnemyStationary : MonoBehaviour
{
    public Vector3 patrolPoint;

    // vision will be a isosceles triangle with height = viewRange

    public float viewRange;
    public float viewAngle;
    public float viewHeight;
    public float moveSpeed;

    // all time measured in number of ticks of AIUpdate

    public int lookTime;
    public int searchTime;
    

    // 1=+z, 2=+x, 3=-z, 4=-x;
    public float[] lookCycle = {0, 180}; 

    PlayerBehavior pb;

    // 0=stationed, 1=chasing, 2=chase reorient, 3=searching, 4=returning
    int state;

    int currentLook = 0;
    int lookCounter = 0;
    int searchCounter = 0;

    // vertices of the view triangle relative to current position, counterclockwise
    Vector2[] triangle = {Vector2.zero, Vector2.zero, Vector2.zero};
    Vector3 lastPos;
    float angle;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = patrolPoint;
        angle = (180 - viewAngle) / 2 * Mathf.Deg2Rad;
        currentLook = 0;
        searchCounter = 0;
        lookCounter = 0;

        triangle[0] = new Vector2(0, 0);
        triangle[1] = new Vector2(viewRange / Mathf.Tan(angle), viewRange);
        triangle[2] = new Vector2(-1 * viewRange / Mathf.Tan(angle), viewRange);

        pb = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>();

        InvokeRepeating("AIUpdate", 0, 0.02f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AIUpdate()
    {
        if (state == 0)
        {
            Stationed();
        }
        else if (state == 1)
        {
            Chase();
        }
        else if (state == 2)
        {
            ChaseReorient();
        }
        else if (state == 3)
        {
            Search();
        }
        else if (state == 4)
        {
            ReturnToStation();
        }
    }

    void Stationed()
    {
        lookCounter ++;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0, lookCycle[currentLook], 0)), 10);
        if (lookCounter > lookTime)
        {
            lookCounter = 0;
            currentLook = (currentLook + 1) % lookCycle.Length;

            // will eventually need a second counter for lerping between rotations

            
        }
        if (CanSeePlayer())
        {
            state = 1;
            lookCounter = 0; 
        }
    }

    float sign (Vector2 p1, Vector2 p2, Vector2 p3)
    {
        return (p1.x - p3.x) * (p2.y - p3.y) - (p2.x - p3.x) * (p1.y - p3.y);
    }

    bool CanSeePlayer()
    {
        Vector3 targetPoint = pb.chars[pb.activeChar].transform.position;
        Vector3 pointConvert = transform.InverseTransformPoint(targetPoint);

        if (Mathf.Abs(pointConvert.y) > viewHeight)
        {
            return false;
        }
        
        if (PointInTriangle(new Vector2(pointConvert.x, pointConvert.z)))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, (targetPoint - transform.position).normalized, out hit)) 
            {
                if (hit.collider.name == pb.chars[pb.activeChar].name)
                {
                    lastPos = new Vector3(targetPoint.x, transform.position.y, targetPoint.z);
                    searchCounter = 0;
                    return true;
                }
            }
        }
        return false;
    }

    // checks if a relative point is in the relative look triangle
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

    void Chase()
    {
        if (CanSeePlayer()) 
        {
            Vector3 targetPoint = pb.chars[pb.activeChar].transform.position;
            Vector3 targetTemp = new Vector3(targetPoint.x, transform.position.y, targetPoint.z);
            transform.LookAt(targetTemp);
            transform.position = Vector3.MoveTowards(transform.position, targetTemp, moveSpeed);
        }
        else
        {
            state = 2;
        }
    }

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

    void Search()
    {
        // come up with a nicer looking search later
        searchCounter++;
        transform.Rotate(new Vector3(0, 3, 0), Space.Self);
        if (searchCounter > searchTime)
        {
            state = 4;
        }
        if (CanSeePlayer())
        {
            state = 1;
        }

    }

    void ReturnToStation()
    {
        transform.LookAt(patrolPoint);
        transform.position = Vector3.MoveTowards(transform.position, patrolPoint, moveSpeed);
        if (CanSeePlayer())
        {
            state = 1;
        }
        if ((transform.position - patrolPoint).magnitude < 0.5f)
        {
            state = 0;
        }

    }

}
