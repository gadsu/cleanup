﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningWeapons : MonoBehaviour
{
    int itemSelected = 0;
    public bool itemsOn;
    public GameObject[] list;
    public GameObject items;
    public GameObject gloveIndicator;


    // Start is called before the first frame update
    void Start()
    {
        gloveIndicator = GameObject.Find("GloveIndicator");
        gloveIndicator.SetActive(false);
        list[1].SetActive(false);
        list[2].SetActive(false);
        //items.SetActive(false);
        //itemsOn = false;
        itemsOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("ShowItem") && itemsOn == false)
        {
            items.SetActive(true);
            itemsOn = true;
        }
        //else if (Input.GetButtonDown("ShowItem") && itemsOn == true)
        //{
        //    items.SetActive(false);
        //    itemsOn = false;
        //    gloveIndicator.SetActive(false);
        //}
        if (Input.GetButtonDown("SwapItemLeft") && itemsOn == true)
        {
            itemCycleBackwards();
        }
        else if (Input.GetButtonDown("SwapItemRight") && itemsOn == true)
        {
            itemCycleForward();
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

        if (itemSelected == 1)
        {
            gloveIndicator.SetActive(true);
        }
        else
        {
            gloveIndicator.SetActive(false);
        }

        list[itemSelected].SetActive(true);
    }

    public void itemCycleBackwards()
    {
        list[itemSelected].SetActive(false);

        itemSelected--;

        if ((itemSelected) < 0)
        {
            itemSelected = 2;
        }

        if (itemSelected == 1)
        {
            gloveIndicator.SetActive(true);
        }
        else
        {
            gloveIndicator.SetActive(false);
        }
        list[itemSelected].SetActive(true);
    }
}
