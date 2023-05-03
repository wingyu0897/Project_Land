using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
	public static PoolManager Instance;

	private Dictionary<string, Pool<PoolableMono>> pools = new Dictionary<string, Pool<PoolableMono>>();

	[SerializeField] private PoolingListSO poolingList;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		foreach (PoolingObject po in poolingList.poolingList)
		{
			CreatePool(po.prefab, po.count);
		}
	}

	public void CreatePool(PoolableMono prefab, int count)
	{
		Pool<PoolableMono> pool = new Pool<PoolableMono>(prefab, transform, count);
		pools.Add(prefab.gameObject.name, pool);
	}

	public PoolableMono Pop(string monoName)
	{
		if (pools.ContainsKey(monoName))
		{
			PoolableMono ret = pools[monoName].Pop();
			ret.Init();
			return ret;
		}
		else
		{
			Debug.LogError("No Pool!");
			return null;
		}
	}

	public void Push(PoolableMono mono)
	{
		if (pools.ContainsKey(mono.name))
		{
			mono.transform.SetParent(transform);
			pools[mono.name].Push(mono);
		}
		else
		{
			Debug.LogError($"No \"{mono.name}\" Pool!");
			CreatePool(mono, 0);
			Push(mono);
		}
	}
}
