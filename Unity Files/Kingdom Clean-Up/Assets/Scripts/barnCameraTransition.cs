using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barnCameraTransition : MonoBehaviour
{
    public GameObject vcam;
    public GameObject player;

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
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == player.name)
        {
            vcam.SetActive(true);
        }
    }
}
