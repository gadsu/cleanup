
//    AimRender
//    This script's entire function is to show the pretty arc upon aiming.
//    IN HEAVY PROGRESS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimRender : MonoBehaviour {
    public GameObject reticle;
    public ThrowParticle tp;
    float xmax = 50f;
    PlayerController pc;
    Vector2 launchVel = new Vector2();
    bool visible;
    LineRenderer lr;
    public bool inAir;
    List<Vector3> points = new List<Vector3>();
    float lastframeval;

	// Use this for initialization
	void Start () {
        reticle = GameObject.Find("aimReticle");
        tp = GameObject.Find("throwReticle").GetComponent<ThrowParticle>();
        pc = GetComponent<PlayerController>();
        lr = reticle.GetComponent<LineRenderer>();
        launchVel.y = 30f;

        reticle.SetActive(false);
        visible = false;
	}
	
	// Update is called once per frame
	void Update () {
        //We only want to recalculate if there is a noticeable change but this is what we got
        //if aiming
        if (pc.aiming && !inAir)
        {
            if(!visible)
            {
                reticle.SetActive(true);
                visible = true;
            }
            //if moving at all and not throwing
            if (Input.GetAxis("Aim") != 0 && Input.GetAxis("Aim") != lastframeval)
            {
                launchVel.x = Input.GetAxis("Aim")*xmax;
                Cast();
            }
        }
        if(!pc.aiming && visible)
        {
            clearLine();
            hideParticle();
        }
        lastframeval = Input.GetAxis("Aim");
    }

    //Throw a ball, record the points, add at specific intervals
    void Cast()
    {
        tp.Throw(launchVel);
        inAir = true;
    }

    public void AddSpot(Transform pos)
    {
        points.Add(pos.position);
    }

    public void DrawLine(Transform pos)
    {
        reticle.transform.position = pos.position;
        lr.enabled = true;
        lr.positionCount = points.Count;
        lr.SetPositions(points.ToArray());
    }

    public void clearLine()
    {
        points.Clear();
        lr.enabled = false;
    }

    public void hideParticle()
    {
        reticle.SetActive(false);
        visible = false;
        Debug.Log(reticle.activeSelf);
    }


    //void DrawLine(Transform endpos)
    //{
    //    Vector3 current = new Vector3(-5f, 0f, 0f);
    //    while (current.y >= ymin)
    //    {
    //        points.Add(current);
    //        current += launchVel * Time.fixedDeltaTime;
    //        launchVel += Physics.gravity * Time.fixedDeltaTime;
    //    }

    //    LineRenderer renderer = gameObject.GetComponent<LineRenderer>();
    //    renderer.positionCount = points.Count;
    //    renderer.SetPositions(points.ToArray());
    //}

    //Draw dots along a line, link them to a line renderer
}
