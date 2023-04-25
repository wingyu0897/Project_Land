using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �κ��丮�� �� ĭ�� �����ϴ� Ŭ����. Ŭ������ �����ϴ� ���������� �̷���� �ִ�.
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
	[Header("���Read Only!!���")]
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

	#region Add & Drop ������ �߰��� ��������
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
	/// �������� �κ��丮�� �߰��ϴ� �Լ�
	/// </summary>
	/// <param name="item">�߰��Ϸ��� ������</param>
	/// <returns>�߰� ���� ����</returns>
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
	/// �������� �κ��丮���� �����ϰ� �ٴڿ� �������� �Լ�
	/// </summary>
	/// <param name="item">�����Ϸ��� ������</param>
	/// <param name="dropOnce">�ϳ��� �����ϱ�</param>
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

	#region Select ������ ����
	public void SelectItem(int hotbar)
	{
		// ���� ���� Ű�� �ƴ� ��� ��ȯ
		if (hotbar < 1 || hotbar > 8) return;

		if (currentHoldSpace != null)
			currentHoldSpace.item[0].OnDeselect();

		if (spaces.Count < hotbar)
		{
			currentHotbarNum = 0;
			return;
		}

		// ���� ���õ� �����۰� ������ ��ġ�� ������ ��� ���� ���õ� ������ ���� ����
		if (currentHotbarNum == hotbar)
		{
			currentHoldSpace = null;
			currentHotbarNum = 0;
		}
		else // ���� ���õ� �����۰� �ٸ� �������� ���õ� ��� ���� ���õ� �������� ���� �����ϰ� �� �������� ����
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
