
//    GameData
//    This file will be attached to DontDestroyOnLoad and persist through scenes
//    Controls what a character looks like, as well as some other basic information

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;
using System;

public class GameData : MonoBehaviour {
    //player name
//    public Text playerName;
//    public bool newGame;
//    public string Appearance;

    public Dictionary<string, string> gamedic;

    // Use this for initialization
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);  //THIS IS WHAT MAKES THE OBJECT PERSIST

      //  SceneManager.LoadScene("MainMenu");
    }

    void Start() {

        //Load the data from the GameFile
        TextAsset GameFile = new TextAsset();
        GameFile = Resources.Load("GameData") as TextAsset;
        
        //Create a dictionary that can be referenced by gamedic["varname"] = stringresult, i.e. gamedic[1playerName] will return Rachel or some shit
        gamedic = GameFile.text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(l => l.Split(new[] { '=' })).ToDictionary(s => s[0].Trim(), s => s[1].Trim());

        //assign values to the Load game screen - this will need to be moved into a different script for that scene otherwise it will happen on EVERY scene and then get angry
        Text name1 = GameObject.Find("Name1").GetComponent<Text>();
        name1.text = gamedic["1playerName"];

        Text name2 = GameObject.Find("Name2").GetComponent<Text>();
        name2.text = gamedic["2playerName"];

        Text name3 = GameObject.Find("Name3").GetComponent<Text>();
        name3.text = gamedic["3playerName"];

        Text name4 = GameObject.Find("Name4").GetComponent<Text>();
        name4.text = gamedic["4playerName"];
    }

    //Called upon the click of a button
    private void FirstTimeCheck(string playernum)
    {
        if (gamedic[playernum + "newGame" ] == "true")
        {
            SceneManager.LoadScene("CharacterCustomize");
            string val = playernum + "newGame";
            gamedic[val] = "false";
 //           NEED TO CHANGE STRING TO READ FALSE!
        }
        else if (gamedic[playernum + "newGame"] == "false")
        {
            SceneManager.LoadScene("OverWorld");
        }
    }

    //void SaveFile()
    //{
    //    while (true)
    //    {
            
    //    }
    //}

}
