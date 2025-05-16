using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnableObject : MonoBehaviour
{
    public RespawnManager respawnManager;

    private void Update()
    {
        if (this != null && transform.position.y < -10f)
        {
            TriggerRespawn();
        }
    }

    private void OnDestroy()
    {
        if (respawnManager != null && gameObject.scene.isLoaded)
        {
            TriggerRespawn();
        }
    }

    private void TriggerRespawn()
    {

        respawnManager.StartRespawn();
 
    }


}
