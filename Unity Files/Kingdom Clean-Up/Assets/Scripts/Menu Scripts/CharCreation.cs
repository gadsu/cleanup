
//   Char Creation
//   This is the script for the Character Creation scene, will control the movement and making those choices once we've moved scenes

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharCreation : MonoBehaviour {

    GameData gd;

    // Use this for initialization
    void Start()
    {
        gd = GameObject.Find("DontDestroyOnLoad").GetComponent<GameData>();
        GameObject.Find("Player Num").GetComponent<Text>().text = "Player " + gd.saveFileNum;
    }
    public void saveFile() 
    {
        if (GameObject.Find("InputField").GetComponent<InputField>().text.Replace(" ", "").Length > 2)
            gd.SaveFile();
    }
	// Update is called once per frame
    public void changeName()
    {
        string name = GameObject.Find("InputField").GetComponent<InputField>().text;
        gd.gamedic[gd.saveFileNum + "playerName"] = name;
        Debug.Log("Changed name to " + name);
    }
	void Update () {
		
	}
}
