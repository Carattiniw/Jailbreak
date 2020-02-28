using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guardFOVSight : MonoBehaviour
{
    private ExperimentNavMesh ExperimentNavMesh;
    private pauseMenu pauseMenu;
    public GameObject Guard;
    public GameObject Canvas;
    public GameObject player;
    public Color rayColor;

    public float fieldOfViewAngle = 110f;
    public bool playerInSight;
    private SphereCollider col;
    //private LastPlayerSighting lastPlayerSighting;


    void Awake()
    {
        //player = GameObject.FindGameObjectWithTag(Tags.player);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward*10, rayColor);
    }

    void Start()
    {
        ExperimentNavMesh = Guard.GetComponent<ExperimentNavMesh>();
        pauseMenu = Canvas.GetComponent<pauseMenu>();
    }

    void OnTriggerStay (Collider other)
    {
        if (other.gameObject == player)
        {
            playerInSight = false;

            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            if (angle < fieldOfViewAngle * 0.5f)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius))
                {
                    if (hit.collider.gameObject == player)
                    {
                        Debug.Log ("FOV works!");
                        playerInSight = true;
                        //lastPlayerSighting.position = playerInSight.transform.position;
                        //ExperimentNavMesh.getPlayer();
                    }
                }
            }
        }
    }

    /*
    void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log ("I see you bitch!");
            //ExperimentNavMesh.getPlayer();  //tells ExperimentNavMesh to set player as target
            pauseMenu.stopPause();
            pauseMenu.playerCaught();
        }
    }
    */
}
