using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBSOOO : MonoBehaviour {

    GameObject[] points;
    GameObject target = null;  //Will be the player
    public List<Transform> targetArr; //array of patrol points
    public int hitCount = 0;
    public int jumpCount = 0;
    public bool doingSpecial = false;
    public int attackSpecial = 0; // 0 - Not Attacking, 1 - Left, 2 - Right
    public static float PTIME = 10f;
    public float playerFollowCountDown = 0;
    public int YPosFreeze = 47;
    Rigidbody2D rb;
    public float basicSpeed = 30f;
    public float specialSpeed = 40f;
    EnemyState es;
    Animator an;
    public bool onGround;
    GameObject leftPoint;
    GameObject rightPoint;
    float rightX;
    float leftX;
    bool facingRight;

    // Use this for initialization
    void Start ()
    {
        target = GameObject.Find("Player");    //Find the player
        points = GameObject.FindGameObjectsWithTag("Spawner");   //Find all of the spawner objects in the scene
        targetArr.Clear();


        //temp get points
        leftPoint = GameObject.Find("LeftPatrolPoint");
        rightPoint = GameObject.Find("RightPatrolPoint");
        rightX = rightPoint.transform.position.x;
        leftX = leftPoint.transform.position.x;

        rb = GetComponent<Rigidbody2D>();
        es = GetComponent<EnemyState>();
        an = GetComponentInChildren<Animator>();


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
        if (hitCount >= 3 && doingSpecial == false) //Setting persistent direction so OBSOOO doesn't change direction during special prep
        {
            facingRight = target.GetComponent<PlayerController>().facingRight;
            Debug.Log("Setting Direction for Special Attack. Facing Right = " + facingRight);
            hitCount = 0;
            doingSpecial = true;
        }
        if (doingSpecial) //Move to and commence special attack
        {
            if (rb.position.x < rightX && rb.position.x > leftX && attackSpecial == 0)
            {
                moveToSpecialAttack(facingRight);
            }
            else if ((rb.position.x <= leftX && attackSpecial == 1) || (rb.position.x >= rightX && attackSpecial == 2))
            {
                doingSpecial = false;
                attackSpecial = 0;
            }
            else if (rb.position.x >= rightX || attackSpecial == 1)
            {
                specialAttack(leftPoint.transform.position);
                attackSpecial = 1;
            }
            else if (rb.position.x <= leftX || attackSpecial == 2)
            {
                specialAttack(rightPoint.transform.position);
                attackSpecial = 2;
            }
        }
        if (hitCount < 3 && !doingSpecial) //Normal movement
        {
            if (jumpCount < PTIME) 
            {
                Vector3 playerPos = target.transform.position;

                basicJump(playerPos);
                jumpCount++;
            }
            else
            {
                bigJump();
                jumpCount = 0;
            }
        }

    }

    public void basicJump(Vector3 targetPos)
    {
        onGround = false;
        an.Play("jump");
        Vector2 vel = new Vector2(rb.velocity.x, rb.velocity.y);
        if (targetPos.x > rb.position.x)  //If it is to the right of you
        {
            if (!es.facingRight)
            {
                GetComponent<EnemyState>().Flip();
            }

            vel.x = basicSpeed;
            vel.y = specialSpeed;
        }
        else if (targetPos.x < rb.position.x)   //If it is to the left of you
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
        onGround = false;
        an.Play("jump");
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
    }
    public void freezeInAir()
    {
        Vector2 vel = new Vector2(rb.velocity.x, rb.velocity.y);
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        if (playerFollowCountDown < PTIME)
        {
            MoveTowardsPoint(target.transform.position);
            playerFollowCountDown = playerFollowCountDown + Time.deltaTime;
        }
        else
        {
            //rb.constraints = RigidbodyConstraints2D.None;
            //rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            vel.y = specialSpeed * -4;
            rb.velocity = vel;
            //rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            playerFollowCountDown = 0;
        }
    }

    public void moveToSpecialAttack(bool playerDir)
    {
        if (playerDir)
        {
            basicJump(rightPoint.transform.position);
        }
        else
        {
            basicJump(leftPoint.transform.position);
        }
    }

    public void specialAttack(Vector2 pointPos)
    {
        onGround = false;
        an.Play("jump");
        Vector2 vel = new Vector2(rb.velocity.x, rb.velocity.y);
        if (pointPos.x > rb.position.x)  //If it is to the right of you
        {
            if (!es.facingRight)
            {
                GetComponent<EnemyState>().Flip();
            }

            vel.x = basicSpeed *4;
            vel.y = specialSpeed;
        }
        else if (pointPos.x < rb.position.x)   //If it is to the left of you
        {
            if (es.facingRight)
            {
                GetComponent<EnemyState>().Flip();
            }

            vel.x = basicSpeed * -4;
            vel.y = specialSpeed;
        }
        rb.velocity = vel;
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Platform" && !onGround)
        {
            onGround = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "mopAttack")
        {
            hitCount++; 
        }
    }


    // Update is called once per frame
    void FixedUpdate ()
    {
        
        

        if (rb.transform.position.y >= YPosFreeze)
        {
            freezeInAir();
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
