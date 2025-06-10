using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnableObject : MonoBehaviour
{
    public RespawnManager respawnManager;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Transform initialParent;
    private float respawnDelay = 2f;
    public GameObject prefabToRespawn;

    private Renderer[] renderers;
    private Collider[] colliders;
    private Rigidbody rb;

    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        initialParent = transform.parent;

        RespawnManager.Instance.RegisterObject(this);
    }

    void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
        colliders = GetComponentsInChildren<Collider>();
        rb = GetComponent<Rigidbody>();
    }



    public void Respawn()
    {
        StartCoroutine(RespawnWithDelay());
    }

    private IEnumerator RespawnWithDelay()
    {
        foreach (var r in renderers) r.enabled = false;
        foreach (var c in colliders) c.enabled = false;
        if (rb != null) rb.isKinematic = true;


        yield return new WaitForSeconds(respawnDelay);

        if (prefabToRespawn != null)
        {
            Instantiate(prefabToRespawn, initialPosition, initialRotation);
            Destroy(gameObject);
        }

        transform.SetParent(initialParent);
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        

        gameObject.SetActive(true);

        if (rb)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }


        foreach (var r in renderers) r.enabled = true;
        foreach (var c in colliders) c.enabled = true;
        if (rb != null) rb.isKinematic = false;
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
