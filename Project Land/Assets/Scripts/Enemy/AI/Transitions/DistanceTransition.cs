using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTransition : AITransition
{
	[SerializeField] private float radius;

	public override bool Change()
	{
		bool result = Vector3.Distance(transform.position, brain.target.position) <= radius;
		return !isReverse ? result : !result;
	}

#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		if (UnityEditor.Selection.activeGameObject == gameObject)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, radius);
			Gizmos.color = Color.white;
		}
	}
#endif
}
