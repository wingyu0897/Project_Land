using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodResource : Resource
{
	public override void Obtain()
	{
		Instantiate(resource.data.prefab, inven.transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
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
