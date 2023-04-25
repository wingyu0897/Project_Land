using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 인벤토리의 각 칸을 구성하는 클래스. 클래스를 구성하는 아이템으로 이루어져 있다.
/// </summary>
/// <typeparam name="T"></typeparam>
[System.Serializable]
public class Space<T> where T : Item
{
	public List<T> item;
	public GameObject hotbar;

	public Space()
	{
		item = new List<T>();
	}

	public System.Type GetItemType()
	{
		return item[0].GetType();
	}
}

public class PlayerInventory : MonoBehaviour
{
	[Header("↓↓Read Only!!↓↓")]
    public List<Space<Item>> spaces = new List<Space<Item>>();
	public List<GameObject> hotbarUI = new List<GameObject>();

	private int currentHotbarNum = 0;
	private Space<Item> currentHoldSpace = null;
	public Space<Item> CurrentHoldSpace => currentHoldSpace;

	[SerializeField] private int maxCapacity = 10;

	private void Awake()
	{
		currentHotbarNum = 0;
		currentHoldSpace = null;
		spaces = new List<Space<Item>>();
	}

	#region Add & Drop 아이템 추가와 내려놓기
	public Space<Item> AddSpace(Item item)
	{
		if (spaces.Count >= maxCapacity) return null;

		Space<Item> space = new Space<Item>();

		space.item.Add(item);
		item.OnPickUp();
		SetHotbar(space);

		spaces.Add(space);

		return space;
	}

	/// <summary>
	/// 아이템을 인벤토리에 추가하는 함수
	/// </summary>
	/// <param name="item">추가하려는 아이템</param>
	/// <returns>추가 성공 여부</returns>
    public bool AddItem(Item item)
	{
		List<Space<Item>> space = spaces.FindAll(space => space.GetItemType().Equals(item.GetType()));
		if (space != null)
		{
			bool success = false;
			foreach(Space<Item> s in space)
			{
				if (s.item.Count < item.data.maxCapacity)
				{
					s.item.Add(item);
					item.OnPickUp();
					SetHotbar(s);

					success = true;
					return true;
				}
			}
			if (!success)
			{
				if (spaces.Count >= maxCapacity)
					return false;
				else
				{
					AddSpace(item);
					return false;
				}
			}
		}
		else
		{
			if (spaces.Count < maxCapacity)
				AddSpace(item);
		}
		return false;
	}

	/// <summary>
	/// 아이템을 인벤토리에서 제거하고 바닥에 내려놓는 함수
	/// </summary>
	/// <param name="item">제거하려는 아이템</param>
	/// <param name="dropOnce">하나만 제거하기</param>
	public void DropItem(Space<Item> space, bool dropOnce)
	{
		SelectItem(currentHotbarNum);
		foreach(Item item in space.item)
		{
			item.OnDrop();
		}
		spaces.Remove(spaces.Find(s => s.Equals(space)));
		UnsetHotbar(space.hotbar);
	}
	#endregion

	#region Select 아이템 선택
	public void SelectItem(int hotbar)
	{
		// 범위 내의 키가 아닐 경우 반환
		if (hotbar < 1 || hotbar > 8) return;

		if (currentHoldSpace != null)
			currentHoldSpace.item[0].OnDeselect();

		if (spaces.Count < hotbar)
		{
			currentHotbarNum = 0;
			return;
		}

		// 현재 선택된 아이템과 동일한 위치를 선택한 경우 현재 선택된 아이템 선택 해제
		if (currentHotbarNum == hotbar)
		{
			currentHoldSpace = null;
			currentHotbarNum = 0;
		}
		else // 현재 선택된 아이템과 다른 아이템이 선택된 경우 현재 선택된 아이템을 선택 해제하고 새 아이템을 선택
		{
			currentHotbarNum = hotbar;

			if (spaces.Count < hotbar)
			{
				currentHoldSpace = null;
			}
			else
			{
				currentHoldSpace = spaces[hotbar - 1];
				if (spaces[hotbar - 1].item.Count > 0)
					spaces[hotbar - 1].item[0].OnSelect();
			}
		}
	}
	#endregion

	#region UI
	private void SetHotbar(Space<Item> space)
	{
		GameObject hotbar = hotbarUI.Find(hb => hb.activeInHierarchy == false);
		hotbar.gameObject.SetActive(true);
		Image image = hotbar.transform.GetChild(0).GetChild(0).GetComponent<Image>();
		image.sprite = space.item[0].data.image;
		space.hotbar = hotbar;
	}

	private void UnsetHotbar(GameObject hotbar)
	{
		Image image = hotbar.transform.GetChild(0).GetChild(0).GetComponent<Image>();
		image.sprite = null;
		hotbar.gameObject.SetActive(false);
	}
	#endregion

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.TryGetComponent(out Item item))
		{
			AddItem(item);
		}
	}
}
