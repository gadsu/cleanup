//COMPLETE DO NOT TOUCH//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningWeapons : MonoBehaviour
{
    int itemSelected = 0;
    public bool itemsOn;
    public GameObject[] list;
    public GameObject items;
    Animator an;

    private void Awake()
    {
        an = GetComponent<Animator>();
    }

        // Start is called before the first frame update
        void Start()
    {
        list[1].SetActive(false);
        items.SetActive(false);
        itemsOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        //SetBlueBootAnimations();//Play Blue Boot Animations BROKEN BEACUSE SPRITE STUFF

        if (Input.GetButtonDown("ShowItem") && itemsOn == false)
        {
            items.SetActive(true);
            itemsOn = true;
        }
        else if (Input.GetButtonDown("ShowItem") && itemsOn == true)
        {
            items.SetActive(false);
            itemsOn = false;
        }
        if (Input.GetButtonDown("SwapItemLeft") && itemsOn == true)
        {
            itemCycleBackwards();
        }
        else if (Input.GetButtonDown("SwapItemRight") && itemsOn == true)
        {
            itemCycleForward();
        }
    }

    public void SetBlueBootAnimations()
    { 
        if(itemSelected == 1)
        {
            an.SetBool("isBlue", true);
            
        }
        else
        {
            an.SetBool("isBlue", false);
            
        }
    }

    public void itemCycleForward()
    {
        list[itemSelected].SetActive(false);

        itemSelected++;

        if ((itemSelected) >= list.Length)
        {
            itemSelected = 0;
        }

        list[itemSelected].SetActive(true);
    }

    public void itemCycleBackwards()
    {
        list[itemSelected].SetActive(false);

        itemSelected--;

        if ((itemSelected) < 0)
        {
            itemSelected = 1;
        }

        list[itemSelected].SetActive(true);
    }
}
