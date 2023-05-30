using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	[SerializeField] private GameObject dragableItemPrefab;
	[SerializeField] private GameObject slotPrefab;
	[SerializeField] private GameObject slotsParent;
	[SerializeField] private GameObject hotslotsParent;

	private List<DragableItem> items = new List<DragableItem>();

	private List<InventorySlot> hotSlots = new List<InventorySlot>();
	public List<InventorySlot> HotSlots => hotSlots;

	[HideInInspector]
	public static bool isShowing = false;
	public bool IsShowing => isShowing;

	private void Start()
	{
		for (int i = 0; i < 8; i++)
		{
			InventorySlot slot = hotslotsParent.transform.GetChild(i).GetComponent<InventorySlot>();
			slot.isHot = true;
			hotSlots.Add(slot);
		}

		ShowInventory(false);
	}

	public void AddItem(Item item)
	{
		item.OnPickUp();

		foreach (DragableItem sl in items)
		{
			if (sl.Item.GetType().Equals(item.GetType()))
			{
				if (sl.Item.data.maxCapacity > sl.ItemCount)
				{
					sl.AddItem(item);
					return;
				}
			}
		}

		DragableItem dragableItem = PoolManager.Instance.Pop(dragableItemPrefab.name) as DragableItem;
		dragableItem.AddItem(item);
		items.Add(dragableItem);
		AddSlot(dragableItem);
	}

	public void AddSlot(DragableItem dragableItem)
	{
		InventorySlot invSlot = PoolManager.Instance.Pop(slotPrefab.name) as InventorySlot;
		invSlot.transform.SetParent(slotsParent.transform.GetChild(0).GetChild(0));
		dragableItem.transform.SetParent(invSlot.transform.Find("Content"));
		dragableItem.parentSlot = dragableItem.transform.parent;
	}

	public void RemoveItem(DragableItem item)
	{
		if (SelectItem.Instance?.CurrentSelected == item)
		{
			SelectItem.Instance.Deselect();
		}

		items.Remove(item);
	}

	public DragableItem FindItem(Item item)
	{
		try
		{
			DragableItem ret = items.Find(x => x.Item.GetType() == item.GetType());
			return ret;
		}
		catch
		{
			return null;
		}
	}

	/// <summary>
	/// 인벤토리 UI 보이기 & 숨기기
	/// </summary>
	/// <param name="show"></param>
	public void ShowInventory(bool show)
	{
		isShowing = show;

		slotsParent.gameObject.SetActive(show);

		foreach (InventorySlot inv in hotSlots)
		{
			if (inv.transform.GetChild(0).childCount == 0)
			{
				inv.gameObject.SetActive(show);
			}
		}
	}
}
