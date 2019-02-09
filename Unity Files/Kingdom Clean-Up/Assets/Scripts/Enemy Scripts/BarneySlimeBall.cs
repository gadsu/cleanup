using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarneySlimeBall : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    GameObject player;
    public Vector2 playerPos;
    float playerHealth;
    public int slimeDamage;
    public GameObject slimeBall;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        player = GameObject.Find("Player");
        playerHealth = GameObject.Find("DontDestoryOnLoad").GetComponent<PlayerState>().playerHealth;
        playerPos = new Vector2();
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    
        ShootPlayer( playerPos);

    }
    
    void ShootPlayer(Vector2 playerPos)
    {
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, playerPos, speed);
        
    }

    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameObject.Find("DontDestroyOnLoad").GetComponent<PlayerState>().takeDamage(slimeDamage); //damage player
            Destroy(gameObject); //kill self
        }
        else //if (col.gameObject.tag == "Barn")
        {
            Transform startpos = gameObject.transform; //find location of self
            Instantiate<GameObject>(slimeBall, startpos.position, startpos.rotation);  //Create slime
            Destroy(gameObject); //kill self
        }
    }
}
