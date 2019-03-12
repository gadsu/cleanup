using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barney : MonoBehaviour
{

    [Header("Debug Variables")]
    public float distanceToPlayer;
    public Transform BarneySlimePos;
    public float launchTimer;
    public int attackState = 0;

    [Header("Editable Variables")]
    public GameObject player;
    public GameObject throwSlimePrefab;
    public int hitCount;
    public float maxRange;
    public float rateOfFire;
    public List<GameObject> tentacles = new List<GameObject>();
    public GameObject attTentacle;
    public GameObject leftSlimeCover;
    public GameObject rightSlimeCover;

    GameObject target = null;  //Will be the player
    int health;
    float throwSlimeWait = 0;
    System.Random rnd = new System.Random();
    EnemyState es;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");    //Find the player
        es = gameObject.GetComponent<EnemyState>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector2.Distance(target.transform.position, transform.position);   //Calculate the distance between the player and yourself
        //ThrowSlime();
        launchTimer -= Time.deltaTime;

        controller();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "mopAttack")
        {
            hitCount++;
        }
    }

    void controller()
    {
        if (hitCount >= 3)
        {
            if (player.transform.position.x >= gameObject.transform.position.x)
            {
                StartCoroutine(coverSlime(rightSlimeCover));
            }

            else if (player.transform.position.x < gameObject.transform.position.x)
            {
                StartCoroutine(coverSlime(leftSlimeCover));
            }

            hitCount = 0;
        }
        if (attackState == 0 && player.transform.position.x <= gameObject.transform.position.x) //If player is left of Barney and not attacking
        {
            attTentacle = tentacles[rnd.Next(3)];
            attackState = 1;
        }
        else if (attackState == 0 && player.transform.position.x > gameObject.transform.position.x)//If player is right of Barney and not attacking
        {
            attTentacle = tentacles[rnd.Next(3, 6)];
            attackState = 1;
        }

        if (attackState != 0)
        {
            throwSlimeWait += Time.fixedDeltaTime;
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

        foreach (GameObject tentacle in tentacles)
        {
            if (tentacle != attTentacle)
            {
                moveTentacle(tentacle, new Vector3(tentacle.transform.position.x, tentacle.transform.position.y + rnd.Next(-30, 30), 0));
            }
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
            moveTentacle(tent, new Vector3(tent.transform.position.x, tent.transform.position.y + 30, 0));
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

    IEnumerator coverSlime(GameObject cover) //1 - Left, 2 - Right
    {
        es.invulnerable = true;

        yield return new WaitForSeconds(3);
        foreach (Transform section in cover.transform)
        {
            yield return new WaitForSeconds(0.1f);
            section.gameObject.SetActive(true);
        }

        es.invulnerable = false;
    }
}
