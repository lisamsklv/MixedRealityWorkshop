using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public List<CoffeeRecipe> allRecipes;


    public CoffeeRecipe GetRandomRecipe()
    {
        int index = Random.Range(0, allRecipes.Count);
        return allRecipes[index];
    }
}

