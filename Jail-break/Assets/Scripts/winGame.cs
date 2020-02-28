using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winGame : MonoBehaviour
{
    private pauseMenu pauseMenu;
    public GameObject Canvas;

    private bool hasKey;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = Canvas.GetComponent<pauseMenu>();
        hasKey = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (hasKey == false)
            {
                return;
            }
            Cursor.lockState = CursorLockMode.None;
            Debug.Log("We won!");
            pauseMenu.stopPause();
            pauseMenu.playerWin();
        }
    }

    public void theWin()
    {
        hasKey = true;
    }
}
