using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    private const float minimumValue = 0.1f;
    private Vector3 positionA;
    private Vector3 positionB;
    private Vector3 nextPosition;

    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private Transform childTransform;
    [SerializeField]
    private Transform transformB;
    // Use this for initialization
    void Start()
    {

        positionA = childTransform.localPosition;
        positionB = transformB.localPosition;
        nextPosition = positionB;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        childTransform.localPosition = Vector3.MoveTowards(childTransform.localPosition, nextPosition, movementSpeed * Time.deltaTime);
        if (Vector3.Distance(childTransform.localPosition, nextPosition) <= minimumValue)
        {
            ChangeDestination();
        }
    }
    private void ChangeDestination()
    {
        nextPosition = nextPosition != positionA ? positionA : positionB;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.transform.SetParent(childTransform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
