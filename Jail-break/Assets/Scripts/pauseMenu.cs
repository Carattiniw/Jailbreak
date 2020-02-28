using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class pauseMenu : MonoBehaviour
{
    public string mainMenuScene;
    public string newGameScene;
    public GameObject pauseScreen;
    public GameObject gameOverScreen;
    public GameObject winScreen;
    public GameObject keyScreen;
    public GameObject Camera;
    public bool isPaused;
    public EventSystem ES;
    public GameObject firstObject; //put restart button from game over screen here
    public GameObject secondObject; //put restart button from win screen here
    //public Canvas RestartButton;
    
    private GameObject StoreSelected;
    private headBobber headBobber;
    private bool preventPause;

    // Start is called before the first frame update
    void Start()
    {
        StoreSelected = ES.firstSelectedGameObject;
        preventPause = false;
        headBobber = Camera.GetComponent<headBobber>();
        Cursor.lockState = CursorLockMode.Locked; //keeps the mouse from moving about the screen.
    }

    // Update is called once per frame
    void Update()
    {
        if (ES.currentSelectedGameObject != StoreSelected) 
        {
            //this code stops a bug where if you accidentally click a mouse button on the pause menu while 
            //using the controller, you would be unable to use the controller to exit the pause menu.
            if (ES.currentSelectedGameObject == null)
            {
                ES.SetSelectedGameObject(StoreSelected);
            }
            else
            {
                StoreSelected = ES.currentSelectedGameObject;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Cancel")) //press the ESC key to pause the game
        {
            //found bug where the head bobbing will NOT resume if player mouse clicks resume!!!
            // Fixed bug; headBobber.resume(); was not being called when clicking 'Resume'

            if (preventPause == true)
            {
                return;
            }
            if (isPaused)
            {
                Cursor.lockState = CursorLockMode.Locked;
                resumeGame();
            }
            else
            {
                pauseGame();
            }
        }
    }

    public void pauseGame()
    {
        isPaused = true;
        Cursor.lockState = CursorLockMode.None; //reveals mouse on screen.
        pauseScreen.SetActive(true);
        Time.timeScale = 0f; //actually causes the game to stop
        headBobber.stopBobbing();
    }

    public void resumeGame()
    {
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked; //keeps the mouse from moving about the screen.
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
        headBobber.resumeBob();
    }

    public void restartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(newGameScene);
    }

    public void returnToMain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(firstObject, null);//selects the restart button by default
    }

    public void playerCaught()
    {
        //pauseGame();
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f; //actually causes the game to stop
        headBobber.stopBobbing();
        gameOverScreen.SetActive(true);
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(firstObject, null);//selects the restart button by default
    }

    public void playerWin()
    {
        Time.timeScale = 0f; //actually causes the game to stop
        headBobber.stopBobbing();
        winScreen.SetActive(true);
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(secondObject, null);//selects the restart button by default
    }

    public void gotKey()
    {
        keyScreen.SetActive(true);
    }

    public void stopPause()
    {
        preventPause = true; //stops player from being able to pause when game over.
    }
}
