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

        an = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        //Horizontal Movement
        float force = Input.GetAxis("Horizontal");
        Debug.Log("Force: " + force + "rby: " + rb.velocity.y);
        an.SetFloat("Speed", Mathf.Abs(force));

        if (force > 0 && !facingRight && onGround)
        {
            Flip();
        }
        else if (force < 0 && facingRight && onGround)
        {
            Flip();
        }
        /*        if (!onGround)  //tabling this for now
                {
                    if ((facingRight && force < 0) || (!facingRight && force > 0))
                    {
                        if (rb.velocity.y > -3f)
                        {
                            force = rb.velocity.x / charMaxSpeed;
                        }
                        else
                        {
                            force = force * 0.4f + (rb.velocity.x / charMaxSpeed);
                            force = Mathf.Clamp(force, -0.6f, 0.6f);
                        }
                    }
                } */


        if (onGround)
        {
            rb.velocity = new Vector2(force * charMaxSpeed, rb.velocity.y);
        }

        if (rb.velocity.y == 0)
        {
            onGround = true;
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
