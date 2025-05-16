using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public GameObject objectPrefab;
    public Transform spawnPoint;
    public float respawnDelay = 2f;

    private bool isRespawning = false;

    public void StartRespawn()
    {
        if (!isRespawning)
        {
            StartCoroutine(RespawnCoroutine());
        }
    }

    public IEnumerator RespawnCoroutine()
    {
        isRespawning =true;
        yield return new WaitForSeconds(respawnDelay);

        Instantiate(objectPrefab, spawnPoint.position, spawnPoint.rotation);
        isRespawning=false;
    }
}

