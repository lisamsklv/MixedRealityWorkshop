using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AutoRespawnInSocket : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public float respawnDelay = 1f;

    private XRSocketInteractor socket;
    private bool isSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        isSpawning = true;
        socket = GetComponent<XRSocketInteractor>();
        TrySpawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (!socket.hasSelection && !isSpawning)
        {
            isSpawning = true;
            Invoke(nameof(TrySpawn), respawnDelay);
        }
    }

    private void TrySpawn()
    {
        if (socket.hasSelection)
        {
            isSpawning = false;
            return;
        }

        GameObject newObj = Instantiate(prefabToSpawn, transform.position, transform.rotation);
        
        if (newObj.TryGetComponent(out XRGrabInteractable grab))
        {
            socket.StartManualInteraction(grab);
        }

        isSpawning = false;
    }
}
