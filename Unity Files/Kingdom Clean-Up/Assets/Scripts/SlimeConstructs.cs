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

    //A mirror of EnemyState's Death function
    
        //Breaks the slime wall DONT DELETE

    public void breakSlime()
    {
        Transform currentPos = gameObject.transform;
        int i = 1;

    //    while (green + red + blue > 0)
    //    {
    //        GameObject SlimeViscera = Instantiate<GameObject>(slimePrefab, currentPos.position, currentPos.rotation);
    //        SlimeViscera.transform.localScale = new Vector3(2.5f, 2.5f, 0);

    //        if (green > 0)
    //        {
    //            green--;
    //            SlimeViscera.gameObject.GetComponent<ItemInteraction>().setColor("green");
    //        }
    //        else if (red > 0)
    //        {
    //            red--;
    //            //SlimeViscera.gameObject.GetComponent<ItemInteraction>().setColor("red");
    //        }
    //        else if (blue > 0)
    //        {
    //            blue--;
    //            //SlimeViscera.gameObject.GetComponent<ItemInteraction>().setColor("blue");
    //        }
    //        else
    //        {
    //            Debug.Log("IDK MAN");
    //        }


    //        Vector2 vel = new Vector2(30f, 15f);
    //        if (i % 3 == 0)
    //        {
    //            vel.x *= 0;
    //        }
    //        else if(i % 3 == 1)
    //        {
    //            vel.x *= -1;
    //        }

    //        SlimeViscera.GetComponent<ItemInteraction>().setVelocity(vel);
    //        i++;
    //    }
        
    //    Destroy(gameObject);
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
