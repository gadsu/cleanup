
//    KillScreen
//    Information that shows and hides a screen upon death

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class KillScreen : MonoBehaviour {

    public GameObject KillScreenObject;
    public Slider HealthBar;
    public Button firstButton;
    int MinHealthValue = 0; //According to the kill box we die at one health
  
    // Use this for initialization
	void Start ()
    {
        //KillScreenObject = GameObject.Find("Kill Screen"); //(Set in editor the code `
        HealthBar = GameObject.Find("Health").GetComponent<Slider>();
        firstButton = KillScreenObject.GetComponentInChildren<Button>();
        HideKillScreen();
    }

	// Update is called once per frame
	void Update ()
    {
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
        firstButton.Select();

    }

    //hides KillScreen
    public void HideKillScreen()
    {

        KillScreenObject.SetActive(false);

    }
}
