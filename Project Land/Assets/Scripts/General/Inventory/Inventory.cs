using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	[SerializeField] private GameObject dragableItemPrefab;
	[SerializeField] private GameObject slotPrefab;
	[SerializeField] private GameObject slotsUI;
	[SerializeField] private GameObject hotslotsUI;

	List<DragableItem> items = new List<DragableItem>();

	List<InventorySlot> hotSlots = new List<InventorySlot>();
	public List<InventorySlot> HotSlots => hotSlots;

	private bool isShowing = false;
	public bool IsShowing => isShowing;

	private void Start()
	{
		for (int i = 0; i < 8; i++)
		{
			InventorySlot slot = PoolManager.Instance.Pop(slotPrefab.name) as InventorySlot;
			slot.gameObject.transform.SetParent(hotslotsUI.transform);
			slot.transform.Find("Number").GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();
			hotSlots.Add(slot);
		}

		ShowInventory(false);
	}

	public void AddItem(Item item)
	{
		foreach (DragableItem sl in items)
		{
			if (sl.itemList[0].GetType().Equals(item.GetType()))
			{
				if (sl.itemList[0].data.maxCapacity > sl.itemList.Count)
				{
					sl.AddItem(item);
					sl.SetImage(item.data.image);
					sl.SetCountText();
					item.OnPickUp();
					return;
				}
			}
		}

		DragableItem dragableItem = PoolManager.Instance.Pop(dragableItemPrefab.name) as DragableItem;
		dragableItem.image.sprite = item.data.image;
		dragableItem.AddItem(item);
		items.Add(dragableItem);
		item.OnPickUp();
		AddSlot(dragableItem);
	}

	public void AddSlot(DragableItem dragableItem)
	{
		InventorySlot invSlot = PoolManager.Instance.Pop(slotPrefab.name) as InventorySlot;
		invSlot.transform.SetParent(slotsUI.transform.GetChild(0).GetChild(0));
		dragableItem.transform.SetParent(invSlot.transform.Find("Content"));
		dragableItem.parentSlot = dragableItem.transform.parent;
	}

	public void RemoveItem(DragableItem item)
	{
		items.Remove(item);
	}

	/// <summary>
	/// 인벤토리 UI 보이기 & 숨기기
	/// </summary>
	/// <param name="show"></param>
	public void ShowInventory(bool show)
	{
		isShowing = show;

		slotsUI.gameObject.SetActive(show);

		foreach (InventorySlot inv in hotSlots)
		{
			if (inv.transform.GetChild(0).childCount == 0)
			{
				inv.gameObject.SetActive(show);
			}
		}
	}
}
