﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool onGround;
    public bool facingRight;
    public bool isClean;
    public bool isRun;
    public bool isJump;
    public bool isSwing;
    public bool isInteract;
    public bool doubleJump;
    public bool aiming;
    public bool canMove;
    public GameObject mop;
    public float knockbackTime;
    public float maxSpeed = 200f;
    public float speed;
    public float jumpSpeed = 60f;
    public float knockbackY = 30f;
    public float knockbackX = 30f;

    Animator an;
    Rigidbody2D rb;
    GameObject runningMop;
    PlayerState ps;
    float jumpFrame;
    float force = 0;
    bool isFrozen = false;
    bool dontDestroy = false;


    private void Awake()
    {
        if (GameObject.Find("DontDestroyOnLoad"))
        {
            dontDestroy = true;
            ps = GameObject.Find("DontDestroyOnLoad").GetComponent<PlayerState>();
        }

        an = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        runningMop = gameObject.transform.Find("mopRun").gameObject;
        speed = maxSpeed;
    }
    private void Start()
    {
        runningMop.SetActive(false);
            
    }

    // Update is called once per frame
    private void Update()
    {
        force = Input.GetAxisRaw("Horizontal");

        jump();
        interact();
        move();
        isCleanAnimation(); //is the player cleaning?
        isJumpAnimation(); //is the player Jumping?
        an.SetBool("isInteract", isInteract);
        an.SetBool("isSwing", isSwing);

        if (Input.GetButtonDown("Attack"))
        {
            isSwing = true;

            if (facingRight)
            {
                an.Play("swingRight");
            }
            else
            {
                an.Play("swingLeft");
            }
            
            mop.GetComponent<CleanAttack>().swingMop();
        }
        else
        {
            isSwing = false;
        }
        

        if (isRun)
        {
            runningMop.SetActive(false);
        }

        if (knockbackTime > 0)
        {
            knockbackTime = knockbackTime - Time.deltaTime;

            if (knockbackTime < 1)
            {
                canMove = true;
                knockbackTime = 0;
            }
        }
        if (Mathf.Abs(rb.velocity.x) <= 0 || isJump)
        {
            isRun = false;
        }
        an.SetBool("isRun", isRun);
    }

    private void FixedUpdate()
    {
        groundCheck(); //is the player on the ground?
        constrain();

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "slimeInteractable")
        {
            freezePlayer();
        }
    }

    // ------------- Movement -------------

    public void move() //Horizontal Movement
    {
         if (canMove)
         {
            rb.velocity = new Vector2(force * speed, rb.velocity.y); //Applies force to rigidbody

            if (force > 0 && !facingRight) //Determine which way to face the character
            {
                Flip();
            }
            else if (force < 0 && facingRight)
            {
                Flip();
            }

           

                if (Mathf.Abs(rb.velocity.x) > 0 && !isJump)
                {
                    isRun = true;
                    if (facingRight)
                    {
                        if (!an.GetCurrentAnimatorStateInfo(0).IsName("runRight") && !(Input.GetButton("Interact")))
                        {
                            an.Play("runRight");
                        }
                        else if (!an.GetCurrentAnimatorStateInfo(0).IsName("runRight") && (Input.GetButton("Interact")))
                        {
                            runningMop.SetActive(true);
                            an.Play("mopRunRight");
                        }
                        else
                        {
                            runningMop.SetActive(false);
                        }

                    }
                    else
                    {
                        if (!an.GetCurrentAnimatorStateInfo(0).IsName("runLeft") && !(Input.GetButton("Interact")))
                        {
                            an.Play("runLeft");
                        }
                        else if (!an.GetCurrentAnimatorStateInfo(0).IsName("runLeft") && (Input.GetButton("Interact")))
                        {
                            runningMop.SetActive(true);
                            an.Play("mopRunLeft");
                        }
                        else
                        {
                            runningMop.SetActive(false);
                        }

                    }

                }

                if ((Input.GetAxis("Horizontal")) == 0 && onGround)
                {
                    if (facingRight)
                    {
                        if (!an.GetCurrentAnimatorStateInfo(0).IsName("idle") && !isJump && !isClean)
                        {
                            an.Play("idle");
                        }
                    }
                    else
                    {
                        if (!an.GetCurrentAnimatorStateInfo(0).IsName("idleLeft") && !isJump && !isClean)
                        {
                            an.Play("idleLeft");
                        }
                    }
                }


         }

         if (Mathf.Abs(rb.velocity.x) <= 0 || isJump)
         {
            isRun = false;
         }
         an.SetBool("isRun", isRun);
        
    }

    public void jump() //Vertical Movement
    {
        if (Input.GetButtonDown("Jump") && (onGround || doubleJump))
        {
            if (isFrozen)
            {
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                isFrozen = false;
            }

            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            onGround = false;
            doubleJump = false;
            jumpFrame = Time.time;
            if (facingRight)
            {
                an.Play("jumpRight");
            }
            else
            {
                an.Play("jumpLeft");
            }
        }
    }

    public void interact() //Interact/Clean
    {
        if (Input.GetButton("Interact") && !isJump)
        {
            isInteract = true;

            LayerMask layer = LayerMask.GetMask("Viscera");

            RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector2.down, 20f, layer);
            if (!isClean && !isRun)
            {
                if (facingRight)
                {
                    an.Play("mopRight");
                }
                else
                {

                    an.Play("mopLeft");
                }

            }

            if (hit)
            {
                if (hit.collider.gameObject.CompareTag("slimeObject"))
                {
                    GameObject.Find("DontDestroyOnLoad").GetComponent<PlayerState>().addSlime(10, hit.transform.gameObject.GetComponent<ItemInteraction>().color);
                    GameObject.Find("DontDestroyOnLoad").GetComponent<PlayerState>().groundSlimeCleaned++;
                    GameObject.Find("DontDestroyOnLoad").GetComponent<PlayerState>().updateCleanProgress();
                    Destroy(hit.transform.gameObject);
                }
            }


        }
        else
        {
            isInteract = false;
        }
    }

    public void Knockback(int dir) //Throw the player back
    {
        canMove = false;
        knockbackTime = 3f;
        rb.velocity = new Vector2(dir * knockbackX, knockbackY);
    }

    private void freezePlayer()
    {
        if (dontDestroy)
        {
            if (GameObject.Find("GreenGloves") && ps.greenSlimeMeter >= 10)
            {
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                isFrozen = true;
                doubleJump = true;
                ps.useSlime();
            }
        }
        else
        {
            if (GameObject.Find("GreenGloves"))
            {
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                isFrozen = true;
                doubleJump = true;
            }
        }
    }

    // ----------------------------------

    // ------------- Checks -------------

    private void groundCheck() //Checks if touching the ground
    {
        LayerMask layer = LayerMask.GetMask("Platform");
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector2.down, 17f, layer);
        Debug.DrawRay(gameObject.transform.position, Vector2.down * 17);
        if (hit)
        {
            onGround = true;
            canMove = true;
            doubleJump = false;
        }
        else
        {
            onGround = false;
        }
        an.SetBool("onGround", onGround);
    }

    private void constrain() //Constrains movement
    {
        if (rb.velocity.x > speed)
        {
            rb.velocity = new Vector2(speed, rb.velocity.x);
        }
        else if (rb.velocity.x < -speed)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.x);
        }
    }

    public void setSpeed(float newSpeed)
    {
        if (newSpeed < 0)
            speed = maxSpeed;
        else
            speed = newSpeed;
    }

    // --------------------------------------

    // ------------- Animations -------------

    public void isJumpAnimation()
    {

        if (
            an.GetCurrentAnimatorStateInfo(0).IsName("jumpRight") ||
            an.GetCurrentAnimatorStateInfo(0).IsName("jumpLeft") ||
            an.GetCurrentAnimatorStateInfo(0).IsName("inAirRight") ||
            an.GetCurrentAnimatorStateInfo(0).IsName("inAirLeft")
            )
        {
            isJump = true;
        }
        else
        {
            isJump = false;
        }
    }

    public void isCleanAnimation()
    {
        if (an.GetCurrentAnimatorStateInfo(0).IsName("mopLeft") || an.GetCurrentAnimatorStateInfo(0).IsName("mopRight"))
        {
            isClean = true;
        }
        else
        {
            isClean = false;

        }
        if (isInteract)
        {

        }
        an.SetBool("isClean", isClean);
    }

    public void IsInjured()
    {
        if (!an.GetCurrentAnimatorStateInfo(0).IsName("injured"))
        {
            an.Play("injured");
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        an.SetBool("facingRight", facingRight);
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
    }

    // --------------------------------------
}
