using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : ObtainableMeleeItem
{
	public override void ObtainStartHandle()
	{
		base.ObtainStartHandle();
	}

	public override void ObtainEndHandle()
	{
		base.ObtainEndHandle();
		animator.SetSwordAttackBool(false);
	}

	public override void ObtainHandle()
	{
		base.ObtainHandle();
	}

	public override void StartObtain()
	{
		AttackEndHandle();
		base.StartObtain();
	}

	public override void StopObtain()
	{
		animator.SetSwordAttackBool(false);
		base.StopObtain();
	}

	public override void ObtainAnimation()
	{
		animator.SetSwordAttackBool(true);
		animator.SetAttackTrigger(true);
	}
}
