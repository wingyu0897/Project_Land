using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : PoolableMono
{
	private EnemyHealth health;
	[HideInInspector]
	public Movement movement;
	[HideInInspector]
	public EnemyAnimator animator;

	[SerializeField] private BaseAIState currentState;

	public Transform target;

	private void Awake()
	{
		health = GetComponent<EnemyHealth>();
		movement = GetComponent<Movement>();
		animator = GetComponent<EnemyAnimator>();
	}

	private void Start()
	{
		target = Define.Instance.player;
	}

	private void Update()
	{
		currentState.UpdateState();
		foreach (AITransition tr in currentState.transitions)
		{
			if (tr.Change())
			{
				ChangeState(tr.nextState);
				break;
			}
		}
	}

	public void ChangeState(BaseAIState state)
	{
		currentState?.OnExitState();
		currentState = state;
		currentState?.OnEnterState();
	}

	public override void Init()
	{
		health.Init();
	}
}
