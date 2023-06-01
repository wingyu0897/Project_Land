using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotPanel : MonoBehaviour, IDropHandler
{
	private Inventory inventory;

	private void Start()
	{
		inventory = Define.Instance.player.GetComponent<Inventory>();
	}

	public void OnDrop(PointerEventData eventData)
	{
		GameObject dropped = eventData.pointerDrag;
		DragableItem dragItem = dropped.GetComponent<DragableItem>();

		if (dragItem != null)
		{
			if (dragItem.parentSlot.parent.parent.name == "HotSlots")
			{
				InventorySlot oldSlot = dragItem.parentSlot?.parent.GetComponent<InventorySlot>();
				oldSlot.dragItem = null;
				if (SelectItem.Instance.CurrentSelected == dragItem)
				{
					SelectItem.Instance.Select(dragItem);
				}

				inventory.AddSlot(dragItem);
			}
		}
	}
}
