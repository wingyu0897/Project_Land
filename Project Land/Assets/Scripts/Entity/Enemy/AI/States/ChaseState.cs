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
		brain.movement.StopMovement();
	}

	public override void UpdateState()
	{
		Vector3 dir = brain.target.position - transform.position;
		dir.y = 0;
		brain.movement.SetMove(dir.normalized);
	}
}
