using System.Collections.Generic;
using UnityEngine;

public class GrassLand : Land
{
	[SerializeField] private int resourceCount;
	[SerializeField] private Resource resource;
	public List<Resource> resources;

	public override void Init()
	{
		
	}

	public override void OnSpawned()
	{
		for (int i = 0; i < resourceCount; i++)
		{
			Vector3 position = new Vector3(Random.Range(-7.5f, 7.5f), 0.5f, Random.Range(-7.5f, 7.5f));
			for (int j = 0; j < resources.Count; j++)
			{
				while (Vector3.Distance(resources[j].transform.position, position) <= 5)
				{
					position = new Vector3(Random.Range(-7.5f, 7.5f), 0.5f, Random.Range(-7.5f, 7.5f));
					j = 0;
				}
			}
			position += transform.position;
			position.y = 0.5f;
			Resource res = PoolManager.Instance.Pop(resource.gameObject.name) as Resource;
			res.transform.position = position;
			res.Init();
		}
	}
}
