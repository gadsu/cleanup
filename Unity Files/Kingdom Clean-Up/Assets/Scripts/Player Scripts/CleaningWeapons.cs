using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningWeapons : MonoBehaviour
{
    int itemSelected = 0;
    public bool itemsOn;
    public GameObject[] list;
    public GameObject items;



    // Start is called before the first frame update
    void Start()
    {

        items.SetActive(false);
        itemsOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("ShowItem") && itemsOn == false)
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
            itemSelected = 2;
        }
        list[itemSelected].SetActive(true);
    }
}
