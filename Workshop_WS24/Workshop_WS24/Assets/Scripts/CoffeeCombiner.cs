using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeCombiner : MonoBehaviour
{
    public GameObject milkCoffeePrefab;
    public GameObject bloodCoffeePrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Milk") && CompareTag("CoffeeCup"))
        {
            Vector3 spawnPosition = transform.position;
            Quaternion spawnRotation = transform.rotation;

            // Optional: alte Objekte zerstören
            Destroy(other.gameObject);  // Milch
            Destroy(gameObject);        // Kaffee

            // Milchkaffee erzeugen
            Instantiate(milkCoffeePrefab, spawnPosition, spawnRotation);
        }

        if (other.CompareTag("Blood") && CompareTag("CoffeeCup"))
        {
            Vector3 spawnPosition = transform.position;
            Quaternion spawnRotation = transform.rotation;

            // Optional: alte Objekte zerstören
            Destroy(other.gameObject);  // Milch
            Destroy(gameObject);        // Kaffee

            // Milchkaffee erzeugen
            Instantiate(bloodCoffeePrefab, spawnPosition, spawnRotation);
        }
    }
}
