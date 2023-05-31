using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeButton : PoolableMono
{
	public override void Init()
	{

	}

    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemText;

	public void SetData(CraftRecipeSO recipe)
	{
        itemImage.sprite = recipe.item.image;
        itemText.text = recipe.item.itemName;
	}
}
