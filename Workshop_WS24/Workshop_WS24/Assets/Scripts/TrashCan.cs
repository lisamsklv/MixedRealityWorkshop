using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    public AudioSource trashSound;

    private void OnTriggerEnter(Collider other)
    {
       RespawnableObject respawnable = other.GetComponent<RespawnableObject>();
        if (respawnable != null)
        {
            respawnable.Respawn();

            if (trashSound != null)
            {
                trashSound.Play();
            }
        } 
    }
}
