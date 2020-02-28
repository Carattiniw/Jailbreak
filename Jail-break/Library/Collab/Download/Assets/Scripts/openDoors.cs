using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        closedCoordinates = gameObject.transform.position;
        
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
            Debug.Log("Player pressed E");
            direction = -1;
        }
        else
        {
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
