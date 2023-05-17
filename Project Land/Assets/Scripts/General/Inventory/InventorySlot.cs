using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : PoolableMono, IDropHandler, IPointerClickHandler
{
	public DragableItem dragItem = null;

	public override void Init()
	{
		transform.SetParent(GameObject.Find("HotSlots").transform);
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

	public void OnPointerClick(PointerEventData eventData)
	{
		Debug.Log("Click");
	}
}
