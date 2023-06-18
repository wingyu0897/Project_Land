using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : PoolableMono, IDropHandler
{
	public DragableItem dragItem = null;
	public bool isHot = false;

	public override void Init()
	{

	}

	public void OnDrop(PointerEventData eventData)
	{
		if (transform.GetChild(0).childCount != 0)
			return;

		GameObject dropped = eventData.pointerDrag;
		DragableItem dragItem = dropped.GetComponent<DragableItem>();

		if (dragItem != null)
		{
			dragItem.ChangeParent(transform.GetChild(0));
			this.dragItem = dragItem;
		}
	}

	public void RemoveSlot()
	{
		if (!isHot || !Inventory.isShowing)
		{
			gameObject.SetActive(false);
		}
		print(transform.root.gameObject.name);
		dragItem.ChangeParent(transform.root);
		dragItem = null;
		if (!isHot)
			PoolManager.Instance.Push(this);
	}
}
