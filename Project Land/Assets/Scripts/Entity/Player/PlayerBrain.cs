using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBrain : MonoBehaviour
{
	[HideInInspector]
	public PlayerMovement movement;
    [HideInInspector]
    public PlayerActionData actionData;
	[HideInInspector]
	public PlayerAnimator animator;
	[HideInInspector]
	public PlayerHealth health;

	private void Awake()
	{
		movement = GetComponent<PlayerMovement>();
		actionData = GetComponent<PlayerActionData>();
		animator = transform.Find("Visual").GetComponent<PlayerAnimator>();
		health = GetComponent<PlayerHealth>();

		health.OnDead += Dead;
	}

	private void Start()
	{
		animator.Init();
		health.Init();
	}

	public void Dead()
	{
		movement.StopMovement();
		actionData.isDead = true;
		animator.SetDeadBool(true);
		animator.SetDeadTrigger(true);
	}
}
