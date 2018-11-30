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
