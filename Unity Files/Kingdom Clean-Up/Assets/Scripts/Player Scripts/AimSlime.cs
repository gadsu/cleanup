using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimSlime : MonoBehaviour {

    public GameObject reticle;
    Rigidbody2D rbRet;

    PlayerController pc;
    bool visible;

    // Use this for initialization
    void Start ()
    {
        reticle = GameObject.Find("aimReticle");
        pc = GetComponent<PlayerController>();
        reticle.SetActive(false);
        visible = false;
        rbRet = reticle.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (pc.aiming)
        {
            if (!visible)
            {
                reticle.SetActive(true);
                visible = true;
            }
            //if moving at all and not throwing
            if (Input.GetAxis("Aim") != 0)
            {
                Vector2 newpos = rbRet.position;

                rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX;



                rbRet.position = newpos;
            }
        }
        if (!pc.aiming && visible)
        {
            reticle.SetActive(false);
            visible = false;
        }
    }
}
