//-------------COMPLETE DO NOT TOUCH----------//


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeBubbles : MonoBehaviour
{
    public GameObject narrativeBubble;
    public bool bubbleOn = false;

    private void OnTriggerStay2D(Collider2D col)
    {
        if (Input.GetButton("Interact"))
        {
            narrativeBubble.SetActive(true);
            bubbleOn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (bubbleOn == true)
        {
            narrativeBubble.SetActive(false);
            bubbleOn = false;
        }
    }
}
