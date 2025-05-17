using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject[] customerPrefabs; // assign your 3 prefabs in inspector
    public Transform spawnPoint;

    public float spawnInterval = 5f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnCustomer), 1f, spawnInterval);
    }

    void SpawnCustomer()
    {
        int randomIndex = Random.Range(0, customerPrefabs.Length);
        Instantiate(customerPrefabs[randomIndex], spawnPoint.position, Quaternion.identity);
    }
}

