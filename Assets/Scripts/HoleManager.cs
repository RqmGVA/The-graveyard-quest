using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleManager : MonoBehaviour
{

    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private Transform spawnTransform;
    private PlayerControler playerControler;
    private void Start()
    {
        playerControler = FindObjectOfType<PlayerControler>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            playerTransform.position = spawnTransform.position;
            StartCoroutine(playerControler.PlayerTakeDamage());
        }
        if (collision.gameObject.tag == "SpawnedZombie")
        {
            Destroy(collision.gameObject);
        }
    }
}

