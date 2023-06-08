using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseAIState
{
	[SerializeField] private BaseAttack attack;

	public override void OnEnterState()
	{
		brain.animator.OnEnd += OnAttackEndHandle;
	}

	public override void OnExitState()
	{
		brain.animator.OnEnd -= OnAttackEndHandle;
	}

	public override void UpdateState()
	{
		if (actionData.isAttacking)
			return;

		Vector3 dir = brain.target.position - transform.position;
		dir.y = 0;
		if (Mathf.Acos(Vector3.Dot(transform.forward, dir.normalized)) * Mathf.Rad2Deg > 20f)
		{
			brain.movement.SetRotate(brain.target.position - transform.position, 0.025f);
			return;
		}

		if (attack.CanAttack() && !actionData.isAttacking)
		{
			actionData.isAttacking = true;
			brain.animator.SetAttackTrigger(true);
			brain.animator.OnEvent += attack.Attack;
		}
	}

	public void OnAttackEndHandle()
	{
		actionData.isAttacking = false;
		brain.animator.OnEvent -= attack.Attack;
	}
}
