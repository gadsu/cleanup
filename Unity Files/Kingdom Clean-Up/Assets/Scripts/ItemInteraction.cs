using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour {

    void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("TOUCHING " + other.gameObject.name);
        if (gameObject.CompareTag("slimeObject"))
        {
            //Debug.Log("IS SLIME");
            if (Input.GetButtonDown("Interact"))
            {
                Debug.Log("IS INTER");
                if (gameObject.name.Contains("green"))
                    other.GetComponent<PlayerStatus>().addSlime(10, "green");
                else if (gameObject.name.Contains("red"))
                    other.GetComponent<PlayerStatus>().addSlime(10, "red");
                else if (gameObject.name.Contains("blue"))
                {
                    other.GetComponent<PlayerStatus>().addSlime(10, "blue");
                }
                Debug.Log("IS DESTROY");
                Destroy(gameObject);
            }
        }
    }
}
