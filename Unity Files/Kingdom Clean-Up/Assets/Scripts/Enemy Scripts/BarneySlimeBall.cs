using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarneySlimeBall : MonoBehaviour
{
    public float speed;
    GameObject player;
    Vector3 playerPos;
    PlayerState ps;
    public int slimeDamage = 34;
    public GameObject slimeBall;
    bool hitspot;
    Vector3 initPos;


    // Start is called before the first frame update
    void Start()
    {
        //rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        player = GameObject.Find("Player");
        ps = GameObject.Find("DontDestroyOnLoad").GetComponent<PlayerState>();
        playerPos = player.transform.position;
        hitspot = false;
        initPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!hitspot)
            ThrowSlime();
        else
            Flee();

    }

    void ThrowSlime()
    {
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, playerPos, speed);
        if (playerPos == transform.position)
        {
            hitspot = true;
            Debug.Log("HIT");
        }
    }

    void Flee()
    {
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, initPos, -1 * speed);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            ps.takeDamage(slimeDamage); //damage player
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("I LEFT " + col.gameObject.name);
        if(col.gameObject.name == "BarnBox")
        {
            Debug.Log("I HAVE LEFT THE BARN");
            Transform startpos = gameObject.transform; //find location of self
            Instantiate<GameObject>(slimeBall, startpos.position, startpos.rotation);  //Create slime
            Destroy(gameObject); //kill self
        }
    }
}
