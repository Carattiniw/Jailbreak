using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    // Variables and References

    public GameObject playerCharacter;
    public GameObject macGuffin;
    public GameObject guardCharacter;
    public TextMeshProUGUI missionText;


    // Start is called before the first frame update
    void Start()
    {
        missionText.text = "Find the Key!";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void goToExit()
    {
        missionText.text = "Escape!";
    }

}
