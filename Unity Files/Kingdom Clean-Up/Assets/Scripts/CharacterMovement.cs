/*
 * Moving the player character and reading inputs
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    public float charMaxSpeed = 5f;
    public float charJumpSpeed = 10f;
    public bool onGround;
    public bool facingRight = true;

    Animator an;
    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        onGround = true;
        //an = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        //Horizontal Movement
        float force = Input.GetAxis("Horizontal");
        //Debug.Log("Force: " + force + "rby: " + rb.velocity.y);
        //an.SetFloat("Speed", Mathf.Abs(force));

        if (force > 0 && !facingRight && onGround)
        {
            Flip();
        }
        else if (force < 0 && facingRight && onGround)
        {
            Flip();
        }

       // RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f);
        //RaycastHit2D hit = Physics2D.Raycast()
        //Debug.DrawRay(transform.position, Vector2.down * 1f);
        /*if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Platform")
                onGround = true;
        }
        */


        if (!onGround)  //tabling this for now
        {
              if ((facingRight && force < 0) || (!facingRight && force > 0))
              {
                  
                      force = force * 0.4f + (rb.velocity.x / charMaxSpeed);
                      force = Mathf.Clamp(force, -0.6f, 0.6f);
                  
              }
         } 


            rb.velocity = new Vector2(force * charMaxSpeed, rb.velocity.y);
        
    }
    /*
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Platform" && !onGround)
        {
            col.transform.position
            onGround = true;
 //           Debug.Log("onground = true");
        }
    }
    */
    private void OnTriggerExit2D(Collider2D col)
    {
        if (onGround == true && col.tag == "Platform")
        {
            onGround = false;
        }
    }

    private void Update()
    {
        //Vertical Movement
        if ((Input.GetAxis("Vertical") > 0 || Input.GetButtonDown("Jump")) && onGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, charJumpSpeed);
            onGround = false;
        }
        
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
    }
}
