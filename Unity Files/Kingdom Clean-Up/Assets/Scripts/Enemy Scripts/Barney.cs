using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barney : MonoBehaviour
{

    [Header("Debug Variables")]
    public float distanceToPlayer;
    public Transform BarneySlimePos;
    public float launchTimer;
    public int curAmmo;
    public int attackState = 0;
    [Header("Editable Variables")]
    public GameObject player;
    public GameObject throwSlimePrefab;
    public float maxRange;
    public int maxSlimeAmmo = 10;
    public float rateOfFire;
    public List<GameObject> tentacles = new List<GameObject>();
    public GameObject attTentacle;

    GameObject target = null;  //Will be the player
    int hitCount;
    int health;
    float throwSlimeWait = 0;
    System.Random rnd = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");    //Find the player
        curAmmo = 10;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector2.Distance(target.transform.position, transform.position);   //Calculate the distance between the player and yourself
        //ThrowSlime();
        launchTimer -= Time.deltaTime;

        controller();
    }

    void controller()
    {
        if (attackState == 0 && player.transform.position.x <= gameObject.transform.position.x) //If player is left of Barney and not attacking
        {
            attTentacle = tentacles[rnd.Next(2)];
            attackState = 1;
        }
        else if (attackState == 0 && player.transform.position.x > gameObject.transform.position.x)//If player is right of Barney and not attacking
        {
            attTentacle = tentacles[rnd.Next(2, 4)];
            attackState = 1;
        }

        if (attackState != 0)
        {
            throwSlimeWait += Time.fixedDeltaTime;
            Debug.Log(throwSlimeWait);
        }
        if (throwSlimeWait >= 3f && attackState == 1)
        {
            attackState = 2;
        }
        if (throwSlimeWait >= 3.5f && attackState == 2)
        {
            Instantiate(throwSlimePrefab, attTentacle.transform.Find("end").position, new Quaternion());
            attackState = 3; //Cooldown phase
        }
        if (throwSlimeWait >= 5f)
        {
            attackState = 0;
            throwSlimeWait = 0;
        }

        attack(attTentacle);
    }

    void attack(GameObject tent)
    {
        if (attackState == 0)
        {
            //timer
        }
        else if (attackState == 1)
        {
            moveTentacle(tent, new Vector3(2778, 229, 0));
        }
        else if (attackState == 2)
        {
            moveTentacle(tent, player.transform.position);
        }
    }

    void moveTentacle(GameObject t, Vector3 pos)
    {
        Rigidbody2D end = t.transform.Find("end").gameObject.GetComponent<Rigidbody2D>();

        end.AddForce((pos - end.gameObject.transform.position).normalized * 1000);

    }

    void ThrowSlime( )
    {
        if(curAmmo > 0)
        {
            Instantiate(throwSlimePrefab, BarneySlimePos);
            curAmmo--;
        }
    }
    



}
