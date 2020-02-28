using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour 
{
    private pauseMenu pauseMenu;
    private dangerScreenFader dangerScreenFader;
    public GameObject Canvas;
    public GameObject UIFaderScript;

    NavMeshAgent agent;
    [SerializeField] float decisionDelay = 3f;
    [SerializeField] Transform objectToChase;
    [SerializeField] Transform[] waypoints;

    int currentWaypoint = 0;

    enum EnemyStates
    {
        Patrolling,
        Chasing
    }

    [SerializeField] EnemyStates currentState;

    void Start () 
    {
        pauseMenu = Canvas.GetComponent<pauseMenu>();
        dangerScreenFader = Canvas.GetComponent<dangerScreenFader>();
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("SetDestination", 0.5f, decisionDelay);
        if(currentState == EnemyStates.Patrolling) agent.SetDestination(waypoints[currentWaypoint].position);
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, objectToChase.position) > 14f)
        {
            currentState = EnemyStates.Patrolling;
            Debug.Log ("Back to work");
            dangerScreenFader.stopFade();
        }
        else
        {
            currentState = EnemyStates.Chasing;
            Debug.Log ("I coming for you!");
            dangerScreenFader.startFade();
        }

        if(currentState == EnemyStates.Patrolling)
        {
            if(Vector3.Distance(transform.position, waypoints[currentWaypoint].position) <= 0.6f)
            {
                currentWaypoint++;
                if (currentWaypoint == waypoints.Length)
                {
                    currentWaypoint = 0;
                }                
            }
            agent.SetDestination(waypoints[currentWaypoint].position);
        }
    }

    void SetDestination() {
        if(currentState ==  EnemyStates.Chasing) agent.SetDestination(objectToChase.position);
    }

    //private void OnCollisionEnter(Collision collision)
    void OnTriggerEnter (Collider other)
    {
        //if (collision.transform.name == "Player")
        if (other.tag == "Player")
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log ("I got you!");
            //dangerScreenFader.startFade();
            //ExperimentNavMesh.getPlayer();  //tells ExperimentNavMesh to set player as target
            pauseMenu.stopPause();
            pauseMenu.playerCaught();
        }

        //if (collision.transform.name != "Player" && currentState == EnemyStates.Chasing)
        if ( other.tag == "Player" && currentState == EnemyStates.Chasing)
        {
            currentState = EnemyStates.Patrolling;
            currentWaypoint = 0;
            agent.SetDestination(waypoints[currentWaypoint].position);
            dangerScreenFader.stopFade();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 7f);
    }
}
