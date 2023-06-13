using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Land : PoolableMono
{
	public abstract override void Init();
	public abstract void OnSpawned();
}
