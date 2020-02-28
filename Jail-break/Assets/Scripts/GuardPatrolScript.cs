using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardPatrolScript : MonoBehaviour
{
    //set wait time per nav point
    [SerializeField]
    bool patrolWaiting;

    //max amount of time guard waits at each point
    [SerializeField]
    float totalWaitTime = 2f;

    //sets the probability of the guard switching direction
    [SerializeField]
    float switchProbability = 0.5f;

    //list of all potential nav points on the patrol route
    [SerializeField]
    List <Waypoint> patrolPoints;

    NavMeshAgent navMeshAgent;
    int currentPatrolIndex;
    bool travelling;
    bool waiting;
    bool patrolForward;
    float waitTimer;
    private guardRayCastSight guardRayCastSight;
    public GameObject Guard;

    public void Start()
    {
        guardRayCastSight = Guard.GetComponent<guardRayCastSight>();
        navMeshAgent = this.GetComponent<NavMeshAgent>();

        if (navMeshAgent == null)
        {
            Debug.LogError("Nav mesh agent is not attached to " + gameObject.name);
        }
        else
        {
            if (patrolPoints != null && patrolPoints.Count >= 2)
            {
                currentPatrolIndex = 1; //starts are the first nav point
                SetDestination();
            }
            else
            {
                Debug.Log("Not enough patrol points for basic patrol behaviour");
            }
        }
    }


    public void Update()
    {
        //Are we close to the destination?
        if (travelling && navMeshAgent.remainingDistance <= 1.0f)
        {
            travelling = false;

            if (currentPatrolIndex == 0)
            {
                patrolWaiting = true; //will stop the guard from moving
            }

            //If waiting is true, then make guard wait
            if (patrolWaiting)
            {
                waiting = true;
                waitTimer = 0f; //reset the timer
            }
            else //if not waiting then go ahead and set the next point
            {
                ChangePatrolPoint();
                SetDestination();
            }
        }

        //only executes if waiting is true
        if (waiting)
        {
            waitTimer += Time.deltaTime; 
            if (waitTimer >= totalWaitTime) //increment until greater than public totalWaitTime
            {
                waiting = false;

                ChangePatrolPoint();
                SetDestination();
                patrolWaiting = false; //allows the guard to continue without stopping at every navpoint
                //guardRayCastSight.stopLoop();
            }
        }
    }


    //private void SetDestination()
    public void SetDestination()
    {
        //this directs the guard towards the next nav point
        if (patrolPoints != null)
        {
            Vector3 targetVector = patrolPoints[currentPatrolIndex].transform.position;
            navMeshAgent.SetDestination(targetVector);
            travelling = true;
        }
    }

    public void guardChase()
    {
        patrolWaiting = true;
    }

    public void guardResume()
    {
        waiting = true;
        //ChangePatrolPoint();
        totalWaitTime = 2f;
    }


    //this method will determine whether or not the guard changes direction
    //private void ChangePatrolPoint()
    public void ChangePatrolPoint()
    {
        //this updates to the next nav point once the guard has reached the current point
        patrolForward = true;

        if (patrolForward == true)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;

            /*
            if ( currentPatrolIndex == 3)
            {
                if (UnityEngine.Random.Range(0f, 1f) <= switchProbability)
                {
                    currentPatrolIndex = 4;
                }
                else
                {
                    currentPatrolIndex = 5;
                }
            }
            */
        }

        /*
        else
        {
            if (--currentPatrolIndex < 0)
            {
                currentPatrolIndex = patrolPoints.Count -1;
            }

            currentPatrolIndex--;

            if (currentPatrolIndex <0)
            {
                currentPatrolIndex = patrolPoints.Count - 1;
            }
            
        }
        */
    }
}
