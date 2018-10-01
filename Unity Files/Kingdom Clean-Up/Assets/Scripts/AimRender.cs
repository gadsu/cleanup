//scrap for now, ask Berman for help

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimRender : MonoBehaviour {
    public Transform reticle;
    float ymin = -5f;
    Vector3 launchVel = new Vector3();

    List<Vector3> points = new List<Vector3>();

	// Use this for initialization
	void Start () {
        DrawLine(GameObject.Find("aimReticle").GetComponent<Transform>());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void DrawLine(Transform endpos)
    {
        Vector3 current = new Vector3(-5f, 0f, 0f);
        while (current.y >= ymin)
        {
            points.Add(current);
            current += launchVel * Time.fixedDeltaTime;
            launchVel += Physics.gravity * Time.fixedDeltaTime;
        }

        LineRenderer renderer = gameObject.GetComponent<LineRenderer>();
        renderer.positionCount = points.Count;
        renderer.SetPositions(points.ToArray());
    }

    //Draw dots along a line, link them to a line renderer
}
