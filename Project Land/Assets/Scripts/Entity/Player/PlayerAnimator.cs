using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
	public Animator Animator => animator;

	private readonly int speedHash = Animator.StringToHash("speed");
	private readonly int movexHash = Animator.StringToHash("move_x");
	private readonly int moveyHash = Animator.StringToHash("move_y");
	private readonly int jumpHash = Animator.StringToHash("jump");
	private readonly int rollHash = Animator.StringToHash("roll");
	private readonly int attackHash = Animator.StringToHash("attack");
	private readonly int walkHash = Animator.StringToHash("walk");
	private readonly int swordAttackHash = Animator.StringToHash("sword_attack");
	private readonly int meleeAttackHash = Animator.StringToHash("melee_attack");
	private readonly int deadHash = Animator.StringToHash("dead");
	private readonly int isdeadHash = Animator.StringToHash("is_dead");
	private readonly int iswaklHash = Animator.StringToHash("is_walk");

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

	public void SetMeleeAttackBool(bool attack)
	{
		animator.SetBool(meleeAttackHash, attack);
	}

	public void SetAttackTrigger(bool attack)
	{
		animator.SetTrigger(attackHash);
	}

	public void SetSpeed(float speed)
	{
		animator.SetFloat(speedHash, speed);
	}

	public void SetMove(Vector2 move)
	{
		animator.SetFloat(movexHash, move.x);
		animator.SetFloat(moveyHash, move.y);
	}

	public void SetWalkBool(bool walk)
	{
		animator.SetBool(iswaklHash, walk);
	}

	public void SetWalkTrigger(bool walk)
	{
		if (walk)
		{
			animator.SetTrigger(walkHash);
		}
		else
		{
			animator.ResetTrigger(walkHash);
		}
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
		if (dead == false)
		{
			ResetTransform();
		}
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
