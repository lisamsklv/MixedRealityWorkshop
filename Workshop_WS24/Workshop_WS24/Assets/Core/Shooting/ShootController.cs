using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ShootController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform spawnTransform;
    public float firepower = 10;
    public XRGrabInteractable grabInteractable;
    
    void Start()
    {
        grabInteractable.activated.AddListener(OnTrigger);
    }

    void OnTrigger(ActivateEventArgs arg0)
    {
        var bullet = Instantiate(bulletPrefab, spawnTransform.position, spawnTransform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * firepower, ForceMode.Impulse);
    }
}
