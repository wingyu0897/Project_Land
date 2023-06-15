using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : BaseAIState
{
	public override void OnEnterState()
	{
		brain.animator.Animator.applyRootMotion = true;
		brain.animator.SetIsDeadBool(true);
	}

	public override void OnExitState()
	{
		brain.animator.Animator.applyRootMotion = false;
		brain.animator.SetIsDeadBool(false);
	}

	public override void UpdateState()
	{

	}
}
