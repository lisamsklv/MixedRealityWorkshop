using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    public string[] trashTags;
    public bool destroyOnEnter = true;
    public AudioSource trashSound;


    private void OnTriggerEnter(Collider other)
    {
       foreach (string tag in trashTags)
        {
            if (other.CompareTag(tag))
            {
                Debug.Log($"Object '{other.name}' got deleted.");

                if (trashSound != null)
                {
                    trashSound.Play();
                }

                if (destroyOnEnter)
                {
                    Destroy(other.gameObject);
                }
            }
        }
    }
}
