using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barnCameraTransition : MonoBehaviour
{
    public GameObject vcam;
    public GameObject player;
    public GameObject barn;
    public GameObject walls;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == player.name)
        {
            vcam.SetActive(false);
            //Add walls and hide barn
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == player.name)
        {
            vcam.SetActive(true);
            //Remove walls and add barn
        }
    }
}
