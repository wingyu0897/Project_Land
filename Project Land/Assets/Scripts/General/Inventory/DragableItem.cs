using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragableItem : PoolableMono, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public Image image;
	public TextMeshProUGUI countText;

	public List<Item> itemList = new List<Item>();
	public Item Item
	{
		get
		{
			if (itemList.Count == 0)
				return null;
			else
				return itemList[0];
		}
	}

	[HideInInspector]
	public Transform parentSlot;

	private void Awake()
	{
		image = GetComponent<Image>();
		countText = transform.Find("CountText").GetComponent<TextMeshProUGUI>();
	}

	public void SetImage(Sprite sprite)
	{
		image.sprite = sprite;
	}

	public void SetCountText()
	{
		int count = itemList.Count;
		countText.text = count.ToString();

		if (count <= 1)
			countText.enabled = false;
		else
			countText.enabled = true;
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		parentSlot = transform.parent;
		transform.SetParent(transform.root.root.root);
		transform.SetAsLastSibling();
		image.raycastTarget = false;
	}

	public void OnDrag(PointerEventData eventData)
	{
		transform.position = Input.mousePosition;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		transform.SetParent(parentSlot.transform);
		image.raycastTarget = true;
	}

	public void AddItem(Item input)
	{
		itemList.Add(input);

		if (itemList.Count <= 1)
			countText.enabled = false;
		else
			countText.enabled = true;
	}

	public void ChangeParent(Transform parent)
	{
		InventorySlot oldSlot = parentSlot?.parent.GetComponent<InventorySlot>();
		oldSlot.dragItem = null;

		if (parentSlot?.parent.parent.name != "HotSlots")
		{
			PoolManager.Instance.Push(oldSlot);
		}

		if (SelectItem.Instance.CurrentSelected == itemList[0])
		{
			SelectItem.Instance.Select(this);
		}

		parentSlot = parent;
	}

	public void RemoveItem()
	{
		if (itemList.Count == 0) return;

		Item drop = itemList[itemList.Count - 1];
		drop.OnDrop();
		itemList.Remove(drop);
		SetCountText();

		if (itemList.Count == 0)
		{
			InventorySlot oldSlot = parentSlot?.parent.GetComponent<InventorySlot>();
			oldSlot.dragItem = null;
			oldSlot.gameObject.SetActive(false);
			if (parentSlot?.parent.parent.name != "HotSlots")
				PoolManager.Instance.Push(oldSlot);
			PoolManager.Instance.Push(this);
		}
	}

	public override void Init()
	{
		image.sprite = null;
		countText.text = "0";
		countText.enabled = false;
	}
}
