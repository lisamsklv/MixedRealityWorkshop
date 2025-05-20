using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CoffeeMachine : MonoBehaviour
{
    public XRSocketInteractor cupSocket;
    public float brewDuration = 5f;
    public Transform outputPoint;
    public GameObject coffeePrefab;
    public AudioSource coffeeSound;

    private bool hasGroundCoffee = false;
    private bool isBrewing = false;
    public bool coffeeReady = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GroundCoffee") && !hasGroundCoffee && !isBrewing)
        {
            hasGroundCoffee = true;
            Destroy(other.gameObject);
            Debug.Log("Ground coffee inserted");
            StartCoroutine(BrewCoffee());
        }
    }

    private IEnumerator BrewCoffee()
    {
        if (!cupSocket.hasSelection)
        {
            Debug.Log("No cup in socket!");
            yield break;
        }

        isBrewing = true;
        Debug.Log("Brewing coffee...");

        if (coffeeSound != null && !coffeeSound.isPlaying)
        {
            coffeeSound.Play();
        }

        yield return new WaitForSeconds(brewDuration);

        isBrewing = false;
        hasGroundCoffee = false;

        if (coffeePrefab != null && outputPoint != null)
        { 
            Instantiate(coffeePrefab, outputPoint.transform);
            Debug.Log("Coffee ready");
        }
        else
        {
            Debug.LogWarning("Kaffeeprefab oder Ausgabeposition fehlt!");
            
        }
    }

}
