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
		foreach (PoolingObject po in poolingList.poolingList)
		{
			Transform trm;
			try
			{
				trm = GameObject.Find(po.parent).transform;
			}
			catch
			{
				trm = null;
			}
			CreatePool(po.prefab, po.count, trm);
		}
	}

	public void CreatePool(PoolableMono prefab, int count, Transform parent)
	{
		Pool<PoolableMono> pool = new Pool<PoolableMono>(prefab, parent, count);
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
		}
	}
}
