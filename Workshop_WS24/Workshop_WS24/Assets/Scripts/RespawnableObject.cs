using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnableObject : MonoBehaviour
{
    public RespawnManager respawnManager;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Transform initialParent;

    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        initialParent = transform.parent;

        RespawnManager.Instance.RegisterObject(this);
    }


    public void Respawn()
    {
        transform.SetParent(initialParent);
        transform.SetPositionAndRotation(initialPosition, initialRotation);
        gameObject.SetActive(true);

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    /*
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
    */


}
