using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offering : MonoBehaviour
{
	public float contribution;
	public float quotaStart;
	public float quota;
	public float quotaMax;
	public float quotaIncre;

	private void Awake()
	{
		Init();
	}

	public void Init()
	{
		contribution = 0;
		quota = quotaStart;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.TryGetComponent(out Item item))
		{
			if (item.isDrop)
			{
				if (contribution >= quota)
					return;

				contribution += item.price;
				GameManager.Instance.Contribution += item.price;
				PoolManager.Instance.Push(item);
			}
		}
	}

	public void IncrementQuota()
	{
		quota *= quotaIncre;
		quota = Mathf.Clamp(quota, 0, quotaMax);
	}

	public void ResetQuota()
	{
		quota = 0;
	}
}
