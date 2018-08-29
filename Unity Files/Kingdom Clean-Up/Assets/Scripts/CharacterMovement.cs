using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    public float charSpeed = 5f;
    public float charForce = 365f;
    public float charJumpSpeed = 10f;
    public bool onGround { get; protected set; }

    Animator an;
    Rigidbody2D rb;

	// Use this for initialization
	void Start () {

        an = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        //Horizontal Movement
        if (Input.GetAxis("Horizontal") == 0) //reset to 0
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else if (Input.GetAxis("Horizontal") > 0 )
        {
            rb.velocity = new Vector2(charSpeed, rb.velocity.y);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            rb.velocity = new Vector2(-1 * charSpeed, rb.velocity.y);
        }

        //Vertical Movement
        if (Input.GetAxis("Vertical") > 0 && onGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, charJumpSpeed);
            onGround = false;
        }

        if (rb.velocity.y == 0)
        {
            onGround = true;
        }
    }
}
