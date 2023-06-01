using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeButton : PoolableMono
{
	[HideInInspector]
	public CraftingTable crafting;
	private CraftRecipeSO recipe;
	public CraftRecipeSO Recipe => recipe;

    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemText;
	[SerializeField] private Button btn;

	public void SetData(CraftRecipeSO recipe)
	{
		this.recipe = recipe;
        itemImage.sprite = recipe.resultItem.image;
        itemText.text = recipe.resultItem.itemName;
	}

	public void OnClick()
	{
		crafting.SetRecipe(recipe);
	}

	public override void Init()
	{
		btn.onClick.RemoveAllListeners();
		btn.onClick.AddListener(OnClick);
	}
}
