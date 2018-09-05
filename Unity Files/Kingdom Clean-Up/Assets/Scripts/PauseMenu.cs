
//This was testing it doesn't work :)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    GameObject[] PauseObjects;
    int Paused = 1;
    // Use this for initialization
    void Start()
    {

        PauseObjects = GameObject.FindGameObjectsWithTag("Paused");
        HidePaused();
    }

    // Update is called once per frame
    void Update()
    {

        //uses the ESC button to pause and unpause the game
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("P was pressed");
            if (Paused == 1)
            {
                Debug.Log("pausing");
                Paused = 0;
                ShowPaused();
            }
            else if (Paused == 0)
            {
                Debug.Log("un-pausing");
                Paused = 1;
                HidePaused();
            }
        }
    }

    //controls the pausing of the scene
    public void PauseControl()
    {
        if (Paused == 1)
        {
            Paused = 0;
            ShowPaused();
        }
        else if (Paused == 0)
        {
            Paused = 1;
            HidePaused();
        }
    }

    //shows objects with ShowOnPause tag
    public void ShowPaused()
    {
        foreach (GameObject g in PauseObjects)
        {
            g.SetActive(true);
        }
    }

    //hides objects with ShowOnPause tag
    public void HidePaused()
    {
        foreach (GameObject g in PauseObjects)
        {
            g.SetActive(false);
        }
    }
}

