using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawner : MonoBehaviour {

    public GameObject prefab;
    float spawnTime;
    public bool slimeAlive;
    public int slimeCount;
    public int maxSlime = 1;
    public int totalSlime = 4;
    float timeDelay = 3f;

	// Use this for initialization
	void Start ()
    {
        slimeCount = 1;
        respawn();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if ((Time.fixedTime > (spawnTime + timeDelay)) && (slimeCount < maxSlime) && totalSlime != 0)
        {
            slimeCount++;
            Transform startpos = gameObject.transform;
            GameObject SlimeEnemy = Instantiate<GameObject>(prefab, startpos.position, startpos.rotation);
            //SlimeEnemy.GetComponent<EnemyState>().spawner = this.gameObject;
            SlimeEnemy.GetComponent<EnemyState>().setSpawner(gameObject.name.ToString());
            //SlimeEnemy.GetComponent<SpriteRenderer>().color = Color.red;
            spawnTime = Time.fixedTime;
            totalSlime--;
            
        }
	}

    public void respawn()
    {
        spawnTime = Time.fixedTime;
        slimeCount--;
    }
}
