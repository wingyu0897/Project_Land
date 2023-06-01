using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragableItem : PoolableMono, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public Image image;
	public TextMeshProUGUI countText;

	private Item item;
	private int itemCount;
	public int ItemCount => itemCount;

	public Item Item
	{
		get
		{
			if (itemCount == 0)
				return null;
			else
				return item;
		}
	}

	[HideInInspector]
	public Transform parentSlot;

	public override void Init()
	{
		image.sprite = null;
		countText.text = "0";
		countText.enabled = false;
	}

	public void SetCountText() // 개수 텍스트 설정 // 개수가 1 이하면 텍스트를 비활성화한다.
	{
		countText.text = itemCount.ToString();
		countText.enabled = itemCount > 1;
	}

	public void OnBeginDrag(PointerEventData eventData) // 드래그를 시작했을 때
	{
		parentSlot = transform.parent;
		transform.SetParent(transform.root.root.root);
		transform.SetAsLastSibling();
		image.raycastTarget = false;
	}

	public void OnDrag(PointerEventData eventData) // 드래그 중
	{
		transform.position = Input.mousePosition;
	}

	public void OnEndDrag(PointerEventData eventData) // 드래그를 끝냈을 때
	{
		transform.SetParent(parentSlot.transform);
		image.raycastTarget = true;
	}

	public void AddItem(Item input)
	{
		itemCount++;

		if (itemCount <= 1)
		{
			item = input;
			countText.enabled = false;
			image.sprite = input.data.image;
		}
		else
		{
			PoolManager.Instance.Push(input);
			SetCountText();
		}
	}

	public void DropItem()
	{
		if (itemCount == 0) return;

		Item drop;

		if (itemCount > 1)
			drop = PoolManager.Instance.Pop(item.data.prefab.gameObject.name) as Item;
		else
			drop = item;

		drop.OnDrop();
		itemCount--;
		SetCountText();

		if (itemCount == 0)
		{
			item = null;
			InventorySlot oldSlot = parentSlot?.parent.GetComponent<InventorySlot>();
			oldSlot.RemoveSlot();
			PoolManager.Instance.Push(this);
		}
	}

	public void RemoveItem()
	{
		if (itemCount == 0) return;

		itemCount--;
		SetCountText();

		if (itemCount == 0)
		{
			item = null;
			InventorySlot oldSlot = parentSlot?.parent.GetComponent<InventorySlot>();
			oldSlot.RemoveSlot();
			PoolManager.Instance.Push(this);
		}
	}

	public void ChangeParent(Transform parent)
	{
		if (parentSlot == parent) return;

		InventorySlot oldSlot = parentSlot?.parent.GetComponent<InventorySlot>();
		oldSlot.dragItem = null;

		if (parentSlot?.parent.parent.name != "HotSlots")
		{
			PoolManager.Instance.Push(oldSlot);
		}

		if (SelectItem.Instance.CurrentSelected == item)
		{
			SelectItem.Instance.Select(this);
		}

		parentSlot = parent;
	}
}
