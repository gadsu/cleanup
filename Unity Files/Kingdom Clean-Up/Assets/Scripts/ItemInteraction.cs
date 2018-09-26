using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour {

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
