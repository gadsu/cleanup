using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System;
public class GameData : MonoBehaviour {
    //player name
    public Text playerName;
    public bool newGame;
    public string Appearance;
    public Dictionary<string, string> gamedic;

    // Use this for initialization
    void Start() {

        TextAsset GameFile = new TextAsset();
        GameFile = Resources.Load("GameData") as TextAsset;
        gamedic = GameFile.text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(l => l.Split(new[] { '=' })).ToDictionary(s => s[0].Trim(), s => s[1].Trim());

        Text name1 = GameObject.Find("Name1").GetComponent<Text>();
        name1.text = gamedic["1playerName"];

        Text name2 = GameObject.Find("Name2").GetComponent<Text>();
        name2.text = gamedic["2playerName"];

        Text name3 = GameObject.Find("Name3").GetComponent<Text>();
        name3.text = gamedic["3playerName"];

        Text name4 = GameObject.Find("Name4").GetComponent<Text>();
        name4.text = gamedic["4playerName"];
    }



    //void SaveFile()
    //{
    //    while (true)
    //    {
            
    //    }
    //}

}
