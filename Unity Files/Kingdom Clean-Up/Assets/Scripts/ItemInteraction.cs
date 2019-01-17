using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour {

    private Rigidbody2D rb;
    public Vector2 initvel;
    public GameObject prefab;
    public string color;

    //Slime/World Colors
    // Green - 110, 100, 75   (32, 191, 0)     (0.12549, 0.74902, 0)
    // Blue - 190, 100, 95    (255, 25, 102)   (1, 0.09804, 0.4)
    // Red - 345, 90, 100     (0, 202, 242)    (0, 0.79216, 0.94902)
    Color cgreen = new Color(0.12549f, 0.74902f, 0f);
    Color cred = new Color(1f, 0.09804f, 0.4f);
    Color cblue = new Color(0f, 0.79216f, 0.94902f);

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
            GetComponent<SpriteRenderer>().color = cgreen;
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
        //have slime decay?
        
        //Debug.Log("TOUCHING " + other.gameObject.name);
        if (gameObject.CompareTag("slimeObject") && other.CompareTag("Player"))
        {
            //Debug.Log("IS SLIME");
            if (Input.GetButtonDown("Interact"))
            {
                //Debug.Log("IS INTER");
                if(gameObject.name.Contains("Splat"))
                {
                    GameObject.Find("DontDestroyOnLoad").GetComponent<PlayerState>().addSlime(2, color);
                    //if (color == "green")
                    //    other.GetComponent<PlayerState>().addSlime(2, "green");
                    //else if (color == "red")
                    //    other.GetComponent<PlayerState>().addSlime(2, "red");
                    //else if (color == "blue")
                    //    other.GetComponent<PlayerState>().addSlime(2, "blue");
                }
                else
                {
                    GameObject.Find("DontDestroyOnLoad").GetComponent<PlayerState>().addSlime(10, color);
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
