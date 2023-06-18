using UnityEngine;

public class ClickAndDropResource : Resource, IDamageable
{
	[SerializeField] protected Item dedicatedTool;
	[SerializeField] protected int dedicatedToolBuff = 2;
	[SerializeField] protected int maxHealth;
	[SerializeField] protected int dropCount = 3;
	protected int health;

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
		for (int i = 0; i < dropCount; ++i)
		{
			Item item = PoolManager.Instance.Pop(resource.data.prefab.name) as Item;
			Vector3 pos = transform.position + new Vector3(0, 1f * (i + 1), 0);
			float ran = Random.Range(0, 360);
			item.transform.SetPositionAndRotation(pos, Quaternion.Euler(Vector3.one * ran));
		}
	}

	public void OnDamaged(int damage, Transform attacker)
	{
		if (!Obtainable()) return;

		if (SelectItem.Instance.CurrentSelected?.Item.GetType() != dedicatedTool.GetType() && attacker.gameObject.name.Equals("Player"))
			damage /= dedicatedToolBuff;

		health -= damage;
		health = Mathf.Clamp(health, 0, maxHealth);

		if (health == 0)
		{
			Obtain();
			PoolManager.Instance.Push(this);
		}
	}
}
