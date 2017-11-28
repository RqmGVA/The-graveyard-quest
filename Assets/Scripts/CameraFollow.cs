using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField]
    private float yMinimum;
    [SerializeField]
    private float yMaximum;
    [SerializeField]
    private float xMinimum;
    [SerializeField]
    private float xMaximum;

    private Transform target;

    // Use this for initialization
    void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(target.position.x, xMinimum, xMaximum), Mathf.Clamp(target.position.y, yMinimum, yMaximum), transform.position.z);


    }
}
