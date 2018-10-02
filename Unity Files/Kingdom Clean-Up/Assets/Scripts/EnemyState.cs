using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour {

    int health;
    Animator an;
    public GameObject spawner = null;

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
