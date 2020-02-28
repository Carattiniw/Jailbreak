using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardDetector : MonoBehaviour
{
    private ExperimentNavMesh ExperimentNavMesh;
    private pauseMenu pauseMenu;
    public GameObject Guard;
    public GameObject Canvas;

    void Start()
    {
        ExperimentNavMesh = Guard.GetComponent<ExperimentNavMesh>();
        pauseMenu = Canvas.GetComponent<pauseMenu>();
    }
    void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log ("I see you bitch!");
            ExperimentNavMesh.getPlayer();  //tells ExperimentNavMesh to set player as target
            pauseMenu.stopPause();
            pauseMenu.playerCaught();
        }
    }
}
