using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour {

    public Transform player;
    public float maxDist = 3.0f;
    public Vector3 distance;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").GetComponent<Transform>();

	}
	
	// Update is called once per frame
	void Update () {
 //       Vector3 targetPosition = player.position + 
//        distance = transform.position - player.position;
 //       distance.y = 0.0f;
 //       Debug.Log("Max dist: " + maxDist + "Distance: " + distance.ToString());
  //      transform.position += player.position + distance.normalized * maxDist;
	}
}
