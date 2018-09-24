using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillScreen : MonoBehaviour {

    public GameObject KillScreenObject;
    public Slider HealthBar;
    int MinHealthValue = 1; //According to the kill box we die at one health
  
    // Use this for initialization
	void Start ()
    {
        KillScreenObject = GameObject.Find("Kill Screen");
        HealthBar = GameObject.Find("Health").GetComponent<Slider>();
        HideKillScreen();
    }

	// Update is called once per frame
	void Update ()
    {
        KillScreenControl(); //Don't know if this is needed, had to leave before i could test without it
    }

    //checks to see if PC has health left or not
    public void KillScreenControl()
    {
        if (HealthBar.value > MinHealthValue) // Not dead
        {

            Time.timeScale = 1; // Keeps time running
            HideKillScreen(); 

        }
        else if (HealthBar.value == MinHealthValue) // Dead
        {

            Time.timeScale = 0; // Stops time
            ShowKillScreen();
        }
    }

    //shows KillScreen
    public void ShowKillScreen()
    {
        KillScreenObject.SetActive(true);
    }

    //hides KillScreen
    public void HideKillScreen()
    {

        KillScreenObject.SetActive(false);
    }
}
