using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RecipeRequire
{
    public ItemDataSO item;
    public int count;
}

[CreateAssetMenu(menuName = "SO/DATA/Recipe")]
public class CraftRecipeSO : ScriptableObject
{
    public List<RecipeRequire> requires;
    public ItemDataSO item;
}
