using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class PlayerState : MonoBehaviour {
    Dictionary<string, string> saveDic;

    public int greenSlimeMeter { get; protected set; }
    public int redSlimeMeter { get; protected set; }
    public int blueSlimeMeter { get; protected set; }
    public Slider greenMeter;
    public Slider redMeter;
    public Slider blueMeter;
    int maxSlime = 100;

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
        TextAsset PlayerFile = new TextAsset();
        if(true)//CHECK TO SEE IF THE SAVE FILE HAS BEEN LOADED
        {

        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
