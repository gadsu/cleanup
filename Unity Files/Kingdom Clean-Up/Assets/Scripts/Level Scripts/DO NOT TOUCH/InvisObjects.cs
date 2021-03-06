﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisObjects : MonoBehaviour {

    public GameObject BossSpawnerObject;
    public GameObject rightWall;
    public GameObject leftWall;

    //turns on OBSOOO spawner
    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("HE RAN THROUGH ME");
        if (col.gameObject.tag == "Player")
        {
            ShowObject();
        }
    }

    //turns on invisWalls
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Boss")
        {
            leftWall.SetActive(true);
            rightWall.SetActive(true);
        }
        
    }



    //private void OnTriggerExit2D(Collider2D col)
    //{
    //    if (col.gameObject.tag == "Player")
    //    {
    //        GetComponent<Collider2D>().isTrigger = false;
    //    }
    //}

    //shows Spawner
    public void ShowObject()
    {
        BossSpawnerObject.SetActive(true);
        Debug.Log("Spawner is on");
    }

    //hides Spawner
    public void HideObject()
    {

        BossSpawnerObject.SetActive(false);
    }

    // Use this for initialization
    void Start ()
    {
        //rightWall = GameObject.Find("RightWall");
        //leftWall = GameObject.Find("LeftWall");
        //BossSpawnerObject = GameObject.Find("BossSlimeSpawner");
        HideObject();
	}
    
    public void DisableWalls()
    {
        leftWall.SetActive(false);
        rightWall.SetActive(false);
    }
    
}
