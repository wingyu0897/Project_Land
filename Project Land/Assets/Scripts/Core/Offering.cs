using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offering : MonoBehaviour
{
	public float contribution;

	private void Awake()
	{
		Init();
	}

	public void Init()
	{
		contribution = 0;
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.TryGetComponent(out Item item))
		{
			if (item.isDrop)
			{
				contribution += item.price;
				PoolManager.Instance.Push(item);
			}
		}
	}
}
