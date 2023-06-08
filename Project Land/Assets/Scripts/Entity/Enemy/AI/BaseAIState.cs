using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAIState : MonoBehaviour
{
    protected EnemyBrain brain;
	protected EnemyActionData actionData;
	public List<AITransition> transitions;

	private void Awake()
	{
		brain = transform.parent.parent.GetComponent<EnemyBrain>();
		actionData = transform.parent.GetComponent<EnemyActionData>();
		GetComponents(transitions);
	}

	public abstract void OnEnterState();
    public abstract void OnExitState();
    public abstract void UpdateState();
}
