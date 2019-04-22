//-------------COMPLETE DO NOT TOUCH----------//


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawner : MonoBehaviour {

    float spawnTime;

    [Header("Debug Variables")]
    [Tooltip("Is there a slime alive?")]
    public bool slimeAlive;
    [Tooltip("# of slime alive that have been spawned by this spawner")]
    public int slimeCount;

    [Header("Editable Variables")]
    [Tooltip("The type of enemy to spawn")]
    public GameObject enemyPrefab;
    [Tooltip("The color of the slime to spawn (red, green, blue, default)")]
    public string color;
    [Tooltip("Maximum number of slime that can be alive at one time")]
    public int maxSlime = 1;
    [Tooltip("Total number of slime this spawner can create")]
    public int totalSlime = 1;
    [Tooltip("The time inbetween a slime's death and a slime spawn (or, time inbetween spawnings)")]
    public float timeDelay = 3f;

    List<Transform> patrolPoints = new List<Transform>();

	// Use this for initialization
	void Start ()
    {
        slimeCount = 1;
        respawn();

        foreach(Transform point in transform)
        {
            patrolPoints.Add(point);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if ((Time.fixedTime > (spawnTime + timeDelay)) && (slimeCount < maxSlime) && totalSlime != 0)  //If it has been enough time, and there aren't the max of slimes already spawned, and you are not out of total slimes...
        {
            slimeCount++; //Increment counter
            Transform startpos = gameObject.transform;
            GameObject newSlimeEnemy = Instantiate<GameObject>(enemyPrefab, startpos.position, startpos.rotation);  //Create slime
            newSlimeEnemy.GetComponent<EnemyState>().setSpawner(gameObject.name.ToString());  //Set spawner
            newSlimeEnemy.GetComponent<EnemyState>().setColor(color);   //Set color
            newSlimeEnemy.name = name.ToCharArray()[0].ToString() + newSlimeEnemy.name;  //Name

            if (newSlimeEnemy.GetComponent<AIFollow>())
            {
                newSlimeEnemy.GetComponent<AIFollow>().targetArr = patrolPoints;
            }

            spawnTime = Time.fixedTime;  //reset timer
            totalSlime--;
        }
	}

    public void respawn()
    {
        spawnTime = Time.fixedTime;  //reset timer so it doesn't instantly respawn
        slimeCount--;
    }

}
