﻿
//    PlayerController
//    Moving the player character and reading inputs
//    This should control anything and everything to do with the player


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{

    [Header("Debug Variables")]
    [Tooltip("How far should raycasting reach?")]
    public float playerSize = 5000f;
    [Tooltip("Is player touching ground?")]
    public bool onGround;
    [Tooltip("Is player facing right?")]
    public bool facingRight;
    [Tooltip("Can player doublejump?")]
    public bool doubleJump;
    [Tooltip("Is player aiming?")]
    public bool aiming;
    [Tooltip("Can player move?")]
    public bool canMove;
    [Tooltip("Is the jump over?")]
    public bool jumpFinished;
    [Tooltip("The mop object")]
    public GameObject mop;

    [Header("Editable Variables")]
    [Tooltip("How fast can the character move?")]
    public float charMaxSpeed = 40f;
    [Tooltip("How much can the character jump?")]
    public float charJumpSpeed = 60f;

    bool hasLeft;
    float leaveGroundFrame;
    float jumpFrame;
    float force;

    Animator an;
    Rigidbody2D rb;
    //   AimRender ar;

    // Use this for initialization
    void Start()
    {
        onGround = true;
        an = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //       ar = GetComponent<AimRender>();
        aiming = false;
        doubleJump = false;
        facingRight = false;
        canMove = true;
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        /* if (col.tag == "Platform" && !onGround)
         {
             //col.transform.position
             onGround = true;
                        Debug.Log("onground = true");
         } */
        if (col.CompareTag("slimeInteractable"))
        {
            Debug.Log("Hit a slime wall");
            doubleJump = true;
        }
        
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        
        Vector3 colpos = col.gameObject.transform.position;
        Vector3 mypos = transform.position;
        if (col.gameObject.tag == "Platform")
        {
            Debug.DrawRay(transform.position, Vector2.down * playerSize, Color.magenta);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down * playerSize);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.tag + hit.collider.gameObject.tag.ToString());
                if (hit.collider.gameObject.tag == "Platform")
                {
                    //Debug.Log(hit.collider.Distance(GetComponent<Collider2D>()).distance);
                    onGround = true;
                    jumpFinished = false;
                    hasLeft = false;
                }
            }
        }
        
        
    }

    private void OnCollisionExit2D(Collision2D col)
    {

        if (col.gameObject.tag == "Platform" && onGround)
        {
            leaveGroundFrame = Time.deltaTime;
            hasLeft = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("!slimeInteractable"))
        {
            doubleJump = false;
        }
        /* if (onGround == true && col.tag == "Platform")
         {
             onGround = false;
         } */
    }
    private void OnDrawGizmosSelected()
    {
        Debug.DrawRay(transform.position, Vector2.down * playerSize, Color.magenta);
    }

    private void FixedUpdate()
    {
        if (onGround && hasLeft && leaveGroundFrame + 5f < Time.deltaTime)
        {
            onGround = false;
        }
        if (canMove)
        {
            

            //slime throwing shenanigans - movement of reticle happens on the reticle
            if (Input.GetAxis("ShowAim") > 0 && !aiming)//(Input.GetButtonDown("ShowAimButton") || Input.GetAxis("ShowAimTrigger") > 0) && !aiming)
            {
                ShowAim();
                Debug.Log("Showing Aim");
            }
            else if (Input.GetAxis("ShowAim") <= 0 && aiming)//(Input.GetButtonUp("ShowAimButton") || Input.GetAxis("ShowAimTrigger") == 0) && aiming)
            {
                HideAim();
                Debug.Log("Aim Hidden");
            }
            
            
        }

        //Checking for ground
        //Debug.DrawRay(transform.position, Vector2.down * playerSize, Color.magenta);
/*
        Debug.DrawRay(transform.position, Vector2.down * playerSize,  Color.magenta);
        if (!onGround && ((Time.time - jumpFrame) > 0.5f))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down * playerSize);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.tag + hit.collider.gameObject.tag.ToString());
                if (hit.collider.gameObject.tag == "Platform")
                {
                    if (hit.collider.Distance(GetComponent<Collider2D>()).distance < 1)
                    {
                        Debug.Log(hit.collider.Distance(GetComponent<Collider2D>()).distance);
                        onGround = true;
                        jumpFinished = false;
                        doubleJump = true;
                    }
                }
            }

            if ((facingRight && force < 0) || (!facingRight && force > 0))
            {

                force = force * 0.4f + (rb.velocity.x / charMaxSpeed);
                force = Mathf.Clamp(force, -0.6f, 0.6f);

            }
        }*/

        //Checking for Slime Wall
        /*
        if (!onGround && doubleJump && !jumpFinished) // if you slide past and don't jump
        {
            RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left * playerSize, playerSize);
            RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right * playerSize, playerSize);
            if (hitLeft.collider == null && hitRight.collider == null) //update later

            {
                doubleJump = false;
            }

        }
        if (!onGround && !doubleJump && !jumpFinished) //checking to add doublejump
        {
            Debug.DrawRay(transform.position, Vector2.left * playerSize, Color.green);
            Debug.DrawRay(transform.position, Vector2.right * playerSize, Color.green);
            RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left * playerSize, playerSize);
            RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right * playerSize, playerSize);
            if (hitLeft.collider != null) // break into 2 or it gets angry
            {
                if (hitLeft.collider.gameObject.tag == "slimeInteractable" && hitLeft.collider.gameObject.name.Contains("green"))
                    doubleJump = true;
            }
            else if (hitRight.collider != null)
            {
                if (hitRight.collider.gameObject.tag == "slimeInteractable" && hitRight.collider.gameObject.name.Contains("green"))
                    doubleJump = true;
            }
        }

    */
        //rb.AddForce(new Vector2(force * charMaxSpeed, rb.velocity.y));


    }

    void Update()
    {
        //Vertical Movement
        if (Input.GetButtonDown("Jump") && onGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, charJumpSpeed);
            onGround = false;
            jumpFrame = Time.time;
        }
        else if (Input.GetButtonDown("Jump") && !onGround && doubleJump && !jumpFinished)
        {
            jumpFinished = true;
            rb.velocity = new Vector2(-rb.velocity.x, charJumpSpeed);
            doubleJump = false;
        }

        //checking for basic button presses - all button input should be here
        if (Input.GetButtonDown("Attack"))
        {
            mop.GetComponent<CleanAttack>().swingMop();
        }

        //Horizontal Movement
        float force = Input.GetAxis("Horizontal");
        //Debug.Log("Force: " + force + "rby: " + rb.velocity.y);
        //an.SetFloat("Speed", Mathf.Abs(force));

        if (force > 0 && !facingRight /*&& onGround*/)
        {
            Flip();
        }
        else if (force < 0 && facingRight /*&& onGround*/)
        {
            Flip();
        }

        rb.velocity = new Vector2(force * charMaxSpeed, rb.velocity.y);
        if (!an.GetCurrentAnimatorStateInfo(0).IsName("run") && Mathf.Abs(rb.velocity.x) > 0)
        {
            an.Play("run");
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
    }

    public void Knockback(int dir)
    {
        if (dir == 1) //Knockback Left
        {
            Debug.Log("Flying Left");
            rb.velocity = new Vector2(rb.velocity.x, 30f);
            rb.velocity = new Vector2(-50f, rb.velocity.y);
        }
        else if (dir == 2) //Knockback Right
        {
            Debug.Log("Flying Right");
            rb.velocity = new Vector2(rb.velocity.x, 30f);
            rb.velocity = new Vector2(50f, rb.velocity.y);
        }
    }

    //Display the reticle and allow you to interact with it
    void ShowAim()
    {
        aiming = true;
    }

    void HideAim()
    {
        aiming = false;
    }

    public void toggleMove()
    {
        canMove = !canMove;
    }

    void ThrowSlime()
    {
        HideAim();
    }

}