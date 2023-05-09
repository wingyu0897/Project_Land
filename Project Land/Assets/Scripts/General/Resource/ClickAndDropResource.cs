using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAndDropResource : Resource
{
	public override void Obtain()
	{
		Item item = PoolManager.Instance.Pop(resource.data.prefab.name) as Item;
		item.transform.position = inven.transform.position + new Vector3(0, 1f, 0);
	}

	public override void OnStartObtain()
	{
		Vector3 pos = transform.position + (movement.transform.position - transform.position).normalized * 1.5f - movement.transform.position;
		pos.y = 0;
		movement.SetPosition(pos);
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
