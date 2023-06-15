using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBrain : PoolableMono
{
	[HideInInspector]
	public EnemyActionData actionData;
	[HideInInspector]
	public Movement movement;
	[HideInInspector]
	public EnemyAnimator animator;
	private EnemyHealth health;
	private Collider col;

	[Header("Set")]
	[SerializeField] private BaseAIState defaultState;
	[SerializeField] private BaseAIState currentState;

	public Transform target;

	[Header("Event")]
	public UnityEvent OnDeadEvent = null;

	private void Awake()
	{
		actionData = transform.Find("AI").GetComponent<EnemyActionData>();
		movement = GetComponent<Movement>();
		animator = GetComponent<EnemyAnimator>();
		health = GetComponent<EnemyHealth>();
		col = GetComponent<Collider>();

		health.OnDie.AddListener(OnDead);
	}

	private void Start()
	{
		target = Define.Instance.player;
	}

	private void Update()
	{
		currentState.UpdateState();
		foreach (AITransition tr in currentState?.transitions)
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
		actionData.isDead = true;
		col.enabled = false;

		OnDeadEvent?.Invoke();

		StartCoroutine(Dead());
	}

	public override void Init()
	{
		StopAllCoroutines();

		actionData.isDead = false;
		col.enabled = true;

		ChangeState(defaultState);

		health.Init();
	}

	private IEnumerator Dead()
	{
		yield return new WaitForSeconds(10f);

		PoolManager.Instance.Push(this);
	}
}
