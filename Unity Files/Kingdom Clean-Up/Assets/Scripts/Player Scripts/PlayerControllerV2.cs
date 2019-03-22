﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerV2 : MonoBehaviour
{
    public bool onGround;
    public bool facingRight;
    public bool isClean;
    public bool isRun;
    public bool isJump;
    public bool isInteract;
    public bool doubleJump;
    public bool aiming;
    public bool canMove;
    public GameObject mop;
    public float knockbackTime;
    public float maxSpeed = 40f;
    public float jumpSpeed = 60f;
    public float knockbackY = 30f;
    public float knockbackX = 30f;

    Animator an;
    Rigidbody2D rb;
    GameObject runningMop;
    float jumpFrame;
    float force = 0;


    private void Awake()
    {
        an = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        runningMop = gameObject.transform.Find("mopRun").gameObject;
    }
    private void Start()
    {
        runningMop.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        force = Input.GetAxis("Horizontal");

        jump();
        interact();
        isCleanAnimation(); //is the player cleaning?
        isJumpAnimation(); //is the player Jumping?
        an.SetBool("isInteract", isInteract);

        if (Input.GetButtonDown("Attack"))
        {
            mop.GetComponent<CleanAttack>().swingMop();
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

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("slimeInteractable") && GameObject.Find("DontDestroyOnLoad").GetComponent<PlayerState>().useSlime())
        {
            doubleJump = true;
        }

    }

    // ------------- Movement -------------

    public void move()
    {
         if (canMove)
        {
            rb.AddForce(transform.right * force); //Applies force to rigidbody

            if (force > 0 && !facingRight) //Determine which way to face the character
            {
                Flip();
            }
            else if (force < 0 && facingRight)
            {
                Flip();
            }
        }
    }

    public void jump()
    {
        if (Input.GetButtonDown("Jump") && (onGround || doubleJump))
        {
            rb.AddForce(transform.up * jumpSpeed);
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

    public void interact()
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

    public void Knockback(int dir)
    {
        canMove = false;
        knockbackTime = 3f;
        rb.velocity = new Vector2(dir * knockbackX, knockbackY);
    }

    // ------------- Checks -------------

    private void groundCheck()
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
}