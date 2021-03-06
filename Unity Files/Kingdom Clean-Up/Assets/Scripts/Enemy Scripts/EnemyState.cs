﻿
//    EnemyState
//    A script attached to every enemy
//    This regulates health and movement philosophies depending on what type of enemy this is attached to - allowing us to reference the same object multiple times

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyState : MonoBehaviour {

    Rigidbody2D rb;  //this object's rigidbody
    public int health;      //total health
    Animator an;     //this object's animator
    float slimeDamage;

    [Header("Debug Variables")]
    [Tooltip("The spawner it came from")]
    public GameObject spawner = null;
    [Tooltip("The color of the slime (red, green, or blue)")]
    public string color = null;
    [Tooltip("Is it facing right?")]
    public bool facingRight = true;
    [Tooltip("Can enemy move?")]
    public bool canMove;
    [Tooltip("Is the slime invulnerable")]
    public bool invulnerable = false;
    [Header("Editable Variables")]
    //[Tooltip("How fast the enemy moves")]
    //public float baseSpeed = 20f;
    [Tooltip("The buffer between the person's xy and the point, to stop aggressive wiggling")]
    public float buffer = 3f;
    [Tooltip("How high you go on knockback")]
    public float knockbackY = 30f;
    [Tooltip("How far you go on knockback")]
    public float knockbackX = 30f;
    //[Tooltip("can you knockback the player")]
    //public bool canKnockback;
    GameObject Walls;

    [Tooltip("Setting the prefab for what viscera it spawns")]
    public GameObject visceraPrefab;

    //Slime/World Colors
    // Green - 110, 100, 75   (32, 191, 0)     (0.12549, 0.74902, 0)
    // Blue - 190, 100, 95    (255, 25, 102)   (1, 0.09804, 0.4)
    // Red - 345, 90, 100     (0, 202, 242)    (0, 0.79216, 0.94902)
    Color cgreen = new Color(0.12549f, 0.74902f, 0f);
    Color cred = new Color(1f, 0.09804f, 0.4f);
    Color cblue = new Color(0f, 0.79216f, 0.94902f);


    // Initialization
    void Start () {
        //canKnockback = true;
        canMove = true;
        health = 10; // set defult health
        slimeDamage = 16.7f;
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
        if (gameObject.CompareTag("Boss"))
        {
            health = 90; //set boss health
            //slimeDamage = 34f;
        }
        else if (gameObject.CompareTag("GoopMother"))
        {
            health = 110; //set goop mother heath must be less than number of goopilings * 10, 110 is max rn
            //slimeDamage = 34f;
        }
        Walls = GameObject.FindGameObjectWithTag("WallBox");
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void setColor(string inColor)
    {
        color = inColor;
        
        if (color == "green")
        {
            GetComponentInChildren<Animator>().Play("green");
        }
        else if (color == "red")
        {
            GetComponentInChildren<Animator>().Play("red");
        }
        else if (color == "blue")
        {
            GetComponentInChildren<Animator>().Play("blue");
        }
        else
        {
            //           Debug.Log("color:" + color.ToString() + inColor);
            //GetComponent<SpriteRenderer>().color = Color.black;
            GetComponentInChildren<Animator>().Play("chromatic");
        }
    }
    public void setColorinAnimation()
    {
        if (color == "green")
        {
            GetComponentInChildren<SpriteRenderer>().color = cgreen;
        }
        else if (color == "red")
        {
            GetComponentInChildren<SpriteRenderer>().color = cred;
        }
        else if (color == "blue")
        {
            GetComponentInChildren<SpriteRenderer>().color = cblue;
        }
        Debug.Log(color + " color");
    }

    // Happens every time the slime take damage, called from the Player
    public void takeDamage(int dmg)  
    {
        if (!invulnerable)
        {
            health -= dmg;

            if (health <= 0)
            {
                death();
                //           an.Play("death"); //calls death function at end of animation
            }
        }
    }

    public void EnemyKnockback(int dir)
    {
        canMove = false;
        rb.velocity = new Vector2(dir * knockbackX, knockbackY);
    }

    // Do damage to the player when colliders hits
    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")  //If you are hit by an enemy
        {
            GameObject.Find("DontDestroyOnLoad").GetComponent<PlayerState>().takeDamage(slimeDamage); //
            Debug.Log("PLAYER HIT: " + col.gameObject.name);
            

            if (col.gameObject.transform.position.x > gameObject.transform.position.x) //If player is to the position x value - Right of slime
            {
                EnemyKnockback(-1);
                col.gameObject.GetComponent<PlayerController>().Knockback(1);
                col.gameObject.GetComponent<PlayerController>().IsInjured();
                //Debug.Log("Fly Left");
            }
            else if (col.gameObject.transform.position.x <= gameObject.transform.position.x) //If player is to the negative x value - Left of slime
            {
                EnemyKnockback(1);
                col.gameObject.GetComponent<PlayerController>().Knockback(-1);
                col.gameObject.GetComponent<PlayerController>().IsInjured();
                //Debug.Log("Fly Right");
            }

        }
        else
        {
            canMove = (true);
        }
    }

    //Finds a reference from the spawner it came from
    public void setSpawner(string spawnName)
    {
        spawner = GameObject.Find(spawnName);
    }

    public void Flip()  //Rotate the player
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
    }

    void Jump()  //Jump in a direction
    {

    }

    public void walkto(Vector3 pos, float speed)  //Move towards a position 
    {
        if(canMove)
        {
            if(pos.x - buffer > rb.position.x)  //If it is to the right of you
            {
                if (!facingRight)
                {
                    Flip();
                }

                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else if (pos.x + buffer < rb.position.x)   //If it is to the left of you
            {
                if (facingRight)
                {
                    Flip();
                }

                rb.velocity = new Vector2(-1 * speed, rb.velocity.y);
            }
            else  //Hang out
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
    }

    //A mirror of SlimeConstruct's breakSlime function
    public void death()
    {
        if (spawner)  //If the slime has a spawner, tell the spawner the slime died and to spawn another one
        {
            spawner.GetComponent<SlimeSpawner>().respawn();
        }

        int green = 0, red = 0, blue = 0;
        if(color == "Boss")
        {
            Walls.GetComponent<InvisObjects>().DisableWalls();
        }
        else if(color == "green")
            green = 3;
        else if (color == "red")
            red = 3;
        else if (color == "blue")
            blue = 3;
        else
        {
            green = 1;
            red = 1;
            blue = 1;
        }

    
        //spawn viscera
        Transform currentPos = gameObject.transform;
        int i = 1;
        while (green + red + blue > 0)
        {
            GameObject SlimeViscera = Instantiate<GameObject>(visceraPrefab, currentPos.position, currentPos.rotation);
            SlimeViscera.transform.localScale = new Vector3(2.5f, 2.5f, 0);

            if (green > 0)
            {
                green--;
                SlimeViscera.gameObject.GetComponent<ItemInteraction>().setColor("green");
            }
            else if (red > 0)
            {
                red--;
                SlimeViscera.gameObject.GetComponent<ItemInteraction>().setColor("red");
            }
            else if (blue > 0)
            {
                blue--;
                SlimeViscera.gameObject.GetComponent<ItemInteraction>().setColor("blue");
            }
            else
            {
                Debug.Log("IDK MAN");
            }


            Vector2 vel = new Vector2(30f, 15f);
            if (i % 3 == 0)
            {
                vel.x *= 0;
            }
            else if (i % 3 == 1)
            {
                vel.x *= -1;
            }

            SlimeViscera.GetComponent<ItemInteraction>().setVelocity(vel);
            i++;
        }

        // Die
        Debug.Log("i should die now");
        Destroy(gameObject);
       // Destroy(gameObject.transform.parent.gameObject); //trying to get the parent to die 
    }
}
