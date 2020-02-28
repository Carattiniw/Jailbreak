using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{
    public Rigidbody playerRB;
    public float speed = 2f;
    public float rotationSpeed = 15f;
    public float cameraSmoothing = 10.0f;

    //public float horizontalSpeed = 2.0f;
    //public float verticalSpeed = 2.0f;

    public Camera playerCamera;
    
    private AudioSource audioSource;
    public AudioClip walkingSound;
    public AudioClip jumpSound;

    float rotationX;
    float rotationY;
    float rotationZ;

    // Jump Script - mW 2/3/20
        // Variables

    public bool isGrounded;
    public float jumpHeight = 30.0f;

    // Crouch Script - mW 2/3/20
        // Variables

    /*

    private bool isCrouched;
    private CapsuleCollider playerCollider;    // Reference to Player's collider
    public float crouchedHeight;               // The Height of the Capsule Collider when Crouched
    public Vector3 crouchedCenter;             // The Center of the Capsule Collider, When Crouched
    public Vector3 crouchedCamHeight;          // The Camera Height of the Player, when Crouched

    private float originalHeight;
    private Vector3 originalCenter;
    private Vector3 originalCamHeight;

    private float tempSpeed;
    */

    // Variables for Raycasting
    // mW - 2/10/20

    public int interactableLayer = 8;
    public openDoors openDoors;
    public TextMeshProUGUI interactText;

    // Start is called before the first frame update
    void Start()
    {
        interactText.enabled = false;
        playerRB = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        //playerCollider = GetComponent<CapsuleCollider>();

        //originalHeight = playerCollider.height;
        //originalCenter = playerCollider.center;
        //originalCamHeight = playerCamera.transform.localPosition;
    }

    // Jump Script - mW 2/3/20
        // Collision Checks

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == ("Ground") && isGrounded == false)
        {
            isGrounded = true;
        }
    }

    // Crouch Script - mW 2/3/20
    // CrouchFunc function
    /*
    void CrouchFunc()
    {
        if (isCrouched == true)
        {
            tempSpeed = speed;
            speed = speed - (speed * 0.30f);
            playerCollider.center = crouchedCenter;
            playerCollider.height = crouchedHeight;
            playerCamera.transform.localPosition = crouchedCamHeight;
            Debug.Log("you are crouched!");
        }
        else
        {
            speed = tempSpeed;
            playerCollider.center = originalCenter;
            playerCollider.height = originalHeight;
            playerCamera.transform.localPosition = originalCamHeight;
            Debug.Log("you've stopped crouching!");
        }

    }
    */

    // Update is called once per frame

    void FixedUpdate()
    {
        /*
        float moveForward = Input.GetAxis("Vertical");
        Vector3 Fmovement = new Vector3(0, 0, moveForward);
        playerRB.AddRelativeForce(Fmovement * speed);

        float moveHorziontal = Input.GetAxis("Horizontal");
        Vector3 Hmovement = new Vector3(moveHorziontal, 0, 0);
        playerRB.AddRelativeForce(Hmovement * speed);
        */

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        movement = movement.normalized * Time.deltaTime * speed;
        playerRB.AddRelativeForce(movement);

        //playerRB.AddRelativeForce(moveHorziontal * horizontalSpeed, 0, moveForward * speed);

        //if (Input.GetKeyDown("space") && isGrounded)
        if (Input.GetKeyDown("space") && isGrounded || Input.GetButtonDown("Jump") && isGrounded)
        {
            audioSource.PlayOneShot(jumpSound, 0.6F);
            Vector2 jump = new Vector2(0, jumpHeight);
            playerRB.AddForce(jump);
            isGrounded = false;

        }


        if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
        {
            audioSource.PlayOneShot(walkingSound, 0.4F);
        }

        /*
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouched = !isCrouched;
            CrouchFunc();
            
        }
        */

        // Raycast for interacting with objects
        // mW 2-10-20
        // int layerMask = 1 << 8;

        //Vector3 fwdView = transform.TransformDirection(Vector3.forward);

        //Debug.Log(LayerMask.LayerToName(layerMask));

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 2.5f))
        {
            
            //Debug.Log("Did Hit");
            if (hit.collider.CompareTag("Interactable"))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 2.5f, Color.yellow);
                interactText.enabled = true;
                // Text Reference
                if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Fire1"))
                {                    
                    //openDoors.OnUse();
                    openDoors = hit.collider.GetComponentInParent<openDoors>();
                    openDoors.OnUse();
                    
                   
                }
            }
           
        }
        else
        {
            interactText.enabled = false;
        }

        GameObject.FindWithTag("Player").transform.rotation = Quaternion.Euler(0, rotationY, 0);

    }





    void Update()
    {
        rotationX -= Input.GetAxis("Mouse Y") * Time.deltaTime * rotationSpeed;
        rotationY += Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed;
        
        rotationX -= Input.GetAxis("Xbox Right Stick X") * Time.deltaTime * rotationSpeed;
        rotationY += Input.GetAxis("Xbox Right Stick Y") * Time.deltaTime * rotationSpeed;
        

        
        if (rotationX < -90)
        {
            rotationX = -90;
        }
        else if (rotationX > 90)
        {
            rotationX = 90;
        }


        //transform.rotation = Quaternion.Euler(0, rotationY, 0);
        
        GameObject.FindWithTag("MainCamera").transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        //GameObject.FindWithTag("Player").transform.rotation = Quaternion.Euler(0, rotationY, 0);
      

        //Debug.Log(rotationSpeed);
    }
    

   
}
