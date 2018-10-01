using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorKillbox : MonoBehaviour {

    public Transform playertransform;
    Slider health;

    // Use this for initialization
    void Start()
    {
        playertransform = GameObject.Find("Player").GetComponent<Transform>();
        health = GameObject.Find("Health").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playertransform.position.x, transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.transform.position = new Vector3(-25, 7, 0);
            if (health.value > 0)
            {
                GameObject.Find("Health").GetComponent<Slider>().value -= 33;
            }
            else
            {
                Debug.Log("Player is DEAD");
                GameObject.Find("UI Canvas").GetComponent<KillScreen>().KillScreenControl();
            }
        }
        else
        {
            // kil
        }
    }


    
}
