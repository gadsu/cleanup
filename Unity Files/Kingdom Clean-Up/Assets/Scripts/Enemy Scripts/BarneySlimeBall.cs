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
        if (GameObject.Find("DontDestroyOnLoad"))
            ps = GameObject.Find("DontDestroyOnLoad").GetComponent<PlayerState>();
        player = GameObject.Find("Player");
        playerPos = player.transform.position;
        hitspot = false;
        initPos = gameObject.transform.position;
        
        Vector2 relativePos = player.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector2.up);
        rotation.x = 0;
        rotation.y = 0;
        transform.rotation = rotation;
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
            if (ps)
                ps.takeDamage(slimeDamage); //damage player
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.name == "BarnBox")
        {
            Transform startpos = gameObject.transform; //find location of self
            Instantiate<GameObject>(slimeBall, startpos.position, new Quaternion());  //Create slime
            Destroy(gameObject); //kill self
        }
    }
}
