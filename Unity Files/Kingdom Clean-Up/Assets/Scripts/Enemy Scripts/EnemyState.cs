
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
<<<<<<< HEAD

    //Slime/World Colors
    Color green = Color.HSVToRGB(110f, 100f, 75f);
    Color red = Color.red;
    Color blue = Color.blue;
=======
    public float toEdge;
    public GameObject prefab;
    Color green;
    private Rigidbody2D body;
>>>>>>> 9753f228fd21dee9af328be96dd2fc72db271b61

	// Initialization
	void Start () {
        health = 10;
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();

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
            GetComponent<SpriteRenderer>().color = green;
        }
        else if (color == "red")
        {
            GetComponent<SpriteRenderer>().color = red;
        }
        else if (color == "blue")
        {
            GetComponent<SpriteRenderer>().color = blue;
        }
        else
        {
            Debug.Log("color:" + color.ToString() + inColor);
            GetComponent<SpriteRenderer>().color = Color.black;
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


    public void death()
    {
        if (spawner)  //If the slime has a spawner, tell the spawner the slime died and to spawn another one
        {
            spawner.GetComponent<SlimeSpawner>().respawn();
        }

<<<<<<< HEAD
        //Spawn viscera
=======
        //spawn viscera
        Transform currentPos = gameObject.transform;
        //new Vector2 = currentPos.position.y;
        for(int i = 0; i < 2; i++ )
        {
            GameObject SlimeViscera = Instantiate<GameObject>(prefab, new Vector2(currentPos.position.x, (currentPos.position.y)), currentPos.rotation);
        }
        
>>>>>>> 9753f228fd21dee9af328be96dd2fc72db271b61

        // Die
        Destroy(gameObject);
    }
}
