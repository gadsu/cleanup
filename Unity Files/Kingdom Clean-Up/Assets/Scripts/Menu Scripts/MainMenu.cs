
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
        if(GameObject.Find("DontDestroyOnLoad").GetComponent<PlayerState>().sceneLoaded == true)
        {
            GameObject.Find("DontDestroyOnLoad").GetComponent<GameData>().ReloadLevel();
        }
        Debug.Log("Got Scene");
        SceneManager.LoadScene(name);
        Debug.Log("Changing Scene");
    }

    public void ReloadScene()
    {
        GameObject.Find("DontDestroyOnLoad").GetComponent<GameData>().ReloadLevel();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Reloading Scene");
    }

    //quits the game
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit mothas");
    }
    
}
