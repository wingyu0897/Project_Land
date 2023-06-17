using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneMeleeItem : MeleeItem
{
	public override void AttackStartHandle()
	{
		movement.StopMovement();

		animator.SetWalkTrigger(true);
		animator.SetWalkBool(true);
		animator.SetAttackTrigger(true);
		animator.SetMeleeAttackBool(true);
		animator.OnEnd += AttackEndHandle;
		animator.OnEvent += AttackHandle;

		actionData.isAttacking = true;
		actionData.canChange = false;

		actionData.canRun = false;
		actionData.canRotate = false;
	}

	public override void AttackEndHandle()
	{
		if (!actionData.isAttacking) return;

		animator.ResetTransform();
		animator.SetWalkBool(false);
		animator.SetMeleeAttackBool(false);
		animator.OnEnd -= AttackEndHandle;
		animator.OnEvent -= AttackHandle;

		actionData.isAttacking = false;
		actionData.canChange = true;

		actionData.canRun = true;
		actionData.canRotate = true;

		movement.ChangeMovementMode(true);
	}
}
