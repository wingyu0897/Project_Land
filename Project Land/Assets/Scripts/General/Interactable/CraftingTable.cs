using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : Interactable
{
	[SerializeField] private float interactTime;
	public override float InteractTime => interactTime;

	public override void OnInteract()
	{
		print("Interact!");
		actionData.isInteracting = false;
	}
}
