using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyState : MonoBehaviour {

    int health;
    Animator an;
    public GameObject spawner = null;
    public bool facingRight = true;

	// Use this for initialization
	void Start () {
        health = 10;
        an = GetComponent<Animator>();
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

    private void OnCollisionEnter2D(Collision2D col)
    {
    
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

    }

    public void death()
    {
        if (spawner)
        {
            Debug.Log("I HAVE A SPAWNER " + spawner);
            spawner.GetComponent<SlimeSpawner>().respawn();
        }

        //spawn viscera

        Destroy(gameObject);
    }
}
