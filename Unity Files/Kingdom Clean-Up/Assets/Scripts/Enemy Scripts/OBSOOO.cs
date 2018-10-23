using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBSOOO : MonoBehaviour {

    GameObject[] points;
    GameObject target = null;  //Will be the player
    public List<Transform> targetArr;
    public int hitCount = 0;
    public int jumpCount = 0;
    public static float PTIME = 4f;
    Rigidbody2D rb;
    public float basicSpeed = 20f;
    public float specialSpeed = 40f;
    public bool facingRight = true;
    private bool onGround;

    // Use this for initialization
    void Start ()
    {
        target = GameObject.Find("Player");    //Find the player

        points = GameObject.FindGameObjectsWithTag("Spawner");   //Find all of the spawner objects in the scene
        targetArr.Clear();

        rb = GetComponent<Rigidbody2D>();

        //Cycle through all spawner objects and only add the ones that match our character
        foreach (GameObject n in points)
        {
            if (n.name.Contains("PatrolPoint") && n.transform.parent.name.Contains("Boss"))
            {
                targetArr.Add(n.transform);
            }
        }
    }


    public void movementController()
    {
        if (jumpCount < PTIME) 
        {
            basicJump();
            jumpCount++;
        }
        else if (jumpCount == PTIME)
        {
            //big jump
            jumpCount = 0;
        }

    }

    public void basicJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, basicSpeed);

        Vector3 playerPos = target.transform.position;
        if (playerPos.x > rb.position.x)  //If it is to the right of you
        {
            if (!facingRight)
            {
                GetComponent<EnemyState>().Flip();
            }

            rb.velocity = new Vector2(rb.velocity.y, basicSpeed);
        }
        else if (playerPos.x < rb.position.x)   //If it is to the left of you
        {
            if (facingRight)
            {
                GetComponent<EnemyState>().Flip();
            }

            rb.velocity = new Vector2(-1 * rb.velocity.y, basicSpeed);
        }
        
        if(gameObject.transform.position.y >= 25)
        {
            rb.velocity = new Vector2(-1 * rb.velocity.x, basicSpeed);
        }
        
    }

    public void bigjump()
    {

    }

    public void specialAttack()
    {

    }

    private void OnCollisionStay2D(Collision2D col)
    {
        while (col.gameObject.tag == "Platform")
        {
            onGround = true;
        }
        onGround = false;
    }
    // Update is called once per frame
    void FixedUpdate ()
    {
        if(onGround)
        {
            movementController();
        }
        
	}

    private void MoveTowardsPoint(Vector3 pos)
    {
        GetComponent<EnemyState>().walkto(pos); 
    }
}
