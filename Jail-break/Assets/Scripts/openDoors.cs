using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class openDoors : MonoBehaviour
{
    public Vector3 closedCoordinates;
    public Vector3 openCoordinates;
    public bool isOpen=false;

    // Lerp for Door Movement
    // mW - 2/12/20

    public float speed = 0.5f;
    private float open = 0;
    private float direction = 0;

    private AudioSource audioSource;
    public AudioClip openDoorSound;

    // Start is called before the first frame update
    void Start()
    {
        closedCoordinates = gameObject.transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        open += direction * speed * Time.deltaTime;
        open = Mathf.Clamp01(open);

        transform.position = Vector3.Lerp(closedCoordinates, openCoordinates, open);
    }
 

    public void OnUse()
    {
        if (direction > 0)
        {
            audioSource.PlayOneShot(openDoorSound, 0.2F);
            Debug.Log("Player pressed E");
            direction = -1;
        }
        else
        {
            audioSource.PlayOneShot(openDoorSound, 0.2F);
            Debug.Log("Player pressed E");
            direction = 1;
        }
    }

    // Old code
    /*
         if (isOpen == false)
        {
            Debug.Log("Player pressed E");
            isOpen = true;
            gameObject.transform.position = openCoordinates;
        }
        else if (isOpen == true)
        {
            Debug.Log("Player pressed E");
            isOpen = false;
            gameObject.transform.position = closedCoordinates;
        }
      */
}
