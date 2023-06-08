using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
	private Animator animator;
	public Animator Animator => animator;

	private readonly int speedHash = Animator.StringToHash("speed");
	private readonly int attackHash = Animator.StringToHash("attack");

	public event Action OnEvent = null;
	public event Action OnEnd = null;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	public void ResetTransform()
	{
		transform.localPosition = -Vector3.up;
		transform.localEulerAngles = Vector3.zero;
	}

	public void OnAnimationEvent()
	{
		OnEvent?.Invoke();
	}

	public void OnAnimationEnd()
	{
		OnEnd?.Invoke();
	}

	public void SetSpeed(float speed)
	{
		animator.SetFloat(speedHash, speed);
	}

	public void SetAttackTrigger(bool attack)
	{
		if (attack)
		{
			animator.SetTrigger(attackHash);
		}
		else
		{
			animator.ResetTrigger(attackHash);
		}
	}
}
