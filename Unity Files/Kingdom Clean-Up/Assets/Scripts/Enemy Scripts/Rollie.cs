using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rollie : MonoBehaviour
{
    public int state; // 1 - Idle, 2 - Attack, 3 - Stopping
    public int acceleration;
    public int maxSpeed;

    GameObject player;
    Rigidbody2D rb;
    float foundTimer;
    float distance;



    void Awake()
    {
        player = GameObject.Find("Player");
        rb = gameObject.GetComponent<Rigidbody2D>();
        state = 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance <= 30 && state == 1)
        {
            state = 2;
        }

        Controller();
        if (transform.position.y < -150)
            Destroy(gameObject);
    }

    void Controller()
    {
        if (state == 1) //Idle animations, etc.
        {
            
        }
        else if (state == 2) //ATTACK!!!
        {
            if (transform.position.x <= player.transform.position.x)
            {
                rb.AddForce(new Vector2(acceleration, 0));
                Debug.Log("Attack Right");
            }
            else if (transform.position.x > player.transform.position.x)
            {
                rb.AddForce(new Vector2(-acceleration, 0));
                Debug.Log("Attack Left");
            }
        }
    }
}
