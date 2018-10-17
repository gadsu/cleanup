﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class PlayerState : MonoBehaviour {
    Dictionary<string, string> saveDic;

    [Header("Debug Variables")]
    [Tooltip("How much green slime?")]
    public int greenSlimeMeter;
    [Tooltip("How much red slime?")]
    public int redSlimeMeter;
    [Tooltip("How much blue slime?")]
    public int blueSlimeMeter;
    [Tooltip("Slider object")]
    public Slider greenMeter;
    [Tooltip("Slider object")]
    public Slider redMeter;
    [Tooltip("Slider object")]
    public Slider blueMeter;


    int maxSlime = 100;

    //public TextAsset PlayerFile; DOES NOT WORK FOR SOME RAISIN

    public void loadData(string playernum)
    {
        TextAsset PlayerFile = new TextAsset();
        PlayerFile = Resources.Load("SaveFile" + playernum) as TextAsset;
    }

    public void addSlime(int val, string type)// Adds slime to the slime meter
    {
        if ((greenSlimeMeter < maxSlime) && type == "green")//Adds green slime to the slime meter
        {
            greenSlimeMeter = Mathf.Clamp(greenSlimeMeter + val, 0, 100);
            greenMeter.value = greenSlimeMeter;
            //Debug.Log("<color=green>GreenSlimeVal:</color> " + greenSlimeMeter);//tells the debug log that green slime was added to the slime meter
        }

        if ((redSlimeMeter < maxSlime) && type == "red")//Adds red slime to the slime meter
        {
            redSlimeMeter = Mathf.Clamp(redSlimeMeter + val, 0, 100);
            redMeter.value = redSlimeMeter;
            //Debug.Log("<color=red>RedSlimeVal:</color> " + redSlimeMeter);//tells the debug log that red slime was added to the slime meter
        }

        if ((blueSlimeMeter < maxSlime) && type == "blue")//Adds blue slime to the slime meter
        {
            blueSlimeMeter = Mathf.Clamp(blueSlimeMeter + val, 0, 100);
            blueMeter.value = blueSlimeMeter;
            //Debug.Log("<color=blue>BlueSlimeVal:</color> " + blueSlimeMeter);//tells the debug log that blue slime was added to the slime meter
        }


    }

	// Use this for initialization
	void Start () {
        Debug.Log("TEST");
        greenMeter = GameObject.Find("GreenMeter").GetComponent<Slider>();
        redMeter = GameObject.Find("RedMeter").GetComponent<Slider>();
        blueMeter = GameObject.Find("BlueMeter").GetComponent<Slider>();

        //Fill the dictionary from file   saveDic["name"] will return the string "Rachel" 
        if(true)//CHECK TO SEE IF THE SAVE FILE HAS BEEN LOADED
        {

        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
