using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeItem : Item
{
	public override void OnSelect()
	{
		gameObject.SetActive(true);
	}

	public override void OnDeselect()
	{
		gameObject.SetActive(false);
	}
}
