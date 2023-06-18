using System.Collections.Generic;
using UnityEngine;

public class ResourceLand : Land
{
	[SerializeField] private int resourceCount;
	[SerializeField] private List<Resource> resource;
	public List<Resource> resources;

	public override void Init()
	{
		
	}

	public override void OnSpawned()
	{
		SpawnTrees();
	}

	private void SpawnTrees()
	{
		foreach (Resource re in resource)
		{
			for (int i = 0; i < resourceCount; i++)
			{
				Vector3 position = new Vector3(Random.Range(-10f, 10f), 0.5f, Random.Range(-10f, 10f));

				position += transform.position;
				position.y = 0.5f;

				Resource res = PoolManager.Instance.Pop(re.gameObject.name) as Resource;
				res.transform.SetPositionAndRotation(position, Quaternion.Euler(0, Random.Range(0, 360), 0));
				float size = Random.Range(0.7f, 1.3f);
				res.transform.localScale = Vector3.one * size;;
				res.Init();
			}
		}
	}

	public override int GetWeight()
	{
		return weight;
	}
}
