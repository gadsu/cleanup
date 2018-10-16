using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyState : MonoBehaviour {

    Rigidbody2D rb;
    int health;
    Animator an;
    public GameObject spawner = null;
    public bool facingRight = true;
    public float speed = 20f;
    public float buffer = 3f;
    public float toEdge;
    public GameObject prefab;
    Color green;
    private Rigidbody2D body;

	// Use this for initialization
	void Start () {
        health = 10;
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
        green = Color.HSVToRGB(110f, 100f, 75f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void takeDamage(int dmg)
    {
        health -= dmg;
        if(health <= 0)
        {
            an.Play("death"); //calls death function at end of animation
        }
    }

    public void setSpawner(string spawnName)
    {
        spawner = GameObject.Find(spawnName);
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
    }

    void Jump()
    {

    }

    public void walkto(Vector3 pos)
    {
        if(pos.x - buffer > rb.position.x)
        {
            if (!facingRight)
            {
              //  Flip();
            }

            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else if (pos.x + buffer < rb.position.x)
        {
            if (facingRight)
            {
               // Flip();
            }

            rb.velocity = new Vector2(-1 * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }


    public void death()
    {
        if (spawner)
        {
            Debug.Log("I HAVE A SPAWNER " + spawner);
            spawner.GetComponent<SlimeSpawner>().respawn();
        }

        //spawn viscera
        Transform currentPos = gameObject.transform;
        //new Vector2 = currentPos.position.y;
        for(int i = 0; i < 2; i++ )
        {
            GameObject SlimeViscera = Instantiate<GameObject>(prefab, new Vector2(currentPos.position.x, (currentPos.position.y)), currentPos.rotation);
        }
        

        // Die
        Destroy(gameObject);
    }
}
