using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotAttackingDecision : AIDecision
{
	public override bool Condition()
	{
		if (isReverse)
			return actionData.isAttacking;
		return !actionData.isAttacking;
	}
}
