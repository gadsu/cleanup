using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarneySlimeBall : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    GameObject player;
    Vector2 playerPos;
    float playerHealth;
    public int slimeDamage = 34;
    public GameObject slimeBall;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        player = GameObject.Find("Player");
        playerHealth = GameObject.Find("DontDestoryOnLoad").GetComponent<PlayerState>().playerHealth;
        playerPos = player.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ThrowSlime();
    }

    void ThrowSlime()
    {
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, playerPos, speed);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            playerHealth -= slimeDamage; //damage player
        }
        else if (col.gameObject.tag == "Barn")
        {
            Transform startpos = gameObject.transform; //find location of self
            Instantiate<GameObject>(slimeBall, startpos.position, startpos.rotation);  //Create slime
            Destroy(gameObject); //kill self
        }
    }

}
