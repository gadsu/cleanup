using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorKillbox : MonoBehaviour {

    public Transform player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.transform.position = new Vector3(0, 0, 0);
        }
        else
        {
            // kill the damn thing
        }
    }


    
}
