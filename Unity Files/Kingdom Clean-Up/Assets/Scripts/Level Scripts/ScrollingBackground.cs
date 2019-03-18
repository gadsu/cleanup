using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {
    [Tooltip("How fast the background scrolls?")]
    public float paralaxSpeed;

    public float paralaxSpeedY;

    public bool paralax;
    public Transform cameraTransform;
    private Vector3 lastCamera;


    private void Start()
    {
        //cameraTransform = Camera.main.transform;
        lastCamera = cameraTransform.position;
    }

    private void Update()
    {
        if (paralax)
        {
            float deltaX = cameraTransform.position.x - lastCamera.x;
            float deltaY = cameraTransform.position.y - lastCamera.y;
            transform.position += Vector3.right * (deltaX * paralaxSpeed);
            transform.position += Vector3.up * (deltaY * paralaxSpeedY);
        }

        lastCamera = cameraTransform.position;
    }
}
