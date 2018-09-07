﻿
//This was testing it doesn't work :)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    
    public GameObject PauseObjects;
    public bool Unpaused;
    // Use this for initialization
    void Start()
    {

        // PauseObjects = GameObject.FindGameObjectsWithTag("Paused");
        PauseObjects = GameObject.Find("PauseMenu");
        HidePaused();
        Unpaused = true;
    }

    // Update is called once per frame
    void Update()
    {
    
        //uses the P button to pause and unpause the game
        if (Input.GetButtonDown("Pause"))
        {
            PauseControl();
        }
    }
    
    public void PauseControl()
    {
         if (Unpaused)
            {
               
                Time.timeScale = 0;
                Unpaused = false;
                ShowPaused();
                
            }
            else if (!Unpaused)
            {
                
                Time.timeScale = 1;
                Unpaused = true;
                HidePaused();
            }
    }
    
    //shows objects with ShowOnPause tag
    public void ShowPaused()
    {
        PauseObjects.SetActive(true);
    }

    //hides objects with ShowOnPause tag
    public void HidePaused()
    {

        PauseObjects.SetActive(false);
    }
}
