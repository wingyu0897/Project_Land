using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
	public Animator Animator => animator;

	private readonly int speedHash = Animator.StringToHash("speed");
	private readonly int jumpHash = Animator.StringToHash("jump");
	private readonly int rollHash = Animator.StringToHash("roll");
	private readonly int swordAttackHash = Animator.StringToHash("sword_attack");
	private readonly int attackHash = Animator.StringToHash("attack");
	private readonly int deadHash = Animator.StringToHash("dead");
	private readonly int isdeadHash = Animator.StringToHash("is_dead");

	public event Action OnEvent = null;
	public event Action OnEnd = null;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	public void Init()
	{
		SetDeadBool(false);
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
		ResetTransform();
	}

	public void SetSwordAttackBool(bool attack)
	{
		animator.SetBool(swordAttackHash, attack);
	}

	public void SetAttackTrigger(bool attack)
	{
		animator.SetTrigger(attackHash);
	}

	public void SetSpeed(float speed)
	{
		animator.SetFloat(speedHash, speed);
	}

	public void SetJumpTrigger(bool jump)
	{
		if (jump)
		{
			animator.SetTrigger(jumpHash);
		}
		else
		{
			animator.ResetTrigger(jumpHash);
		}
	}

	public void SetRollTrigger()
	{
		animator.SetTrigger(rollHash);
	}

	public void SetDeadBool(bool dead)
	{
		animator.applyRootMotion = dead;
		animator.SetBool(isdeadHash, dead);
	}

	public void SetDeadTrigger(bool dead)
	{
		if (dead)
			animator.SetTrigger(deadHash);
		else
			animator.ResetTrigger(deadHash);
	}
}
