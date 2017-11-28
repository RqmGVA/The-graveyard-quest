using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiesSpawner : MonoBehaviour {

    [SerializeField]
    private float timeToSpawn = 3.0f;
    [SerializeField]
    private GameObject zombiePrefab;
    private Transform zombieSpawner;

    public bool directionSpawnRight;
	// Use this for initialization
	void Start () {
        zombieSpawner = GetComponent<Transform>();
        StartCoroutine(SpawningZombie());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator SpawningZombie()
    {

        while (true)
        {
            GameObject zombie = Instantiate(zombiePrefab, zombieSpawner.position, zombieSpawner.rotation);
            yield return new WaitForSeconds(timeToSpawn);
        }
        }
    }

