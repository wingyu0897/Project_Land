using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObtainableMeleeItem : MeleeItem
{
	public override void OnDeselect()
	{
		base.OnDeselect();
		input.OnClickAction -= ObtainStartHandle;
	}

	public override void OnSelect()
	{
		base.OnSelect();
		input.OnClickAction += ObtainStartHandle;
	}

	public virtual void ObtainStartHandle()
	{
		if (actionData.isInteracting == false) return;
		if (animator.Animator.applyRootMotion == true) return;

		animator.Animator.applyRootMotion = true;
		ObtainAnimation();
		animator.OnEvent += ObtainHandle;
		animator.OnEnd += ObtainEndHandle;
	}

	public virtual void ObtainEndHandle()
	{
		animator.ResetTransform();
		animator.OnEvent -= ObtainHandle;
		animator.OnEnd -= ObtainEndHandle;
		animator.Animator.applyRootMotion = false;
	}

	public virtual void ObtainHandle()
	{
	}

	public virtual void StartObtain()
	{
		actionData.isActive = false;
		actionData.canChange = false;
	}

	public virtual void StopObtain()
	{
		animator.ResetTransform();
		animator.OnEvent -= ObtainHandle;
		animator.OnEnd -= ObtainEndHandle;
		animator.Animator.applyRootMotion = false;
	}

	public virtual void ObtainAnimation()
	{

	}
}
