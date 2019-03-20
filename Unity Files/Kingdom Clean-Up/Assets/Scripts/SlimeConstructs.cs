using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeConstructs : MonoBehaviour {

    public int green;
    public int red;
    public int blue;
    public string color;
    public GameObject slimePrefab;
    public bool isFrozen = false;
    GameObject player;
    Rigidbody2D playerRB;
    public GameObject GreenGloves;
    public GameObject BlueBottle;
    public GameObject RedBucket;


    // Use this for initialization
    void Start ()
    {
        GreenGloves = GameObject.Find("GreenGloves");
        BlueBottle = GameObject.Find("Blue");
        RedBucket = GameObject.Find("Red");
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isFrozen && Input.GetButtonDown("Jump"))
        {
            player.GetComponent<PlayerController>().jump();
            isFrozen = false;
            playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //for green walls
        if(col.gameObject.tag == "Player" && color == "green")
        {
            if (GreenGloves.activeInHierarchy == true)
            {
                if (GameObject.Find("DontDestroyOnLoad").GetComponent<PlayerState>().greenSlimeMeter >= 10)
                {
                    playerRB = col.gameObject.GetComponent<Rigidbody2D>();
                    player = col.gameObject;
                    freezePlayer();
                }
            }
        }
    }

    public void freezePlayer()
    {
        playerRB.constraints = RigidbodyConstraints2D.FreezeAll; //when player hits wall FREEZE THEM
        isFrozen = true;

    }

}
