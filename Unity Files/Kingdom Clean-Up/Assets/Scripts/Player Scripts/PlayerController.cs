
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
    [Tooltip("How long the player is off the ground")]
    public float groundTimer;
    [Tooltip("Is player facing right?")]
    public bool facingRight;
    [Tooltip("Is player cleaning?")]
    public bool isClean;
    [Tooltip("Is player running?")]
    public bool isRun;
    [Tooltip("Is player Jumping?")]
    public bool isJump;
    [Tooltip("Is player pressing Interact?")]
    public bool isInteract;
    [Tooltip("Can player doublejump?")]
    public bool doubleJump;
    [Tooltip("Is player aiming?")]
    public bool aiming;
    [Tooltip("Can player move?")]
    public bool canMove;
    [Tooltip("The mop object")]
    public GameObject mop;
    [Tooltip("How long the player is unable to move from knockback")]
    public float knockbackTime;

    public bool ignoreCollision;

    public List<GameObject> ignoredObjects;

    [Header("Editable Variables")]
    [Tooltip("How fast can the character move?")]
    public float charMaxSpeed = 40f;
    [Tooltip("How much can the character jump?")]
    public float charJumpSpeed = 60f;
    [Tooltip("How high you go on knockback")]
    public float knockbackY = 30f;
    [Tooltip("How far you go on knockback")]
    public float knockbackX = 30f;
    [Tooltip("Sprite to show when running and moping")]
    GameObject runningMop; 

    bool startTimer;
    float jumpFrame;
    float force;

    Animator an;
    Rigidbody2D rb;
    //   AimRender ar;

    // Use this for initialization
    void Start()
    {
        runningMop = GameObject.Find("mopRun");
        runningMop.SetActive(false);
        
        onGround = true;
        an = GetComponent<Animator>();
        
        rb = GetComponent<Rigidbody2D>();
        //       ar = GetComponent<AimRender>();
        isRun = false;
        aiming = false;
        doubleJump = false;
        facingRight = false;
        canMove = true;
        ignoreCollision = false;
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        /* if (col.tag == "Platform" && !onGround)
         {
             //col.transform.position
             onGround = true;
                        Debug.Log("onground = true");
         } */
        if (col.CompareTag("slimeInteractable") && GameObject.Find("DontDestroyOnLoad").GetComponent<PlayerState>().useSlime())
        {
            Debug.Log("Hit a slime wall");
            doubleJump = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag != "slimeInteractable")
        {
            doubleJump = false;
        }
    }

    private void FixedUpdate()
    {
        groundCheck();//is the player on the ground?
        
    }

    void Update()
    {
        jump(); //made own function as we can call it in other places
        
        if(isRun)
        {
            runningMop.SetActive(false);
        }

        if (Input.GetButton("Interact") && !isJump)
        {
            isInteract = true;

            RaycastHit2D hit;
            hit = Physics2D.Raycast(gameObject.transform.position, Vector2.down, 20f);
            if (!isClean && !isRun)
            {
                if(facingRight)
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
        isCleanAnimation(); //is the player cleaning?
        isJumpAnimation();//is the player Jumping?

        an.SetBool("isInteract", isInteract);

        if (knockbackTime > 0)
        {
            knockbackTime = knockbackTime - Time.deltaTime;

            if (knockbackTime < 1)
            {
                canMove = true;
                knockbackTime = 0;
            }
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

     


        if (canMove)
        {
            if (force > 0 && !facingRight /*&& onGround*/)
            {
                Flip();
            }
            else if (force < 0 && facingRight /*&& onGround*/)
            {
                Flip();
            }
            
            rb.velocity = new Vector2(force * charMaxSpeed, rb.velocity.y);
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
                    if (!an.GetCurrentAnimatorStateInfo(0).IsName("runLeft") && !(Input.GetButton("Interact")) )
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

            if ((Input.GetAxis("Horizontal")) == 0)
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

    public void IsInjured()
    {
        if (!an.GetCurrentAnimatorStateInfo(0).IsName("injured"))
        {
            an.Play("injured");
        }
    }

    private void groundCheck()
    {
        LayerMask layer = LayerMask.GetMask("Platform");
        RaycastHit2D hit;
        hit = Physics2D.Raycast(gameObject.transform.position, Vector2.down, 17f, layer);
        Debug.DrawRay(gameObject.transform.position, Vector2.down * 17);
        if (hit)
        {
          //  Debug.Log("FLOOR");
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
    public void jump()
    {
        //Vertical Movement
        if (Input.GetButtonDown("Jump") && onGround)// && onGround
        {
            rb.velocity = new Vector2(rb.velocity.x, charJumpSpeed);
            onGround = false;
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
        else if (Input.GetButtonDown("Jump") && !onGround && doubleJump)
        {
            rb.velocity = new Vector2(-rb.velocity.x, charJumpSpeed);
            doubleJump = false;
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
    
    public void isJumpAnimation()
    {
        
        if (
            an.GetCurrentAnimatorStateInfo(0).IsName("jumpRight") ||
            an.GetCurrentAnimatorStateInfo(0).IsName("jumpLeft")  ||
            an.GetCurrentAnimatorStateInfo(0).IsName("inAirRight")||
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
        if( an.GetCurrentAnimatorStateInfo(0).IsName("mopLeft")|| an.GetCurrentAnimatorStateInfo(0).IsName("mopRight"))
        {
            isClean = true;
        }
        else
        {
            isClean = false;
            
        }
        if(isInteract)
        {
         
        }
        an.SetBool("isClean", isClean);
    }
    

    void Flip()
    {
        facingRight = !facingRight;
        an.SetBool("facingRight", facingRight);
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
    }

    public void Knockback(int dir)
    {
        canMove = false;
        knockbackTime = 3f;
        rb.velocity = new Vector2(dir * knockbackX, knockbackY);
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