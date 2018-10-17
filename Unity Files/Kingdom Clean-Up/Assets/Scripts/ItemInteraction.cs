using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour {

    private Rigidbody2D rb;
    public Vector2 initvel;
    public int slimeInteractableHealth;
    public GameObject prefab;


    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Platform")
        {
        Debug.Log("I FOUND THE GROUND!");
            rb = GetComponent<Rigidbody2D>();
        //       body.bodyType = RigidbodyType2D.Static;
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        }

    }

    public void takeDamage(int dmg)
    {
        slimeInteractableHealth-= dmg;
        if (slimeInteractableHealth <= 0)
        {
            Transform currentPos = gameObject.transform;
            for (int i = 0; i < 2; i++)
            {
                GameObject SlimeViscera = Instantiate<GameObject>(prefab, new Vector2(currentPos.position.x, (currentPos.position.y)), currentPos.rotation);
            }
            Destroy(gameObject);
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
                    if (gameObject.name.Contains("green"))
                        other.GetComponent<PlayerState>().addSlime(2, "green");
                    else if (gameObject.name.Contains("red"))
                        other.GetComponent<PlayerState>().addSlime(2, "red");
                    else if (gameObject.name.Contains("blue"))
                    {
                        other.GetComponent<PlayerState>().addSlime(2, "blue");

                    }
                }
                else
                {
                    if (gameObject.name.Contains("green"))
                        other.GetComponent<PlayerState>().addSlime(10, "green");
                    else if (gameObject.name.Contains("red"))
                        other.GetComponent<PlayerState>().addSlime(10, "red");
                    else if (gameObject.name.Contains("blue"))
                    {
                        other.GetComponent<PlayerState>().addSlime(10, "blue");

                    }
                }
                Debug.Log("IS DESTROY");
                Destroy(gameObject);
            }
        }
    }
}
