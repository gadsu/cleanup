using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeCover : MonoBehaviour
{
    public float slowSpeed = 10f;
    PlayerController pc;

    private void Awake()
    {
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.name == "Player")
        {
            Debug.Log("Should Be SLOW");
            Rigidbody2D rb = col.GetComponent<Rigidbody2D>();

            if (rb.velocity.x > slowSpeed)
                rb.velocity = new Vector2(slowSpeed, rb.velocity.y);
            else if (rb.velocity.x < -slowSpeed)
                rb.velocity = new Vector2(-slowSpeed, rb.velocity.y);
            else if (rb.velocity.y > slowSpeed)
                rb.velocity = new Vector2(rb.velocity.x, slowSpeed);
            else if (rb.velocity.y < -slowSpeed)
                rb.velocity = new Vector2(rb.velocity.x, -slowSpeed);

            pc.setSpeed(slowSpeed);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.name == "Player")
        {
            pc.setSpeed(-1f);
        }
    }
}
