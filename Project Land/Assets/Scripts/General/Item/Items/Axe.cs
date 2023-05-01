using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : Melee
{
	protected ResourceManager resManager;

	protected override void Start()
	{
		base.Start();
		resManager = Define.Instance.player.GetComponent<ResourceManager>();
	}

	public override void OnDeselect()
	{
		base.OnDeselect();
		input.OnClickAction -= ObtainStartHandle;
		resManager.OnStartObtaining -= StartObtain;
	}

	public override void OnSelect()
	{
		base.OnSelect();
		input.OnClickAction += ObtainStartHandle;
		resManager.OnStartObtaining += StartObtain;
	}

	public void ObtainStartHandle()
	{
		if (actionData.isObtaining == false) return;
		if (animator.Animator.applyRootMotion == true) return;

		animator.Animator.applyRootMotion = true;
		animator.SetSwordAttackBool(true);
		animator.SetAttackTrigger(true);
		animator.OnEvent += ObtainHandle;
		animator.OnEnd += ObtainEndHandle;
	}

	public virtual void ObtainEndHandle()
	{
		animator.ResetTransform();
		animator.SetSwordAttackBool(false);
		animator.OnEvent -= ObtainHandle;
		animator.OnEnd -= ObtainEndHandle;
		animator.Animator.applyRootMotion = false;
	}

	public void ObtainHandle()
	{
		resManager.currentResource?.Obtain();
	}

	public void StartObtain()
	{
		AttackEndHandle();
		actionData.isActive = false;
		actionData.canChange = false;
		resManager.OnStopObtaining += StopObtain;
	}

	public void StopObtain()
	{
		animator.ResetTransform();
		animator.SetSwordAttackBool(false);
		animator.OnEvent -= ObtainHandle;
		animator.OnEnd -= ObtainEndHandle;
		animator.Animator.applyRootMotion = false;
		resManager.OnStopObtaining -= StopObtain;
	}
}
