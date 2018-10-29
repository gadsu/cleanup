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
    int specialAttackTimer = 0;
    bool playerDir;

    // Use this for initialization
    void Start ()
    {
        target = GameObject.Find("Player");    //Find the player
        points = GameObject.FindGameObjectsWithTag("Spawner");   //Find all of the spawner objects in the scene
        targetArr.Clear();


        //temp get points
        leftPoint = GameObject.Find("LeftPatrolPoint");
        rightPoint = GameObject.Find("RightPatrolPoint");

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
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            vel.y = specialSpeed * -4;
            rb.velocity = vel;
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            playerFollowCountDown = 0;
        }
    }

    public void moveToSpecialAttack(bool playerDir)
    {
        if (onGround)
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
        if(rb.position.x == leftPoint.transform.position.x || rb.position.x == rightPoint.transform.position.x)
        {
            if (rb.position.x == leftPoint.transform.position.x)
            {
                specialAttackTimer = 1;
            }
            else
            {
                specialAttackTimer = 2;
            }
        }
    }

    public void specialAttack(Vector2 pointPos)
    {
        onGround = false;
        an.Play("jump");
        Vector2 vel = new Vector2(rb.velocity.x, rb.velocity.y);
        specialSpeed = 20f;
        if (pointPos.x > rb.position.x)  //If it is to the right of you
        {
            if (!es.facingRight)
            {
                GetComponent<EnemyState>().Flip();
            }

            vel.x = basicSpeed *3;
            vel.y = specialSpeed / 2;
        }
        else if (pointPos.x < rb.position.x)   //If it is to the left of you
        {
            if (es.facingRight)
            {
                GetComponent<EnemyState>().Flip();
            }

            vel.x = basicSpeed * -3;
            vel.y = specialSpeed / 2;
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
        
        if (hitCount >= 3)
        {
            playerDir = target.GetComponent<PlayerController>().facingRight;
            Debug.Log(" OUCH");
            moveToSpecialAttack(playerDir);
        }

        if (specialAttackTimer == 1)
        {
            specialAttack(rightPoint.transform.position);
        }

        if (specialAttackTimer == 2)
        {
            specialAttack(leftPoint.transform.position);
        }

        if (rb.transform.position.y >= YPosFreeze)
        {
            freezeInAir();
        }
        //!an.GetCurrentAnimatorStateInfo(0).IsName("jump")
        if (onGround && hitCount < 3)
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
