using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class CoffeeGrinder : MonoBehaviour
{
    public Button grindButton;
    public float grindDuration = 3f;
    public GameObject GroundCoffeeBag_Root;
    public Transform outputPoint;
    public ParticleSystem grindParticles;
    public AudioSource grindSound;

    private bool hasBeans = false;
    private bool isGrinding = false;
    private bool hasGroundCoffee = false;

    private void OnTriggerEnter(Collider other)
    {
        RespawnableObject respawnable = other.GetComponent<RespawnableObject>();
        

        if (other.CompareTag("CoffeeBag") && !hasBeans && respawnable != null)
        {
            hasBeans = true;
            respawnable.Respawn();
            Debug.Log("Beans inserted");
            
        }
    }

    void OnEnable()
    {
        if (grindButton != null)
        {
            grindButton.onClick.AddListener(OnGrindButtonPressed);
        }
    }

    private void OnDisable()
    {
        if (grindButton != null)
        {
            grindButton.onClick.RemoveListener(OnGrindButtonPressed);

        }
    }

    
    private void OnGrindButtonPressed()
    {
        if (hasBeans && !isGrinding && !hasGroundCoffee)
        {
            StartCoroutine(GrindCoffee());
        }
        else
        {
            Debug.Log("Grind failed");
        }
    }

    private IEnumerator GrindCoffee()
    {
        isGrinding = true;
        if (grindParticles != null)
        {
            grindParticles.Play();
        }
        Debug.Log("Start grinding");

        if (grindSound != null && !grindSound.isPlaying)
        {
            grindSound.Play();
        }

        yield return new WaitForSeconds(grindDuration);

        if (grindParticles != null)
        {
            grindParticles.Stop();
        }

        hasBeans = false;
        hasGroundCoffee = true;
        isGrinding = false;

        Debug.Log("Finished grinding");

        if (GroundCoffeeBag_Root != null && outputPoint != null)
        {
            Instantiate(GroundCoffeeBag_Root, outputPoint.position, outputPoint.rotation);
        }

        
        hasGroundCoffee = false;
    }

    public bool TryTakeGroundCoffee()
    {
        if (hasGroundCoffee)
        {
            hasGroundCoffee = false;
            return true;
        }
        return false;
    }

   
}
