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

    private bool hasBeans = false;
    private bool isGrinding = false;
    private bool hasGroundCoffee = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CoffeeBag") && !hasBeans)
        {
            hasBeans = true;
            Destroy(other.gameObject);
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
        Debug.Log("Start grinding");

        // Sound hier einf�gen

        yield return new WaitForSeconds(grindDuration);

        hasBeans = false;
        hasGroundCoffee = true;
        isGrinding = false;

        Debug.Log("Finished grinding");

        if (GroundCoffeeBag_Root != null && outputPoint != null)
        {
            Instantiate(GroundCoffeeBag_Root, outputPoint.position, outputPoint.rotation);
        }
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
