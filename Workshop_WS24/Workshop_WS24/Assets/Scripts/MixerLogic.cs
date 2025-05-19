using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MixerLogic : MonoBehaviour
{

    public List<string> currentIngredients = new List<string>();
    public float mixTime = 4f;
    public GameObject resultSpawnPoint;

    public GameObject brainSmoothiePrefab;
    public GameObject lungsSmoothiePrefab;
    public GameObject heartSmoothiePrefab;
    public GameObject brainLungsSmoothiePrefab;
    public AudioSource blenderSound;

    public XRSocketInteractor cupSocket;

    private bool isMixing = false;
    private HashSet<string> validTags = new HashSet<string> { "Brain", "Lung", "Heart", "Blood" };

    private void OnTriggerEnter(Collider other)
    {
        string ingredientTag = other.tag;

        if (!isMixing && IsValidIngredientTag(ingredientTag))
        {
            currentIngredients.Add(ingredientTag);
            Destroy(other.gameObject);
        }
    }

    public void StartMixing()
    {
        if (!cupSocket.hasSelection)
        {
            Debug.Log("No cup in mixer base!");
            return;
        }

        if (isMixing || currentIngredients.Count == 0) return;

        isMixing = true;
        if (blenderSound != null && !blenderSound.isPlaying)
        {
            blenderSound.Play();
        }
        Invoke(nameof(FinishMixing), mixTime);
    }

    void FinishMixing()
    {
        string smoothieType = GetSmoothieType(currentIngredients);
        GameObject result = null;

        switch (smoothieType)
        {
            case "Brain":
                result = Instantiate(brainSmoothiePrefab, resultSpawnPoint.transform.position, Quaternion.identity);
                break;
            case "Lung":
                result = Instantiate(lungsSmoothiePrefab, resultSpawnPoint.transform.position, Quaternion.identity);
                break;
            case "Heart":
                result = Instantiate(heartSmoothiePrefab, resultSpawnPoint.transform.position, Quaternion.identity);
                break;
            case "Brain&Lungs":
                result = Instantiate(brainLungsSmoothiePrefab, resultSpawnPoint.transform.position, Quaternion.identity);
                break;
            default:
                Debug.Log("Unknown smoothie!");
                break;
        }

        isMixing = false;
    }

    string GetSmoothieType(List<string> ingredients)
    {
        ingredients.Sort();

        if (ingredients.Contains("Brain") && ingredients.Contains("Blood"))
            return "Brain";

        if (ingredients.Contains("Lung") && ingredients.Contains("Blood"))
            return "Lung";

        if (ingredients.Contains("Heart") && ingredients.Contains("Blood") )
            return "Heart";

        if (ingredients.Contains("Brain") && ingredients.Contains("Blood") && ingredients.Contains("Lung"))
            return "Brain&Lungs";

        return "Unknown";
    }

    bool IsValidIngredientTag(string tag)
    {
        return validTags.Contains(tag); 
    }

    private void OnEnable()
    {
        cupSocket.selectEntered.AddListener(OnKrugPlatziert);
    }

    private void OnDisable()
    {
        cupSocket.selectEntered.RemoveListener(OnKrugPlatziert);
    }

    private void OnKrugPlatziert(SelectEnterEventArgs args)
    {
        StartMixing();
    }
}
