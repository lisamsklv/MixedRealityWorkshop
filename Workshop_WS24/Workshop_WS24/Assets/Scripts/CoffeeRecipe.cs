using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public enum DrinkType
{
    Coffee,
    Smoothie
}

public class CoffeeRecipe
{
    public string name;
    public DrinkType drinkType;
    public List<string> ingredients;

    public CoffeeRecipe(string name, DrinkType type, List<string> ingredients)
    {
        this.name = name;
        this.drinkType = type;
        this.ingredients = ingredients;
    }
}


