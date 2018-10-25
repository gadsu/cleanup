using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBSOOO : MonoBehaviour {

    GameObject[] points;
    GameObject target = null;  //Will be the player
    public List<Transform> targetArr; //array of patrol points
    public int hitCount = 0;
    public int jumpCount = 0;
    public static float PTIME = 4f;
    public int playerFollowCountDown;
    public int YPosFreeze = 47;
    Rigidbody2D rb;
    public float basicSpeed = 30f;
    public float specialSpeed = 40f;
    EnemyState es;
    //Animator an;
    public bool onGround;

    // Use this for initialization
    void Start ()
    {
        target = GameObject.Find("Player");    //Find the player
        points = GameObject.FindGameObjectsWithTag("Spawner");   //Find all of the spawner objects in the scene
        targetArr.Clear();

        rb = GetComponent<Rigidbody2D>();
        es = GetComponent<EnemyState>();
        //an = GetComponent<Animator>();


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
        else
        {
            bigJump();
            jumpCount = 0;
        }

    }

    public void basicJump()
    {
        Debug.Log("aim dumbping");
        onGround = false;
        //an.Play("jump");
        Vector3 playerPos = target.transform.position;
        Vector2 vel = new Vector2(rb.velocity.x, rb.velocity.y);
        if (playerPos.x > rb.position.x)  //If it is to the right of you
        {
            if (!es.facingRight)
            {
                GetComponent<EnemyState>().Flip();
            }

            vel.x = basicSpeed;
            vel.y = specialSpeed;
        }
        else if (playerPos.x < rb.position.x)   //If it is to the left of you
        {
            if (es.facingRight)
            {
                GetComponent<EnemyState>().Flip();
            }

            vel.x = basicSpeed * -1;
            vel.y = specialSpeed;
        }
        rb.velocity = vel*2;
    }

    public void bigJump()
    {
        Debug.Log("jump count is " + jumpCount);
        onGround = false;
        //an.Play("jump");
        Vector3 playerPos = target.transform.position;
        Vector2 vel = new Vector2(rb.velocity.x, rb.velocity.y);
        if (playerPos.x > rb.position.x)  //If it is to the right of you
        {
            if (!es.facingRight)
            {
                GetComponent<EnemyState>().Flip();
            }

            vel.x = basicSpeed;
            vel.y = specialSpeed * 4;
        }
        else if (playerPos.x < rb.position.x)   //If it is to the left of you
        {
            if (es.facingRight)
            {
                GetComponent<EnemyState>().Flip();
            }

            vel.x = basicSpeed * -1;
            vel.y = specialSpeed * 4;
        }
        rb.velocity = vel;

        if (rb.transform.position.y >= YPosFreeze)
        {
                rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                if (playerFollowCountDown < PTIME)
                {
                    MoveTowardsPoint(target.transform.position);
                    playerFollowCountDown++;
                }
                else
                {
                    rb.constraints = ~RigidbodyConstraints2D.FreezePositionY;
                    rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                    vel.y = specialSpeed * -4;
                    rb.velocity = vel;
                    rb.constraints = ~RigidbodyConstraints2D.FreezePositionX;
                }
            }
    }

    public void specialAttack()
    {

    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Platform" && !onGround)
        {
            onGround = true;
        }
    }
    // Update is called once per frame
    void FixedUpdate ()
    {
        if(hitCount == 3)
        {
            //do stuff
        }
        //!an.GetCurrentAnimatorStateInfo(0).IsName("jump")
        if (onGround)
        {
            movementController();
        }
        else if (!onGround)
        {
           // MoveTowardsPoint(target.transform.position);
        }
	}

    private void MoveTowardsPoint(Vector3 pos)
    {
        GetComponent<EnemyState>().walkto(pos); 
    }
}
