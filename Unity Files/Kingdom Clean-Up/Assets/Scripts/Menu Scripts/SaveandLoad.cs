using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class SaveandLoad : MonoBehaviour {
    public Text name1;
    public Text name2;
    public Text name3;
    public Text name4;
    GameData gd;
    public bool copymode;

	// Use this for initialization
	void Start () {
        gd = GameObject.Find("DontDestroyOnLoad").GetComponent<GameData>();
        loadSaveAndLoad();
    }

    public void loadSaveAndLoad()
    {
        name1 = GameObject.Find("Name1").GetComponent<Text>();
        name1.text = gd.gamedic["1playerName"];

        name2 = GameObject.Find("Name2").GetComponent<Text>();
        name2.text = gd.gamedic["2playerName"];

        name3 = GameObject.Find("Name3").GetComponent<Text>();
        name3.text = gd.gamedic["3playerName"];

        name4 = GameObject.Find("Name4").GetComponent<Text>();
        name4.text = gd.gamedic["4playerName"];
    }
    
    //Called when button is pressed
    public void SelectFile(string playernum)
    {
        //Tell GameData which file to load
        if (copymode)
        {
            gd.copyFile(gd.saveFileNum, playernum);
            gd.LoadFile(gd.saveFileNum);
            gd.saveFileNum = playernum;
            gd.SaveFile();
            GameObject.Find("StatusText").GetComponent<Text>().text = "";
            copymode = false;
            loadSaveAndLoad();
        }
        //Checking if first time, then moving 
        else if (gd.gamedic[playernum + "newGame"] == "true")
        {
            string val = playernum + "newGame";
            gd.gamedic[val] = "false";
            //           NEED TO CHANGE STRING TO READ FALSE! (we do)
            SceneManager.LoadScene("CharacterCustomize");
        }
        else if (gd.gamedic[playernum + "newGame"] == "false")
        {
            SceneManager.LoadScene("OverWorld");
        }
    }

    public void DeleteFile(string playernum)
    {
        gd.DeleteFile(playernum);
        loadSaveAndLoad();
    }
    
    public void startCopy(string playernum)
    {
        GameObject.Find("StatusText").GetComponent<Text>().text = "COPYING FROM FILE " + playernum;
        copymode = true;
        gd.LoadFile(playernum);
    }


    // Update is called once per frame
    void Update () {
		
	}
}
