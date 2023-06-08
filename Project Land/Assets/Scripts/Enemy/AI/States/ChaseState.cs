using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseAIState
{
	public override void OnEnterState()
	{
		brain.animator.SetSpeed(1f);
	}

	public override void OnExitState()
	{
		brain.animator.SetSpeed(0f);
	}

	public override void UpdateState()
	{
		brain.movement.Move(brain.target.position - transform.position);
	}
}
