using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieView : MonoBehaviour
{

    public Vector2 getDirection;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
            getDirection = Vector2.right;
        else
            getDirection = Vector2.left;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            Debug.Log("blblbl");
            getDirection = Vector2.right;
        }
        else
            getDirection = Vector2.left;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
            getDirection = Vector2.left;
        else
            getDirection = Vector2.right;
    }
}
