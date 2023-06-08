using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
	private Inventory inventory;

	private void Awake()
	{
		inventory = GetComponent<Inventory>();
	}

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (hit.transform.TryGetComponent(out Item item))
		{
			inventory.AddItem(item);
		}
	}
}
