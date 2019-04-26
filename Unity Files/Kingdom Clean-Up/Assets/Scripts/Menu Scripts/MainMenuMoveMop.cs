//-------------COMPLETE DO NOT TOUCH----------//



//    MainMenuMoveMop
//    Code to turn on the mop images.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuMoveMop : MonoBehaviour, ISelectHandler, IDeselectHandler // ISelect and IDeselect used for controller use
{
    public GameObject MopImage;
    public GameObject SlimeOverlay;

    private void Start()
    {
        // MopImage = gameObject.transform.Find("Sprite").gameObject;
        Hide(MopImage);
        //Show(SlimeOverlay);
    }
    
    //If button is being hovered over show Mop on the Left
    public void OnMouseEnter()
    {
        Show(MopImage);
        //Hide(SlimeOverlay);
    }
    private void OnMouseOver()
    {
        Show(MopImage);
        //Hide(SlimeOverlay);
    }
    private void OnMouseExit() //hide mop when not hovered
    {
        Hide(MopImage);
        //Show(SlimeOverlay);
    }

    //same as above but with controller
    void ISelectHandler.OnSelect(BaseEventData eventData)
    {
        Show(MopImage);
        //Hide(SlimeOverlay);
    }
    void IDeselectHandler.OnDeselect(BaseEventData eventData)
    {
        Hide(MopImage);
        //Show(SlimeOverlay);
    }
    
    // shows and hides objects
    public void Show(GameObject TheObject)
    {
        TheObject.SetActive(true);
    }

    public void Hide(GameObject TheObject)
    {
        TheObject.SetActive(false);
    }
}
