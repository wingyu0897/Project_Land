using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;

	private readonly int speedHash = Animator.StringToHash("speed");
	private readonly int jumpHash = Animator.StringToHash("jump");
	private readonly int rollHash = Animator.StringToHash("roll");

	private void Awake()
	{
		animator = GetComponent<Animator>();
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
}
