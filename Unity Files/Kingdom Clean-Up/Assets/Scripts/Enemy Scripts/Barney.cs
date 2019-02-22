using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barney : MonoBehaviour
{

    int hitCount;
    int health;
    float throwSlimeWait;
    public GameObject player;
    public GameObject throwSlimePrefab;
    GameObject target = null;  //Will be the player
    public float distanceToPlayer;
    public float maxRange;
    public Transform BarneySlimePos;
    public int maxSlimeAmmo;
    public float rateOfFire;
    public float launchTimer;
    int curAmmo;
    public int attackState = 0;
    public List<GameObject> tentacles = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");    //Find the player
        health = 90;
        maxSlimeAmmo = 10;
        curAmmo = 10;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector2.Distance(target.transform.position, transform.position);   //Calculate the distance between the player and yourself
        ThrowSlime();
        launchTimer -= Time.deltaTime;

        attack();
    }

    void attack()
    {
        if (attackState == 0)
        {
            //timer
        }
        else if (attackState == 1)
        {
            moveTentacle(tentacles[0], new Vector3(2778, 229, 0));
        }
        else if (attackState == 2)
        {
            moveTentacle(tentacles[0], player.transform.position);
        }
    }

    void moveTentacle(GameObject t, Vector3 pos)
    {
        Rigidbody2D end = t.transform.Find("end").gameObject.GetComponent<Rigidbody2D>();

        end.AddForce((pos - end.gameObject.transform.position).normalized * 1000);

    }

    void ThrowSlime( )
    {
        //if (distanceToPlayer < maxRange && maxSlimeAmmo > 0 )
        //{
        //        if(launchTimer <= 0)
        //        {

        //            Instantiate(throwSlimePrefab, BarneySlimePos).GetComponent<BarneySlimeBall>().playerPos = target.transform.position;
        //            //maxSlimeAmmo--;
        //            launchTimer = rateOfFire;
        //        }
        //}
        if(curAmmo > 0)
        {
            Instantiate(throwSlimePrefab, BarneySlimePos);
            curAmmo--;
        }
    }
    



}
