using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class mainMenu : MonoBehaviour
{
    public string newGameScene;
    public EventSystem ES;
    private GameObject StoreSelected;

    void Start()
    {
        StoreSelected = ES.firstSelectedGameObject;
        //preventPause = false;
        //headBobber = Camera.GetComponent<headBobber>();
        //Cursor.lockState = CursorLockMode.Locked; //keeps the mouse from moving about the screen.
    }

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
    }

    public void newGame()
    {
        SceneManager.LoadScene(newGameScene);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
