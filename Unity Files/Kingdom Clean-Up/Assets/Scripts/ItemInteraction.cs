using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour {

    private Rigidbody2D rb;
    public Vector2 initvel;
    public GameObject prefab;
    public string color;

    Color cgreen = Color.HSVToRGB(.110f, .100f, 0.75f);
    Color cred = Color.red;
    Color cblue = Color.blue;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        //if (color == "green")
        //{
        //    GetComponent<SpriteRenderer>().color = cgreen;
        //}
        //else if (color == "red")
        //{
        //    GetComponent<SpriteRenderer>().color = cred;
        //}
        //else if (color == "blue")
        //{
        //    GetComponent<SpriteRenderer>().color = cblue;
        //}
        //else
        //{
        //    Debug.Log("color:" + color.ToString() + color);
        //    //GetComponent<SpriteRenderer>().color = Color.black;
        //}

    }


    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Platform")
        {
           // Debug.Log("I FOUND THE GROUND!");
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX;
        }

    }

    public void setVelocity(Vector2 vel)
    {
        if(rb != null)
        {
            rb.velocity = vel;
        }
    }
    public void setColor(string col)
    {
        color = col;
        if (color == "green")
        {
            Debug.Log("I'M FUCKING GREEN");
            GetComponent<SpriteRenderer>().color = cgreen;
            Debug.Log("COLOR " + cgreen.ToString() + " MADE ME " + GetComponent<SpriteRenderer>().color.ToString());
        }
        else if (color == "red")
        {
            GetComponent<SpriteRenderer>().color = cred;
        }
        else if (color == "blue")
        {
            GetComponent<SpriteRenderer>().color = cblue;
        }
        else
        {
            Debug.Log("HELP");
        }
    }


    void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("TOUCHING " + other.gameObject.name);
        if (gameObject.CompareTag("slimeObject") && other.CompareTag("Player"))
        {
            //Debug.Log("IS SLIME");
            if (Input.GetButtonDown("Interact"))
            {
                Debug.Log("IS INTER");
                if(gameObject.name.Contains("Splat"))
                {
                    other.GetComponent<PlayerState>().addSlime(2, color);
                    //if (color == "green")
                    //    other.GetComponent<PlayerState>().addSlime(2, "green");
                    //else if (color == "red")
                    //    other.GetComponent<PlayerState>().addSlime(2, "red");
                    //else if (color == "blue")
                    //    other.GetComponent<PlayerState>().addSlime(2, "blue");
                }
                else
                {
                    other.GetComponent<PlayerState>().addSlime(10, color);
                    //if (color == "green")
                    //    other.GetComponent<PlayerState>().addSlime(10, "green");
                    //else if (color == "red")
                    //    other.GetComponent<PlayerState>().addSlime(10, "red");
                    //else if (color == "blue")
                    //    other.GetComponent<PlayerState>().addSlime(10, "blue");
                }
                Debug.Log("IS DESTROY");
                Destroy(gameObject);
            }
        }
    }
}
