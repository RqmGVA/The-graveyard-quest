using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackgroundManager : MonoBehaviour
{
    private Transform cameraTransform;
    private Transform[] layers;
    private float viewZone = 10;
    private int leftIndex;
    private int rightIndex;
    private float lastCameraX;

    [SerializeField]
    private float backgroundSize;
    [SerializeField]
    private float paralaxSpeed;
    [SerializeField]
    private bool scrolling;
    [SerializeField]
    private bool parallax;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraX = cameraTransform.position.x;

        layers = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            layers[i] = transform.GetChild(i);
        }

        leftIndex = 0;
        rightIndex = layers.Length - 1;
    }

    private void Update()
    {
        if (parallax)
        {
            float deltaX = cameraTransform.position.x - lastCameraX;
            transform.position += Vector3.right * (deltaX * paralaxSpeed);
        }
        lastCameraX = cameraTransform.position.x;
        if (scrolling)
        {

            if (cameraTransform.position.x < (layers[leftIndex].transform.position.x + viewZone))
                ScrollLeft();
            if (cameraTransform.position.x > (layers[rightIndex].transform.position.x - viewZone))
                ScrollRight();
        }

    }
    private void ScrollLeft()
    {
        float x = layers[leftIndex].position.x - backgroundSize;
        float y = layers[leftIndex].position.y;
        float z = layers[leftIndex].position.z;
        layers[rightIndex].position = new Vector3(x, y, z);

        leftIndex = rightIndex;
        rightIndex--;
        if (rightIndex < 0)
            rightIndex = layers.Length - 1;
    }

    private void ScrollRight()
    {
        float x = layers[rightIndex].position.x + backgroundSize;
        float y = layers[rightIndex].position.y;
        float z = layers[rightIndex].position.z;

        layers[leftIndex].position = new Vector3(x, y, z);
        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex == layers.Length)
            leftIndex = 0;
    }
}

