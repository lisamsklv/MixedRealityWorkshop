using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager Instance;

    private List<RespawnableObject> respawnObjects = new();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void RegisterObject(RespawnableObject obj)
    {
        if (!respawnObjects.Contains(obj))
            respawnObjects.Add(obj);
    }

    public void RespawnAll()
    {
        foreach (var obj in respawnObjects)
        {
            if (obj != null)
                obj.Respawn();
        }
    }

    public void RespawnByTag(string tag)
    {
        foreach (var obj in respawnObjects)
        {
            if (obj != null && obj.CompareTag(tag))
                obj.Respawn();
        }
    }

    /*
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
    */
}

