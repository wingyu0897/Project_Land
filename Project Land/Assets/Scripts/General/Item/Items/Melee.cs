using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Item
{
	public MeleeItemDataSO meleeData;

	private PlayerInput input;
	private PlayerMovement movement;
	private PlayerActionData actionData;
	private PlayerAnimator animator;
	private DamageCaster damageCaster;

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
		gameObject.SetActive(false);
		input.OnAttackAction -= AttackStartHandle;
		input.OnRollingAction -= AttackEndHandle;
		AttackEndHandle();
	}

	public override void OnSelect()
	{
		gameObject.SetActive(true);
		input.OnAttackAction += AttackStartHandle;
		input.OnRollingAction += AttackEndHandle;
	}

	public void AttackStartHandle()
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

	public void AttackEndHandle()
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

	public void AttackHandle()
	{
		damageCaster.SphereCast(meleeData.attackRadius);
	}
}
