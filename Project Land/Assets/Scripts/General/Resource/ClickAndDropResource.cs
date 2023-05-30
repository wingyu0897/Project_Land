using UnityEngine;

public class ClickAndDropResource : Resource, IDamageable
{
	[SerializeField] private Item dedicatedTool;
	[SerializeField] private int maxHealth;
	private int health;

	public void Awake()
	{
		Init();
	}

	public override void Init()
	{
		health = maxHealth;
	}

	public override bool Obtainable()
	{
		if (health <= 0)
			return false;

		return true;
	}

	public override void Obtain()
	{
		for (int i = 0; i < 3; ++i)
		{
			Item item = PoolManager.Instance.Pop(resource.data.prefab.name) as Item;
			item.transform.position = transform.position + new Vector3(0, 1f * (i + 1), 0);
		}
	}

	public void OnDamaged(int damage)
	{
		if (!Obtainable()) return;

		if (SelectItem.Instance.CurrentSelected.Item.GetType() != dedicatedTool.GetType())
			damage /= 10;

		health -= damage;
		health = Mathf.Clamp(health, 0, maxHealth);

		if (health == 0)
		{
			Obtain();
			PoolManager.Instance.Push(this);
		}
	}
}
