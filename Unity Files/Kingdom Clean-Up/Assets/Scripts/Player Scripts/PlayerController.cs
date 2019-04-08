using System.Collections;
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
    public bool canAttack;
    public GameObject mop;
    public float knockbackTime;
    public float maxSpeed;
    public float speed;
    public float fastSpeed;
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
        canAttack = true;
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
        if(isSwing)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else
        {
            move();
        }
        isCleanAnimation(); //is the player cleaning?
        if (isJumpAnimation())
        { isJump = true; }//is the player Jumping?
        an.SetBool("isInteract", isInteract);
        if(onGround)//is the player on the ground?
        { isJump = false; }
        an.SetBool("isJump", isJump);


        if (Input.GetButtonDown("Attack") && canAttack && !isFrozen)
        {

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
        an.SetBool("isSwing", isSwing);

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
            if (transform.position.x < col.gameObject.transform.position.x)
            {
                Debug.Log("Right");
                if (!facingRight)
                    Flip();
                if (freezePlayer())
                    an.Play("clingRight");
            }
            else
            {
                Debug.Log("Left");
                if (!facingRight)
                    Flip();
                if (freezePlayer())
                    an.Play("clingLeft");
            }
        }
    }


    // ------------- Movement -------------

    public void move() //Horizontal Movement
    {
         if (canMove)
         {
            rb.velocity = new Vector2(force * speed, rb.velocity.y); //Applies force to rigidbody

            if (force > 0 && !facingRight && !isFrozen) //Determine which way to face the character
            {
                Flip();
            }
            else if (force < 0 && facingRight && !isFrozen)
            {
                Flip();
            }

           

                if (Mathf.Abs(rb.velocity.x) > 0 && !isJump && isAttackAnimation() == false && onGround && isJumpAnimation() == false)
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
                /*
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
                */
                if (!Input.GetButton("Interact"))
                {
                runningMop.SetActive(false);
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
            isJump = true;
            runningMop.SetActive(false);// when the player jumps the running mop is disabled
            an.SetBool("isJump", isJump);
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
        if (Input.GetButton("Interact") && !isJump && isJumpAnimation() == false && !isFrozen)
        {
            isInteract = true;
            LayerMask layer = LayerMask.GetMask("Viscera");
            RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector2.down, 20f, layer);

            if (!isClean && !isRun)
            {
                

                

                runningMop.SetActive(false);
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

    private bool freezePlayer()
    {
        if (dontDestroy)
        {
            if (GameObject.Find("GreenGloves") && ps.greenSlimeMeter >= 10)
            {

                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                isFrozen = true;
                doubleJump = true;
                ps.useSlime();
                return true;
            }
            else
                return false;
        }
        else
        {
            if (GameObject.Find("GreenGloves"))
            {
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                isFrozen = true;
                doubleJump = true;
                return true;
            }
            else
                return false;
        }
    }

   
    // ----------------------------------

    // ------------- Checks -------------

    private void groundCheck() //Checks if touching the ground
    {
        LayerMask layer = LayerMask.GetMask("Platform");
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector2.down, 17f, layer);
        Debug.DrawRay(gameObject.transform.position, Vector2.down * 17);
        if (hit && !an.GetBool("isSwing"))
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
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else if (rb.velocity.x < -speed)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
    }

    public void setSpeed(float newSpeed)
    {
        if (newSpeed < 0)
            speed = maxSpeed;
        else
            speed = newSpeed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("Hit Trig");
        if (col.gameObject.tag == "blueSlimeAcc")//Check to see if the player entered the blueSlimeAccelerator
        {

            setSpeed(fastSpeed);//Set the speed to go faster
            //Debug.Log("HitSlime " + speed);
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        //Debug.Log("Hit Trig");
        if (col.gameObject.tag == "blueSlimeAcc")//Check to see if the player left the blueSlimeAccelerator
        {

            setSpeed(maxSpeed);//set the speed back to normal
            //Debug.Log("HitSlime " + speed);
        }
    }

    // --------------------------------------

    // ------------- Animations -------------

    public bool isJumpAnimation()//Is the player playing a jump related animation?
    {

        if (an.GetCurrentAnimatorStateInfo(0).IsName("jumpRight") ||
            an.GetCurrentAnimatorStateInfo(0).IsName("jumpLeft") ||
            an.GetCurrentAnimatorStateInfo(0).IsName("inAirRight") ||
            an.GetCurrentAnimatorStateInfo(0).IsName("inAirLeft")||
            an.GetCurrentAnimatorStateInfo(0).IsName("crouchLeft")||
            an.GetCurrentAnimatorStateInfo(0).IsName("crouchRight"))
        {
            return true;
        }
        else
        {
             return false;
        }
        
    }
    public bool isAttackAnimation()//Is the player playing an attack animation?
    {
       if(an.GetCurrentAnimatorStateInfo(0).IsName("swingLeft") ||
          an.GetCurrentAnimatorStateInfo(0).IsName("swingRight"))
        {
            return true;
        }
       else
        {
            return false;
        }
    }
    public void isCleanAnimation() //Is the player playing a clean animation?
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

    public void IsInjured() //Is the player playing the injured animation?
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
