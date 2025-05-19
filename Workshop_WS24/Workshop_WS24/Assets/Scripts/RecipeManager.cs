using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public List<CoffeeRecipe> allRecipes;

    public CoffeeRecipe GetRandomRecipe()
    {
        if (allRecipes == null || allRecipes.Count == 0)
        {
            Debug.LogWarning("[RecipeManager] No recipes available!");
            return null;
        }

        int index = Random.Range(0, allRecipes.Count);
        return allRecipes[index];
    }
}
