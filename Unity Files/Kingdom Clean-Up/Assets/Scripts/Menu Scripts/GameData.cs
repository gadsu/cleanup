
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
    public string saveFileNum;

    public Dictionary<string, string> gamedic;
    public Dictionary<string, string> playerdic;


    void Start() {

        //Load the data from the GameFile
        TextAsset GameFile = new TextAsset();
        GameFile = Resources.Load("GameData") as TextAsset;
        
        //Create a dictionary that can be referenced by gamedic["varname"] = stringresult, i.e. gamedic[1playerName] will return Rachel or some shit
        gamedic = GameFile.text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(l => l.Split(new[] { '=' })).ToDictionary(s => s[0].Trim(), s => s[1].Trim());
        LoadFile("0");
    }

    void Update()
    {

    }
    public void LoadFile(string num)
    {
        //Load the data from the GameFile
        saveFileNum = num;

        TextAsset SaveFile = new TextAsset();
        SaveFile = Resources.Load("Save" + num) as TextAsset;

        //Create a dictionary that can be referenced by gamedic["varname"] = stringresult, i.e. gamedic[1playerName] will return Rachel or some shit
        playerdic = SaveFile.text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(l => l.Split(new[] { '=' })).ToDictionary(s => s[0].Trim(), s => s[1].Trim());

    }

    public void ReloadLevel()
    {
        PlayerState ps = GetComponent<PlayerState>();
        ps.playerHealth = 100f;

        ps.greenSlimeMeter = 0;
//        ps.greenMeter.GetComponent<Slider>().value = 0;
        //ps.setSlimeMeterImage(0, ps.greenChildren);
        ps.greenChildren.Clear();

        ps.redSlimeMeter = 0;
//        ps.redMeter.GetComponent<Slider>().value = 0;
        //ps.setSlimeMeterImage(0, ps.redChildren);
        ps.blueChildren.Clear();

        ps.blueSlimeMeter = 0;
//        ps.blueMeter.GetComponent<Slider>().value = 0;
        //ps.setSlimeMeterImage(0, ps.blueChildren);
        ps.redChildren.Clear();

        ps.sceneLoaded = false;
    }

    public void SaveFile()
    {
        //Save player state
        string saveData = "";
        foreach(var item in playerdic)
        {
            saveData += item.Key + " = " + item.Value + "\r\n";
        }

        File.WriteAllText("Assets/Resources/Save" + saveFileNum + ".txt", saveData);

        //Save Gamedata state
        string gameData = "";
        foreach (var item in gamedic)
        {
            gameData += item.Key + " = " + item.Value + "\r\n";
        }

        File.WriteAllText("Assets/Resources/GameData.txt", gameData);
        Debug.Log("Saved.");
    }

    public void DeleteFile(string num)
    {
        LoadFile("0");
        saveFileNum = num;
        gamedic[num + "newGame"] = "true";
        gamedic[num + "playerName"] = "--- New Game ---";
        gamedic[num + "appearance"] = "Default";
        SaveFile();
    }
    
    public void copyFile(string source, string dest)
    {
        gamedic[dest + "newGame"] = gamedic[source + "newGame"];
        gamedic[dest + "playerName"] = gamedic[source + "playerName"];
        gamedic[dest + "appearance"] = gamedic[source + "appearance"];
    }
}
