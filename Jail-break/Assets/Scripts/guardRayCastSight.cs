using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guardRayCastSight : MonoBehaviour
{
    private ExperimentNavMesh ExperimentNavMesh;
    private GuardPatrolScript GuardPatrolScript;
    private pauseMenu pauseMenu;
    public GameObject Guard;
    public GameObject Canvas;
    public GameObject player;
    public Transform sightStart, sightEnd;

    Ray guardRay;
    public Color rayColor;
    RaycastHit rayHit;
    bool chase = false;
    bool resumeLoop = false;

    void Start()
    {
        ExperimentNavMesh = Guard.GetComponent<ExperimentNavMesh>();
        GuardPatrolScript = Guard.GetComponent<GuardPatrolScript>();
        pauseMenu = Canvas.GetComponent<pauseMenu>();
    }


    void Update()
    {
        //guardRay = new Ray(transform.position, transform.forward*10);
        Debug.DrawRay(transform.position, transform.forward*10, rayColor);

        //if (Physics.Raycast(transform.position, transform.forward, 2))
        /*
        if (Physics.Raycast(sightStart.position, sightEnd.forward, 1 << LayerMask.NameToLayer("Player")))
        {
            spotted = true;
            Debug.Log ("Gotcha!");
        }

        if (spotted == true)
        {
            Debug.Log ("Comming for ya!");
            ExperimentNavMesh.getPlayer();
        }
        */

        /*
        spotted = Physics.Raycast(sightStart.position, sightEnd.forward, 1 << LayerMask.NameToLayer("Player"));

        if (spotted == true)
        {
            Debug.Log ("I can smell ya!");
        }
        */

        RaycastHit hit;

        //if (Physics.Linecast(transform.position, player.transform.position, out hit))
        if (Physics.Linecast(transform.position, sightEnd.position, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                chase = true;
                Debug.Log ("I can smell ya!");
                ExperimentNavMesh.getPlayer(); //stop patrol and chase player
                GuardPatrolScript.guardChase();
            }
            else
            {
                if(resumeLoop == false)
                {
                    return;
                }
                chase = false; //want guard to stop chasing when player is out of sight
                GuardPatrolScript.guardResume(); //guard resume patrol
            }
        }
    }
    void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log ("I see you!");
            ExperimentNavMesh.getPlayer();  //tells ExperimentNavMesh to set player as target
            pauseMenu.stopPause();
            pauseMenu.playerCaught();
        }
    }

    public void stopLoop()
    {
        resumeLoop = false;
    }

    public void goLoop()
    {
        resumeLoop = true;
    }
}
