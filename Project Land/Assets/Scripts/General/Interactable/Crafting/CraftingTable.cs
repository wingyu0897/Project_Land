using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingTable : Interactable
{
	private Inventory inventory;

	private CraftRecipeSO currentRecipe;

	[field:SerializeField]
	public override string Name { get; set; }

	[Header("Recipe")]
	[SerializeField] private CraftRecipeListSO recipeList;
	private List<RecipeButton> recipeBtns = new List<RecipeButton>();
	
	[Header("UI")]
	[SerializeField] private GameObject craftingUI;
	[SerializeField] private RecipeButton recipeButtonPrefab;
	[SerializeField] private Transform recipeButtonParent;
	[SerializeField] private Image resultImage;
	[SerializeField] private List<ItemSlot> requireItemSlots;
	[SerializeField] private Button closeBtn;
	[SerializeField] private Button craftBtn;
	
	public override void OnInteract()
	{
		Init();
		craftingUI.SetActive(true);
	}

	public override void StopInteract()
	{
		craftingUI.SetActive(false);
		actionData.isInteracting = false;
	}

	private void Init()
	{
		closeBtn.onClick.RemoveAllListeners();
		closeBtn.onClick.AddListener(StopInteract);
		craftBtn.onClick.RemoveAllListeners();
		craftBtn.onClick.AddListener(CraftItem);
		craftBtn.interactable = false;
		resultImage.sprite = null;
		resultImage.gameObject.SetActive(false);
		foreach (ItemSlot its in requireItemSlots)
		{
			its.gameObject.SetActive(false);
		}
		for (int i = 0; i < recipeList.recipes.Count; i++)
		{
			recipeBtns[i].SetData(recipeList.recipes[i]);
		}
	}

	protected override void Start()
	{
		MakeRecipeBtn();
		base.Start();
		inventory = Define.Instance.player.GetComponent<Inventory>();
	}

	public void MakeRecipeBtn()
	{
		for (int i = 0; i < recipeList.recipes.Count; i++)
		{
			if (recipeButtonPrefab == null)
				print("Btn");
			RecipeButton recipeBtn = PoolManager.Instance.Pop(recipeButtonPrefab.gameObject.name) as RecipeButton;
			recipeBtn.crafting = this;
			recipeBtn.transform.SetParent(recipeButtonParent);
			recipeBtn.transform.localScale = Vector3.one;
			recipeBtn.SetData(recipeList.recipes[i]);

			recipeBtns.Add(recipeBtn);
		}
	}

	public void SetRecipe(CraftRecipeSO recipe)
	{
		resultImage.sprite = recipe.resultItem.image;
		resultImage.gameObject.SetActive(true);
		for (int i = 0; i < recipe.requires.Count; i++)
		{
			requireItemSlots[i].gameObject.SetActive(true);
			requireItemSlots[i].SetItem(recipe.requires[i].item, recipe.requires[i].count);
		}
		craftBtn.interactable = inventory.FindItem(recipe);
		currentRecipe = recipe;
	}

	public void CraftItem()
	{
		if (!inventory.FindItem(currentRecipe))
			return;

		for (int i = 0; i < currentRecipe.requires.Count; i++)
		{
			inventory.RemoveItem(inventory.FindItem(currentRecipe.requires[i].item.prefab), currentRecipe.requires[i].count);
		}

		Item item = PoolManager.Instance.Pop(currentRecipe.resultItem.prefab.gameObject.name) as Item;
		inventory.AddItem(item);
		craftBtn.interactable = inventory.FindItem(currentRecipe);
	}
}
