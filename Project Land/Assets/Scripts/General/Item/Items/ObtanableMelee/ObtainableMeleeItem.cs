using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObtainableMeleeItem : MeleeItem
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

	public virtual void ObtainStartHandle()
	{
		if (actionData.isObtaining == false) return;
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
		resManager.currentResource?.Obtain();
	}

	public virtual void StartObtain()
	{
		actionData.isActive = false;
		actionData.canChange = false;
		resManager.OnStopObtaining += StopObtain;
	}

	public virtual void StopObtain()
	{
		animator.ResetTransform();
		animator.OnEvent -= ObtainHandle;
		animator.OnEnd -= ObtainEndHandle;
		animator.Animator.applyRootMotion = false;
		resManager.OnStopObtaining -= StopObtain;
	}

	public virtual void ObtainAnimation()
	{

	}
}
