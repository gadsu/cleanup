
//    MainMenu
//    Any code needed for the main menu (mostly quit)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeSceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void setResetPlayer()
    {
        GameObject.Find("DontDestroyOnLoad").GetComponent<PlayerState>().playerHealth = 100;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit mothas");
    }
}
