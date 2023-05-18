using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Inventory))]
public class SelectItem : MonoBehaviour
{
	public static SelectItem Instance;

	private Inventory inventory;

	private DragableItem currentSelected = null;
	public DragableItem CurrentSelected => currentSelected;

	public UnityEvent OnDeselectItem = null;

	private void Awake()
	{
		Debug.LogWarning("예외처리 안 됨");
		Instance = this;

		inventory = GetComponent<Inventory>();
	}

	public void Select(int number)
	{
		if (inventory.HotSlots[number].dragItem != null)
		{
			DragableItem nextItem = inventory.HotSlots[number].dragItem;
			if (currentSelected != null && nextItem == currentSelected)
			{
				Deselect();
				return;
			}
			currentSelected?.Item.OnDeselect();
			currentSelected = nextItem;
			currentSelected?.Item.OnSelect();
		}
	}

	public void Select(DragableItem nextItem)
	{
		if (currentSelected != null && nextItem == currentSelected)
		{
			Deselect();
			return;
		}
		currentSelected?.Item.OnDeselect();
		currentSelected = nextItem;
		currentSelected?.Item.OnSelect();
	}

	public void Deselect()
	{
		currentSelected?.Item?.OnDeselect();
		currentSelected = null;
		OnDeselectItem?.Invoke();
	}

	public void DropSelected(int count)
	{
		if (currentSelected == null) 
			return;

		for(int i = 0; i < count; i++)
		{
			currentSelected.RemoveItem();
			if (currentSelected.ItemCount == 0)
			{
				inventory.RemoveItem(currentSelected);
				Deselect();
				return;
			}
		}
	}
}
