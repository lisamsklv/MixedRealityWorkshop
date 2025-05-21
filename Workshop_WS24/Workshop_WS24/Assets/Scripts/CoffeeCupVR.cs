using System.Collections.Generic;
using UnityEngine;

public class CoffeeCupVR : MonoBehaviour
{
    private List<string> ingredientTags = new List<string>();

    [Header("Finalized Drink Info")]
    public string finalDrinkName; // Set this when the drink is complete
    public bool isFinalized = false;

    public void AddIngredient(string tag)
    {
        if (isFinalized)
        {
            Debug.LogWarning("[CoffeeCupVR] Cannot add ingredients. Drink is finalized.");
            return;
        }

        ingredientTags.Add(tag);
        Debug.Log($"[CoffeeCupVR] Added ingredient tag: {tag}");
    }

    public bool MatchesRecipe(List<string> requiredTags)
    {
        if (requiredTags.Count != ingredientTags.Count)
        {
            Debug.Log("[CoffeeCupVR] Ingredient count mismatch.");
            return false;
        }

        foreach (string tag in requiredTags)
        {
            if (!ingredientTags.Contains(tag))
            {
                Debug.Log($"[CoffeeCupVR] Missing tag: {tag}");
                return false;
            }
        }

        return true;
    }

    public bool MatchesFinalRecipe(string expectedName)
    {
        if (!isFinalized)
        {
            Debug.LogWarning("[CoffeeCupVR] Drink is not finalized yet.");
            return false;
        }

        bool match = finalDrinkName == expectedName;
        Debug.Log($"[CoffeeCupVR] Final drink name check: {finalDrinkName} == {expectedName} ? {match}");
        return match;
    }

    // Optional helper to finalize the drink
    public void FinalizeDrink(string recipeName)
    {
        finalDrinkName = recipeName;
        isFinalized = true;
        Debug.Log($"[CoffeeCupVR] Drink finalized as: {finalDrinkName}");
    }
}
