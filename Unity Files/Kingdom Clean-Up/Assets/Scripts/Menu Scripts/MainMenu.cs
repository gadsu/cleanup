
//    MainMenu
//    Any code needed for the main menu (mostly quit)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour, ISelectHandler, IDeselectHandler // ISelect and IDeselect used for controller use
{
    GameObject Child;

    private void Start()
    {
        Child = gameObject.transform.Find("Sprite").gameObject;
        Hide(Child);
    }

    public void ChangeSceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void setResetPlayer()
    {
        GameObject.Find("DontDestroyOnLoad").GetComponent<PlayerState>().playerHealth = 100;
    }
    
    //If button is being hovered over show Mop on the Left
    public void OnMouseEnter()
    {
        Show(Child);
    }
    private void OnMouseOver()
    {
        Show(Child);
    }
    private void OnMouseExit() //hide mop when not hovered
    {
        Hide(Child);
    }
    
    //same as above but with controller
    void ISelectHandler.OnSelect(BaseEventData eventData)
    {
        Show(Child);
    }   
    void IDeselectHandler.OnDeselect(BaseEventData eventData)
    {
        Hide(Child);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit mothas");
    }

    public void Show(GameObject Gobject)
    {
        Gobject.SetActive(true);
    }

    public void Hide(GameObject Gobject)
    {

        Gobject.SetActive(false);
    }
}
