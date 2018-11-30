
//    MainMenu
//    Any code needed for the main menu and for transferring scenes

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public void ChangeSceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void setResetPlayer()
    {
        GameObject.Find("DontDestroyOnLoad").GetComponent<PlayerState>().playerHealth = 100;
    }
    
    //quits the game
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit mothas");
    }
    
}
