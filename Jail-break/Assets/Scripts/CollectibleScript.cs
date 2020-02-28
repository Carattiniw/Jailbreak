using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CollectibleScript : MonoBehaviour
{

    public winGame winGame;
    public GameController GameController;
    public pauseMenu pauseMenu;

    private AudioSource audioSource;
    public AudioClip pickUpSound;
    //public GameObject Key;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        GameObject winGameObject = GameObject.FindWithTag("Key");
        if (winGameObject != null)
        {
            winGame = winGameObject.GetComponent<winGame>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.PlayOneShot(pickUpSound, 0.9F);
            Debug.Log("Got the key!");
            GameController.goToExit();
            winGame.theWin();
            pauseMenu.gotKey();
            Destroy(gameObject);
        }
        //else
            //return;
    }
}
