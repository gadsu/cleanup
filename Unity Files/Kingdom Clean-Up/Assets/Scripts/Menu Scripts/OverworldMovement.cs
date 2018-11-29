
//    Overworld Movement
//    Script for the thing that moves between worlds in the save file
//    needs a LOT OF FUCKIN WORK

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OverworldMovement : MonoBehaviour {

    Vector3 move;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        move = EventSystem.current.currentSelectedGameObject.transform.position - transform.position;

        transform.Translate(move * 0.05f);

    }
}
