
//    PauseMenu
//    Controls the display of the pausemenu screen and freezing time

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    
    public GameObject PauseObjects;
    public bool Unpaused;
    // Use this for initialization
    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }
    void Start()
    {
        //TESTING FOR LAG
        // PauseObjects = GameObject.FindGameObjectsWithTag("Paused");
        PauseObjects = GameObject.Find("PauseMenu");
        HidePaused();
        Unpaused = true;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
    
        //uses the P button to pause and unpause the game
        if (Input.GetButtonDown("Menu"))
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
            GameObject.Find("Player").GetComponent<PlayerController>().toggleMove();
        }
        else if (!Unpaused)
        {
            Time.timeScale = 1;
            Unpaused = true;
            HidePaused();
            GameObject.Find("Player").GetComponent<PlayerController>().toggleMove();
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

