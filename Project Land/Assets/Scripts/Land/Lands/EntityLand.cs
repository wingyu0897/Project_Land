using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityLand : Land
{
	public EnemyBrain prefab;
	private EnemyBrain entity;
	[SerializeField] protected float respawnTime;
	private float currentTime = 0;
	private bool respawn = false;

	public override int GetWeight()
	{
		return weight;
	}

	public override void Init()
	{

	}

	public override void OnSpawned()
	{
		SpawnEntity();
		entity.health.OnDie.AddListener(OnDieHandle);
	}

	protected virtual void Update()
	{
		if (respawn)
		{
			currentTime += Time.deltaTime;

			if (currentTime > respawnTime)
			{
				SpawnEntity();
			}
		}
	}

	protected virtual void OnDieHandle()
	{
		currentTime = 0;
		respawn = true;

		entity?.health?.OnDie?.RemoveListener(OnDieHandle);
	}

	protected virtual void SpawnEntity()
	{
		entity = PoolManager.Instance.Pop(prefab.gameObject.name) as EnemyBrain;
		entity.movement.CharController.enabled = false;
		entity.transform.position = transform.position + new Vector3(0, 0.5f, 0);
		entity.movement.CharController.enabled = true;

		respawn = false;
	}
}
