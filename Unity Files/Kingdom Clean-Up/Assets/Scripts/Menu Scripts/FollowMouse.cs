using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour {

    public GameObject playerSelect;
    Vector3 oldPos;

	// Use this for initialization
	void Start () {
		oldPos = Input.mousePosition;
        oldPos.z = 65f;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 curPos = Input.mousePosition;
        curPos.z = 65f;

        if (oldPos != curPos)
        {
            playerSelect.transform.position = Camera.main.ScreenToWorldPoint(curPos);
        }

        oldPos = curPos;
	}
}
