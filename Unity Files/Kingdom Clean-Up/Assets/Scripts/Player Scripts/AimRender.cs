//This script's entire function is to show the pretty arc upon aiming.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimRender : MonoBehaviour {
    public GameObject reticle;
    public ThrowParticle tp;
    float xmax = 5f;
    PlayerController pc;
    Vector2 launchVel = new Vector2();
    bool visible;
    LineRenderer lr;

    List<Vector3> points = new List<Vector3>();

	// Use this for initialization
	void Start () {
        reticle = GameObject.Find("aimReticle");
        tp = GameObject.Find("throwReticle").GetComponent<ThrowParticle>();
        pc = GetComponent<PlayerController>();
        lr = reticle.GetComponent<LineRenderer>();

        reticle.SetActive(false);
        visible = false;
	}
	
	// Update is called once per frame
	void Update () {
        //We only want to recalculate if there is a noticeable change but this is what we got
        //if aiming
        if (pc.aiming)
        {
            if(!visible)
            {
                reticle.SetActive(true);
            }
            //if moving at all
            if (Input.GetAxis("Aim") != 0)
            {
                launchVel.x = Input.GetAxis("Aim")*xmax;
            }
            Cast();
        }
        
    }

    //Throw a ball, record the points, add at specific intervals
    void Cast()
    {
        tp.Throw(launchVel);
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

    void clearLine()
    {
        points.Clear();
        lr.enabled = false;
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
