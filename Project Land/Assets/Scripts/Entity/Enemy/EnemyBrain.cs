using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBrain : PoolableMono
{
	private EnemyHealth health;
	[HideInInspector]
	public Movement movement;
	[HideInInspector]
	public EnemyAnimator animator;

	[SerializeField] private BaseAIState currentState;

	public Transform target;

	[Header("Event")]
	public UnityEvent OnDeadEvent = null;

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
			if (tr.CanChange())
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

	public void OnDead()
	{
		OnDeadEvent?.Invoke();
	}

	public override void Init()
	{
		health.Init();
	}
}
