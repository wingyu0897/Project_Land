using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodResource : Resource
{
	public override void Obtain()
	{
		Item wood = PoolManager.Instance.Pop(resource.data.prefab.name) as Item;
		wood.transform.position = inven.transform.position + new Vector3(0, 1f, 0);
	}

	public override void OnStartObtain()
	{
		movement.SetPosition((transform.position + (movement.transform.position - transform.position).normalized - movement.transform.position));
		movement.SetRotate(transform.position - movement.transform.position, 1f);
		actionData.isActive = false;
		actionData.canChange = false;
	}

	public override void OnStopObtain()
	{
		actionData.isActive = true;
		actionData.canChange = true;
	}
}
