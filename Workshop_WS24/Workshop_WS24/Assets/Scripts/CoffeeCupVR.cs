using System.Collections.Generic;
using UnityEngine;

public class CoffeeCupVR : MonoBehaviour
{
    private List<string> ingredientTags = new List<string>();

    public void AddIngredient(string tag)
    {
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

    
}
