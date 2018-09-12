using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{

    public Transform player;
    public float maxDist = 20.0f;
    public float cameraSpeed = 2.0f;
    public Vector3 distance;
    public Vector3 currentDistance;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        distance = transform.position - player.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        //       Vector3 targetPosition = player.position + 
        //        distance = transform.position - player.position;
        //       distance.y = 0.0f;
        //       Debug.Log("Max dist: " + maxDist + "Distance: " + distance.ToString());
        //      transform.position += player.position + distance.normalized * maxDist;
        float interpolation = cameraSpeed * Time.deltaTime;

        Vector3 position = transform.position;

//        Debug.Log(Vector3.Distance(transform.position, player.position));
//        if (Vector3.Distance(transform.position, player.position) >= maxDist)
 //       {
            position.y = Mathf.Lerp(transform.position.y, player.transform.position.y, interpolation);
            position.x = Mathf.Lerp(transform.position.x, player.transform.position.x, interpolation);
            transform.position = position;
 //       }
    }
}
