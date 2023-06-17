using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Land : PoolableMono
{
	public int weight;

	public abstract override void Init();
	public abstract void OnSpawned();
	public abstract int GetWeight();
}
