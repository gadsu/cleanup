
//    EnemyState
//    A script attached to every enemy
//    This regulates health and movement philosophies depending on what type of enemy this is attached to - allowing us to reference the same object multiple times

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyState : MonoBehaviour {

    Rigidbody2D rb;  //this object's rigidbody
    int health;      //total health
    Animator an;     //this object's animator
    float slimeDamage;

    [Header("Debug Variables")]
    [Tooltip("The spawner it came from")]
    public GameObject spawner = null;
    [Tooltip("The color of the slime (red, green, or blue)")]
    public string color = null;
    [Tooltip("Is it facing right?")]
    public bool facingRight = true;

    [Header("Editable Variables")]
    [Tooltip("How fast the enemy moves")]
    public float speed = 20f;
    [Tooltip("The buffer between the person's xy and the point, to stop aggressive wiggling")]
    public float buffer = 3f;



    [Tooltip("Setting the prefab for what viscera it spawns")]
    public GameObject visceraPrefab;


    //Slime/World Colors
    Color cgreen = Color.HSVToRGB(110f * 0.1f, 100f*0.1f, 75f*0.1f);
    Color cred = Color.red;
    Color cblue = Color.blue;


	// Initialization
	void Start () {
        health = 10;
        slimeDamage = 16.7f;
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
        if (gameObject.CompareTag("Boss"))
        {
            health = 90;
            slimeDamage = 33.4f;
        }
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void setColor(string inColor)
    {
        color = inColor;

        GetComponent<Animator>().enabled = false;
        if (color == "green")
        {
            GetComponent<SpriteRenderer>().color = cgreen;
        }
        else if (color == "red")
        {
            GetComponent<SpriteRenderer>().color = cred;
        }
        else if (color == "blue")
        {
            GetComponent<SpriteRenderer>().color = cblue;
        }
        else
        {
 //           Debug.Log("color:" + color.ToString() + inColor);
            //GetComponent<SpriteRenderer>().color = Color.black;
        }
        GetComponent<Animator>().enabled = true;
    }
    // Happens every time the slime take damage, called from the Player
    public void takeDamage(int dmg)  
    {
        health -= dmg;
        if(health <= 0)
        {
            an.Play("death"); //calls death function at end of animation
        }
    }
    // Do damage to the player when colliders hits
    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")  //If you are hitting an enemy
        {
            col.gameObject.GetComponent<PlayerState>().takeDamage(slimeDamage); //
            Debug.Log("PLAYER HIT: " + col.gameObject.name);

        }
    }

    //Finds a reference from the spawner it came from
    public void setSpawner(string spawnName)
    {
        spawner = GameObject.Find(spawnName);
    }

    void Flip()  //Rotate the player
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
    }

    void Jump()  //Jump in a direction
    {

    }

    public void walkto(Vector3 pos)  //Move towards a position 
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

    //A mirror of SlimeConstruct's breakSlime function
    public void death()
    {
        if (spawner)  //If the slime has a spawner, tell the spawner the slime died and to spawn another one
        {
            spawner.GetComponent<SlimeSpawner>().respawn();
        }

        int green = 0, red = 0, blue = 0;
        if (color == "green")
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

            if (green > 0)
            {
                green--;
                SlimeViscera.gameObject.GetComponent<ItemInteraction>().setColor("green");
            }
            else if (red > 0)
            {
                red--;
                //SlimeViscera.gameObject.GetComponent<ItemInteraction>().setColor("red");
            }
            else if (blue > 0)
            {
                blue--;
                //SlimeViscera.gameObject.GetComponent<ItemInteraction>().setColor("blue");
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
        Destroy(gameObject);
    }
}
