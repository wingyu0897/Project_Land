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

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.transform.TryGetComponent(out Item item))
		{
			inventory.AddItem(item);
		}
	}
}
