using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {
    [Tooltip("How fast the background scrolls?")]
    public float paralaxSpeed;

    public bool scrolling;
    public bool paralax;
    public Transform cameraTransform;
    private float lastCameraX;


    private void Start()
    {
        //cameraTransform = Camera.main.transform;
        lastCameraX = cameraTransform.position.x;
    }

    private void Update()
    {
        if (paralax)
        {
            float deltaX = cameraTransform.position.x - lastCameraX;
            transform.position += Vector3.right * (deltaX * paralaxSpeed);
        }

        lastCameraX = cameraTransform.position.x;
    }
}
