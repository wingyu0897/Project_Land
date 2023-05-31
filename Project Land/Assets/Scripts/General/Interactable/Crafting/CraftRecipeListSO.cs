using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/DATA/RecipeList")]
public class CraftRecipeListSO : ScriptableObject
{
    public List<CraftRecipeSO> recipes;
}
