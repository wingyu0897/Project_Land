using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeItem : Item
{
	public MeleeItemDataSO meleeData;

	protected PlayerInput input;
	protected PlayerMovement movement;
	protected PlayerActionData actionData;
	protected PlayerAnimator animator;
	protected DamageCaster damageCaster;

	protected virtual void Start()
	{
		input = Define.Instance.player.GetComponent<PlayerInput>();
		movement = Define.Instance.player.GetComponent<PlayerMovement>();
		actionData = Define.Instance.player.GetComponent<PlayerActionData>();
		animator = Define.Instance.player.Find("Visual").GetComponent<PlayerAnimator>();
		damageCaster = Define.Instance.player.GetComponent<DamageCaster>();
	}

	public override void OnDeselect()
	{
		base.OnDeselect();

		input.OnAttackClickAction -= AttackStartHandle;
		input.OnRollingAction -= AttackEndHandle;
		AttackEndHandle();
	}

	public override void OnSelect()
	{
		base.OnSelect();

		input.OnAttackClickAction += AttackStartHandle;
		input.OnRollingAction += AttackEndHandle;
	}

	public virtual void AttackStartHandle()
	{
		movement.StopMovement();

		animator.Animator.applyRootMotion = true;
		animator.SetSwordAttackBool(true);
		animator.SetAttackTrigger(true);
		animator.OnEnd += AttackEndHandle;
		animator.OnEvent += AttackHandle;

		actionData.isActive = false;
		actionData.isAttacking = true;
		actionData.canChange = false;
	}

	public virtual void AttackEndHandle()
	{
		if (!actionData.isAttacking) return;

		animator.ResetTransform();
		animator.SetSwordAttackBool(false);
		animator.OnEnd -= AttackEndHandle;
		animator.OnEvent -= AttackHandle;

		animator.Animator.applyRootMotion = false;
		actionData.isActive = true;
		actionData.isAttacking = false;
		actionData.canChange = true;
	}

	public virtual void AttackHandle()
	{
		damageCaster.SphereCast(meleeData.attackRadius);
	}
}
