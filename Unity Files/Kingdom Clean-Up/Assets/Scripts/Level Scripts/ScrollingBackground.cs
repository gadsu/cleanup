using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {
    [Tooltip("What is the size of the image?")]
    public float backgroundSize;
    public float paralaxSpeed;

    public bool scrolling;
    public bool paralax;
    public Transform cameraTransform;
    private Transform[] layers;
    private float viewZone = 50;

    private int leftIndex;
    private int rightIndex;
    private float lastCameraX;


    private void Start()
    {
        //cameraTransform = Camera.main.transform;
        lastCameraX = cameraTransform.position.x;
        layers = new Transform[transform.childCount]; //makes an array with the number of children on the parent object
        for (int i = 0; i < transform.childCount; i ++)
        {
            layers[i] = transform.GetChild(i);
        }
        leftIndex = 0;
        rightIndex = layers.Length - 1;
    }

    private void Update()
    {
        if (paralax)
        {
            float deltaX = cameraTransform.position.x - lastCameraX;
            transform.position += Vector3.right * (deltaX * paralaxSpeed);
        }

        lastCameraX = cameraTransform.position.x;

        if (scrolling)
        {
            if (cameraTransform.position.x < layers[leftIndex].transform.position.x + viewZone)
            {
                ScrollLeft();
            }

            if (cameraTransform.position.x > layers[rightIndex].transform.position.x - viewZone)
            {
                ScrollRight();
            }
        }
    }

    private void ScrollLeft()
    {
        int lastRight = rightIndex; //temp holds what is in the rightIndex
        layers[rightIndex].position = Vector3.right * (layers[leftIndex].position.x - backgroundSize);
        leftIndex = rightIndex;
        rightIndex--;
        if(rightIndex < 0)
        {
            rightIndex = layers.Length - 1;
        }


    }

    private void ScrollRight()
    {
        int lastLeft = leftIndex; //temp holds what is in the rightIndex
        layers[leftIndex].position = Vector3.right * (layers[rightIndex].position.x + backgroundSize);
        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex == layers.Length)
        {
            leftIndex = 0;
        }
    }


}
