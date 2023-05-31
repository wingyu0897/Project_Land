using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : Interactable
{
	[field:SerializeField]
	public override string Name { get; set; }

	[Space]
	[SerializeField] private CraftRecipeListSO recipeList;
	[SerializeField] private GameObject craftingUI;
	[SerializeField] private RecipeButton recipeButtonPrefab;
	[SerializeField] private Transform recipeButtonParent;

	public override void OnInteract()
	{
		craftingUI.SetActive(true);
	}

	private void Awake()
	{
		SetRecipes();
	}

	public void SetRecipes()
	{
		for (int i = 0; i < recipeList.recipes.Count; i++)
		{
			RecipeButton recipeBtn = PoolManager.Instance.Pop(recipeButtonPrefab.gameObject.name) as RecipeButton;
			recipeBtn.transform.SetParent(recipeButtonParent);
			recipeBtn.transform.localScale = Vector3.one;
			recipeBtn.SetData(recipeList.recipes[i]);
		}
	}
}
